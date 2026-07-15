using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace SmartParking
{
    public class CameraService
    {
        private Thread? _entryFrontThread;
        private Thread? _entryRearThread;
        private Thread? _exitFrontThread;
        private Thread? _exitRearThread;

        private VideoCapture? _entryFrontCapture;
        private VideoCapture? _entryRearCapture;
        private VideoCapture? _exitFrontCapture;
        private VideoCapture? _exitRearCapture;

        private PictureBox? _entryFrontBox;
        private PictureBox? _entryRearBox;
        private PictureBox? _exitFrontBox;
        private PictureBox? _exitRearBox;

        private volatile bool _isRunning;

        private readonly object _entryFrontLock = new object();
        private readonly object _entryRearLock = new object();
        private readonly object _exitFrontLock = new object();
        private readonly object _exitRearLock = new object();

        private Mat? _latestEntryFrontMat;
        private Mat? _latestEntryRearMat;
        private Mat? _latestExitFrontMat;
        private Mat? _latestExitRearMat;

        // =========================================================================
        // THÊM MỚI: 4 Cờ điều khiển đóng băng trạng thái Live Stream từng cam
        // =========================================================================
        public bool IsEntryFrontLive { get; set; } = true;
        public bool IsEntryRearLive { get; set; } = true;
        public bool IsExitFrontLive { get; set; } = true;
        public bool IsExitRearLive { get; set; } = true;

        private string _entryFrontError = "Đang kết nối...";
        private string _entryRearError = "Đang kết nối...";
        private string _exitFrontError = "Đang kết nối...";
        private string _exitRearError = "Đang kết nối...";

        public string CurrentFrontPlate { get; set; } = "30A-999.99";
        public string CurrentRearPlate { get; set; } = "30A-999.99";
        public bool IsVehiclePresent { get; set; } = true;

        public void Start(PictureBox entryFrontBox, PictureBox entryRearBox, PictureBox exitFrontBox, PictureBox exitRearBox)
        {
            Stop();

            _entryFrontBox = entryFrontBox;
            _entryRearBox = entryRearBox;
            _exitFrontBox = exitFrontBox;
            _exitRearBox = exitRearBox;
            _isRunning = true;

            // Đặt mức ưu tiên luồng cao nhất để triệt tiêu delay phần cứng
            _entryFrontThread = new Thread(EntryFrontCameraLoop) { IsBackground = true, Name = "EntryFrontThread", Priority = ThreadPriority.Highest };
            _entryRearThread = new Thread(EntryRearCameraLoop) { IsBackground = true, Name = "EntryRearThread", Priority = ThreadPriority.Highest };
            _exitFrontThread = new Thread(ExitFrontCameraLoop) { IsBackground = true, Name = "ExitFrontThread", Priority = ThreadPriority.Highest };
            _exitRearThread = new Thread(ExitRearCameraLoop) { IsBackground = true, Name = "ExitRearThread", Priority = ThreadPriority.Highest };

            _entryFrontThread.Start();
            _entryRearThread.Start();
            _exitFrontThread.Start();
            _exitRearThread.Start();
        }

        public void Stop()
        {
            _isRunning = false;

            if (_entryFrontThread != null) { _entryFrontThread.Join(200); _entryFrontThread = null; }
            if (_entryRearThread != null) { _entryRearThread.Join(200); _entryRearThread = null; }
            if (_exitFrontThread != null) { _exitFrontThread.Join(200); _exitFrontThread = null; }
            if (_exitRearThread != null) { _exitRearThread.Join(200); _exitRearThread = null; }

            lock (_entryFrontLock) { _entryFrontCapture?.Dispose(); _entryFrontCapture = null; _latestEntryFrontMat?.Dispose(); _latestEntryFrontMat = null; }
            lock (_entryRearLock) { _entryRearCapture?.Dispose(); _entryRearCapture = null; _latestEntryRearMat?.Dispose(); _latestEntryRearMat = null; }
            lock (_exitFrontLock) { _exitFrontCapture?.Dispose(); _exitFrontCapture = null; _latestExitFrontMat?.Dispose(); _latestExitFrontMat = null; }
            lock (_exitRearLock) { _exitRearCapture?.Dispose(); _exitRearCapture = null; _latestExitRearMat?.Dispose(); _latestExitRearMat = null; }
        }

        private void EntryFrontCameraLoop()
        {
            string url = DatabaseHelper.GetCameraUrl("CamEntryFrontUrl");
            DateTime lastRetry = DateTime.MinValue;

            while (_isRunning)
            {
                bool frameProcessed = false;
                try
                {
                    lock (_entryFrontLock)
                    {
                        if (_entryFrontCapture == null || !_entryFrontCapture.IsOpened)
                        {
                            if ((DateTime.Now - lastRetry).TotalSeconds > 3)
                            {
                                lastRetry = DateTime.Now;
                                _entryFrontCapture?.Dispose();
                                _entryFrontCapture = new VideoCapture(url, VideoCapture.API.Ffmpeg);
                                _entryFrontCapture.Set(CapProp.Buffersize, 0);
                            }
                        }
                    }

                    if (_entryFrontCapture != null && _entryFrontCapture.IsOpened)
                    {
                        while (_entryFrontCapture.Grab()) { break; }

                        using (Mat frame = new Mat())
                        {
                            if (_entryFrontCapture.Read(frame) && !frame.IsEmpty)
                            {
                                lock (_entryFrontLock)
                                {
                                    _latestEntryFrontMat?.Dispose();
                                    _latestEntryFrontMat = frame.Clone();
                                }
                                // CHỈ ĐẨY ẢNH LÊN MÀN HÌNH NẾU ĐANG BẬT CHẾ ĐỘ LIVE STREAM
                                if (IsEntryFrontLive)
                                {
                                    UpdatePictureBox(_entryFrontBox, frame.ToBitmap());
                                }
                                _entryFrontError = "";
                                frameProcessed = true;
                            }
                        }
                    }
                }
                catch { }

                if (!frameProcessed)
                {
                    Bitmap bmp = CreateSimulatedFrame("CỔNG VÀO - CAMERA TRƯỚC (LIVE)", CurrentFrontPlate, _entryFrontError, url);
                    if (IsEntryFrontLive) UpdatePictureBox(_entryFrontBox, bmp);
                    else bmp.Dispose();
                    Thread.Sleep(50);
                }
                else { Thread.Sleep(5); }
            }
        }

        private void EntryRearCameraLoop()
        {
            string url = DatabaseHelper.GetCameraUrl("CamEntryRearUrl");
            DateTime lastRetry = DateTime.MinValue;

            while (_isRunning)
            {
                bool frameProcessed = false;
                try
                {
                    lock (_entryRearLock)
                    {
                        if (_entryRearCapture == null || !_entryRearCapture.IsOpened)
                        {
                            if ((DateTime.Now - lastRetry).TotalSeconds > 3)
                            {
                                lastRetry = DateTime.Now;
                                _entryRearCapture?.Dispose();
                                _entryRearCapture = new VideoCapture(url, VideoCapture.API.Ffmpeg);
                                _entryRearCapture.Set(CapProp.Buffersize, 0);
                            }
                        }
                    }

                    if (_entryRearCapture != null && _entryRearCapture.IsOpened)
                    {
                        while (_entryRearCapture.Grab()) { break; }

                        using (Mat frame = new Mat())
                        {
                            if (_entryRearCapture.Read(frame) && !frame.IsEmpty)
                            {
                                lock (_entryRearLock)
                                {
                                    _latestEntryRearMat?.Dispose();
                                    _latestEntryRearMat = frame.Clone();
                                }
                                if (IsEntryRearLive)
                                {
                                    UpdatePictureBox(_entryRearBox, frame.ToBitmap());
                                }
                                _entryRearError = "";
                                frameProcessed = true;
                            }
                        }
                    }
                }
                catch { }

                if (!frameProcessed)
                {
                    Bitmap bmp = CreateSimulatedFrame("CỔNG VÀO - CAMERA SAU (LIVE)", CurrentRearPlate, _entryRearError, url);
                    if (IsEntryRearLive) UpdatePictureBox(_entryRearBox, bmp);
                    else bmp.Dispose();
                    Thread.Sleep(50);
                }
                else { Thread.Sleep(5); }
            }
        }

        private void ExitFrontCameraLoop()
        {
            string url = DatabaseHelper.GetCameraUrl("CamExitFrontUrl");
            DateTime lastRetry = DateTime.MinValue;

            while (_isRunning)
            {
                bool frameProcessed = false;
                try
                {
                    lock (_exitFrontLock)
                    {
                        if (_exitFrontCapture == null || !_exitFrontCapture.IsOpened)
                        {
                            if ((DateTime.Now - lastRetry).TotalSeconds > 3)
                            {
                                lastRetry = DateTime.Now;
                                _exitFrontCapture?.Dispose();
                                _exitFrontCapture = new VideoCapture(url, VideoCapture.API.Ffmpeg);
                                _exitFrontCapture.Set(CapProp.Buffersize, 0);
                            }
                        }
                    }

                    if (_exitFrontCapture != null && _exitFrontCapture.IsOpened)
                    {
                        while (_exitFrontCapture.Grab()) { break; }

                        using (Mat frame = new Mat())
                        {
                            if (_exitFrontCapture.Read(frame) && !frame.IsEmpty)
                            {
                                lock (_exitFrontLock)
                                {
                                    _latestExitFrontMat?.Dispose();
                                    _latestExitFrontMat = frame.Clone();
                                }
                                if (IsExitFrontLive)
                                {
                                    UpdatePictureBox(_exitFrontBox, frame.ToBitmap());
                                }
                                _exitFrontError = "";
                                frameProcessed = true;
                            }
                        }
                    }
                }
                catch { }

                if (!frameProcessed)
                {
                    Bitmap bmp = CreateSimulatedFrame("CỔNG RA - CAMERA TRƯỚC (LIVE)", CurrentFrontPlate, _exitFrontError, url);
                    if (IsExitFrontLive) UpdatePictureBox(_exitFrontBox, bmp);
                    else bmp.Dispose();
                    Thread.Sleep(50);
                }
                else { Thread.Sleep(5); }
            }
        }

        private void ExitRearCameraLoop()
        {
            string url = DatabaseHelper.GetCameraUrl("CamExitRearUrl");
            DateTime lastRetry = DateTime.MinValue;

            while (_isRunning)
            {
                bool frameProcessed = false;
                try
                {
                    lock (_exitRearLock)
                    {
                        if (_exitRearCapture == null || !_exitRearExitOpened())
                        {
                            if ((DateTime.Now - lastRetry).TotalSeconds > 3)
                            {
                                lastRetry = DateTime.Now;
                                _exitRearCapture?.Dispose();
                                _exitRearCapture = new VideoCapture(url, VideoCapture.API.Ffmpeg);
                                _exitRearCapture.Set(CapProp.Buffersize, 0);
                            }
                        }
                    }

                    if (_exitRearCapture != null && _exitRearCapture.IsOpened)
                    {
                        using (Mat frame = new Mat())
                        {
                            while (_exitRearCapture.Grab()) { break; }

                            if (_exitRearCapture.Read(frame) && !frame.IsEmpty)
                            {
                                lock (_exitRearLock)
                                {
                                    _latestExitRearMat?.Dispose();
                                    _latestExitRearMat = frame.Clone();
                                }
                                if (IsExitRearLive)
                                {
                                    UpdatePictureBox(_exitRearBox, frame.ToBitmap());
                                }
                                _exitRearError = "";
                                frameProcessed = true;
                            }
                        }
                    }
                }
                catch { }

                if (!frameProcessed)
                {
                    Bitmap bmp = CreateSimulatedFrame("CỔNG RA - CAMERA SAU (LIVE)", CurrentRearPlate, _exitRearError, url);
                    if (IsExitRearLive) UpdatePictureBox(_exitRearBox, bmp);
                    else bmp.Dispose();
                    Thread.Sleep(50);
                }
                else { Thread.Sleep(5); }
            }
        }

        private bool _exitRearExitOpened() { return _exitRearCapture != null && _exitRearCapture.IsOpened; }

        public Bitmap CaptureEntryFront()
        {
            lock (_entryFrontLock) { if (_latestEntryFrontMat != null && !_latestEntryFrontMat.IsEmpty) return _latestEntryFrontMat.ToBitmap(); }
            return CreateSimulatedFrame("CAMERA VÀO - TRƯỚC (SNAPSHOT)", CurrentFrontPlate, _entryFrontError, "");
        }

        public Bitmap CaptureEntryRear()
        {
            lock (_entryRearLock) { if (_latestEntryRearMat != null && !_latestEntryRearMat.IsEmpty) return _latestEntryRearMat.ToBitmap(); }
            return CreateSimulatedFrame("CAMERA VÀO - SAU (SNAPSHOT)", CurrentRearPlate, _entryRearError, "");
        }

        public Bitmap CaptureExitFront()
        {
            lock (_exitFrontLock) { if (_latestExitFrontMat != null && !_latestExitFrontMat.IsEmpty) return _latestExitFrontMat.ToBitmap(); }
            return CreateSimulatedFrame("CAMERA RA - TRƯỚC (SNAPSHOT)", CurrentFrontPlate, _exitFrontError, "");
        }

        public Bitmap CaptureExitRear()
        {
            lock (_exitRearLock) { if (_latestExitRearMat != null && !_latestExitRearMat.IsEmpty) return _latestExitRearMat.ToBitmap(); }
            return CreateSimulatedFrame("CAMERA RA - SAU (SNAPSHOT)", CurrentRearPlate, _exitRearError, "");
        }

        private void UpdatePictureBox(PictureBox? pictureBox, Bitmap newBitmap)
        {
            if (pictureBox == null || pictureBox.IsDisposed) { newBitmap.Dispose(); return; }
            try
            {
                if (pictureBox.InvokeRequired)
                {
                    pictureBox.BeginInvoke(new Action(() =>
                    {
                        if (!pictureBox.IsDisposed) { var oldImg = pictureBox.Image; pictureBox.Image = newBitmap; oldImg?.Dispose(); }
                        else { newBitmap.Dispose(); }
                    }));
                }
                else { var oldImg = pictureBox.Image; pictureBox.Image = newBitmap; oldImg?.Dispose(); }
            }
            catch { newBitmap.Dispose(); }
        }

        private Bitmap CreateSimulatedFrame(string cameraName, string licensePlate, string errorMsg, string currentUrl)
        {
            int width = 640; int height = 480;
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(24, 24, 27));
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(230, 15, 23, 42)), 25, height / 2 - 100, width - 50, 175);
                    g.DrawRectangle(new Pen(Color.FromArgb(220, 38, 38), 2), 25, height / 2 - 100, width - 50, 175);
                    g.DrawString("🚨 THÔNG BÁO CHẨN ĐOÁN LỖI CAMERA", new Font("Segoe UI", 11.5F, FontStyle.Bold), Brushes.OrangeRed, 40, height / 2 - 90);
                    g.DrawString($"Chi tiết lỗi: {errorMsg}", new Font("Segoe UI", 9.5F, FontStyle.Italic), Brushes.White, 40, height / 2 - 60);
                    g.DrawString($"Đường dẫn đang bắt link: {currentUrl}", new Font("Consolas", 8.5F), Brushes.LightGray, 40, height / 2 + 5);
                    g.DrawString("👉 Đang kích hoạt chế độ GIẢ LẬP ĐỂ DUY TRÌ HỆ THỐNG VẬN HÀNH...", new Font("Segoe UI", 9.5F, FontStyle.Bold), Brushes.LightGreen, 40, height / 2 + 40);
                }
                if (IsVehiclePresent)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(50, 50, 52)), width / 2 - 100, height - 120, 200, 60);
                    Rectangle plateRect = new Rectangle(width / 2 - 70, height - 95, 140, 30);
                    g.FillRectangle(Brushes.White, plateRect);
                    g.DrawString(licensePlate, new Font("Arial", 11, FontStyle.Bold), Brushes.Black, plateRect, new System.Drawing.StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
                g.FillRectangle(new SolidBrush(Color.FromArgb(120, 0, 0, 0)), 10, 10, 360, 65);
                using (Font infoFont = new Font("Consolas", 11, FontStyle.Bold))
                {
                    g.DrawString(cameraName, infoFont, Brushes.Yellow, 15, 15);
                    g.DrawString($"GIỜ: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}", infoFont, Brushes.White, 15, 35);
                    g.DrawString(string.IsNullOrEmpty(errorMsg) ? "STREAM: SIMULATOR - OK" : "STREAM: DIAGNOSTIC FALLBACK", infoFont, Brushes.Orange, 15, 55);
                }
            }
            return bmp;
        }
    }
}