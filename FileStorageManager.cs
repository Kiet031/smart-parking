using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;

namespace SmartParking
{
    public static class FileStorageManager
    {
        public static string SharedServerPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImageData");

        public static string SaveCompressedSubscriptionImage(Image img, string licensePlate, string imageType)
        {
            string cleanPlate = string.IsNullOrEmpty(licensePlate) ? "ONG_DINH_DANH_TRONG" : licensePlate.Replace(":", "-").Replace("/", "-").Trim();
            string folderPath = Path.Combine(SharedServerPath, "Subscriptions", cleanPlate);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filename = (imageType == "portrait") ? "portrait.bin" : "vehicle.bin";
            string filePath = Path.Combine(folderPath, filename);

            using (MemoryStream msJpeg = new MemoryStream())
            {
                img.Save(msJpeg, ImageFormat.Jpeg);
                byte[] jpegBytes = msJpeg.ToArray();

                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                using (GZipStream gzip = new GZipStream(fs, CompressionMode.Compress))
                {
                    gzip.Write(jpegBytes, 0, jpegBytes.Length);
                }
            }

            return $"ImageData/Subscriptions/{cleanPlate}/{filename}";
        }

        public static Image? LoadCompressedSubscriptionImage(string licensePlate, string imageType)
        {
            string cleanPlate = string.IsNullOrEmpty(licensePlate) ? "ONG_DINH_DANH_TRONG" : licensePlate.Replace(":", "-").Replace("/", "-").Trim();
            string folderPath = Path.Combine(SharedServerPath, "Subscriptions", cleanPlate);
            string filename = (imageType == "portrait") ? "portrait.bin" : "vehicle.bin";
            string filePath = Path.Combine(folderPath, filename);

            return LoadCompressedImage(filePath);
        }

        public static string SaveCompressedTransactionImage(Bitmap bmp, bool isMonthly, string licensePlate, string actionType, string position, DateTime time)
        {
            string folderName = (actionType == "Vao" || actionType.Equals("Vao", StringComparison.OrdinalIgnoreCase)) ? "Vao" : "Ra";
            string folderPath = Path.Combine(SharedServerPath, "Transactions", folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string cleanPlate = string.IsNullOrEmpty(licensePlate) ? "Casual" : licensePlate.Replace(":", "-").Replace("/", "-").Trim();
            string dateStr = time.ToString("yyyyMMdd_HHmmss");
            string filename = $"{cleanPlate}_{dateStr}_{position}.bin";
            string filePath = Path.Combine(folderPath, filename);

            using (MemoryStream msJpeg = new MemoryStream())
            {
                ImageCodecInfo? jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                if (jpgEncoder != null)
                {
                    Encoder myEncoder = Encoder.Quality;
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    myEncoderParameters.Param[0] = new EncoderParameter(myEncoder, 75L);

                    bmp.Save(msJpeg, jpgEncoder, myEncoderParameters);
                    byte[] jpegBytes = msJpeg.ToArray();

                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    using (GZipStream gzip = new GZipStream(fs, CompressionMode.Compress))
                    {
                        gzip.Write(jpegBytes, 0, jpegBytes.Length);
                    }
                }
            }

            return $"ImageData/Transactions/{folderName}/{filename}";
        }

        public static Image? LoadCompressedImage(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            string fullPath = Path.IsPathRooted(path) ? path : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            if (!File.Exists(fullPath)) return null;
            try
            {
                using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                using (GZipStream gzip = new GZipStream(fs, CompressionMode.Decompress))
                {
                    // Decompress back to RAM
                    MemoryStream msOut = new MemoryStream();
                    gzip.CopyTo(msOut);
                    msOut.Position = 0;
                    return Image.FromStream(msOut);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi giải nén ảnh: {ex.Message}");
                return null;
            }
        }

        public static string? LocateSourceFile(string filename, string licensePlate)
        {
            if (string.IsNullOrEmpty(filename) || filename == "[NO_IMAGE_FALLBACK]" || filename == "[NO_IMAGE]")
                return null;

            if (File.Exists(filename)) return filename;

            string pathBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            if (File.Exists(pathBase)) return pathBase;

            string cleanPlate = licensePlate.Replace(":", "-").Replace("/", "-").Trim();
            string pathSub = Path.Combine(SharedServerPath, "Subscriptions", cleanPlate, filename);
            if (File.Exists(pathSub)) return pathSub;

            string[] legacyDirs = { "Students", "Staff", "Lecturers" };
            foreach (var dir in legacyDirs)
            {
                string pathLegacy = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Subscriptions", dir, filename);
                if (File.Exists(pathLegacy)) return pathLegacy;

                string pathLegacyImg = Path.Combine(SharedServerPath, "Subscriptions", dir, filename);
                if (File.Exists(pathLegacyImg)) return pathLegacyImg;
            }

            return null;
        }

        // =========================================================================
        // KHÔI PHỤC: Hàm dọn dẹp giao dịch ảnh cũ được gọi từ AdminForm.cs
        // =========================================================================
        public static void CleanupOldTransactions(int retentionMonths)
        {
            if (retentionMonths <= 0) return;
            try
            {
                string transactionsPath = Path.Combine(SharedServerPath, "Transactions");
                if (!Directory.Exists(transactionsPath)) return;

                DateTime cutoff = DateTime.Today.AddMonths(-retentionMonths);
                var files = Directory.GetFiles(transactionsPath, "*.*", SearchOption.AllDirectories);

                foreach (var file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.LastWriteTime < cutoff)
                    {
                        fi.Delete();
                    }
                }

                // Tiến hành dọn dẹp các thư mục rỗng sau khi xóa file dat
                DeleteEmptyDirectories(transactionsPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi dọn dẹp file giao dịch cũ: {ex.Message}");
            }
        }

        private static void DeleteEmptyDirectories(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                DeleteEmptyDirectories(directory);
                if (Directory.GetDirectories(directory).Length == 0 && Directory.GetFiles(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }
        }

        // SỬA ĐỔI: Thêm dấu ? (Nullable) để sửa warning CS8603
        private static ImageCodecInfo? GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid) return codec;
            }
            return null;
        }
    }
}