using System;
using System.IO.Ports;
using System.Text;

namespace SmartParking
{
    public class RFIDReader
    {
        private SerialPort? _serialPort;
        
        // Sự kiện xảy ra khi thẻ được quẹt thành công
        public event Action<string>? CardSwiped;
        
        // Sự kiện báo lỗi kết nối cổng COM
        public event Action<string>? ErrorOccurred;

        public bool IsOpen => _serialPort?.IsOpen ?? false;

        public bool Start(string portName, int baudRate = 9600)
        {
            try
            {
                Stop();

                _serialPort = new SerialPort(portName, baudRate);
                _serialPort.DataBits = 8;
                _serialPort.StopBits = StopBits.One;
                _serialPort.Parity = Parity.None;
                _serialPort.Handshake = Handshake.None;
                
                // Mã hóa dữ liệu dạng text để dễ xử lý RFID dạng chuỗi
                _serialPort.Encoding = Encoding.UTF8;
                
                _serialPort.DataReceived += SerialPort_DataReceived;
                _serialPort.Open();
                
                return true;
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke($"Không thể mở cổng {portName}: {ex.Message}");
                return false;
            }
        }

        public void Stop()
        {
            if (_serialPort != null)
            {
                try
                {
                    if (_serialPort.IsOpen)
                    {
                        _serialPort.Close();
                    }
                }
                catch
                {
                    // Phớt lờ lỗi đóng cổng để tránh crash
                }
                finally
                {
                    _serialPort.DataReceived -= SerialPort_DataReceived;
                    _serialPort.Dispose();
                    _serialPort = null;
                }
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_serialPort == null || !_serialPort.IsOpen) return;

            try
            {
                // Đọc toàn bộ nội dung trong hàng đợi COM
                string rawData = _serialPort.ReadExisting().Trim();
                
                // Lọc bỏ ký tự rác (nếu có)
                string cardId = CleanCardData(rawData);
                
                if (!string.IsNullOrEmpty(cardId))
                {
                    // Kích hoạt sự kiện quẹt thẻ trên giao diện chính
                    CardSwiped?.Invoke(cardId);
                }
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke($"Lỗi đọc dữ liệu RFID: {ex.Message}");
            }
        }

        private string CleanCardData(string raw)
        {
            // RFID thường gửi dạng chuỗi hexa hoặc số nguyên kết thúc bằng \r hoặc \n
            // Ví dụ: "0012345678\r\n" hoặc "A1B2C3D4"
            var sb = new StringBuilder();
            foreach (char c in raw)
            {
                // Chỉ nhận ký tự chữ cái hoặc số (alphanumeric)
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        // Hỗ trợ giả lập từ giao diện (Cho phép quẹt thẻ bằng nút bấm hoặc phím ảo)
        public void SimulateSwipe(string cardId)
        {
            if (!string.IsNullOrWhiteSpace(cardId))
            {
                CardSwiped?.Invoke(cardId.Trim());
            }
        }
    }
}
