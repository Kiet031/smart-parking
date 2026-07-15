using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SmartParking
{
    public static class ExceptionManager
    {
        private static readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.log");

        public static void HandleException(Exception ex, string context = "")
        {
            // Log details to local file in execution root
            LogException(ex, context);

            // Get friendly message in Vietnamese
            string friendlyMessage = GetFriendlyMessage(ex, context);

            // Display custom error dialog (Critical Red)
            ShowCustomDialog(friendlyMessage, "LỖI HỆ THỐNG", isCritical: true, details: ex.ToString());
        }

        public static void ShowWarning(string message, string title = "CẢNH BÁO HỆ THỐNG")
        {
            // Display custom warning dialog (Amber/Yellow)
            ShowCustomDialog(message, title, isCritical: false);
        }

        public static void LogException(Exception ex, string context)
        {
            try
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] CONTEXT: {context}\n" +
                                  $"EXCEPTION: {ex.GetType().FullName}\n" +
                                  $"MESSAGE: {ex.Message}\n" +
                                  $"STACK TRACE:\n{ex.StackTrace}\n" +
                                  new string('=', 80) + "\n";
                File.AppendAllText(LogFilePath, logEntry);
            }
            catch
            {
                // Prevent crash loop if writing to log file fails
            }
        }

        private static string GetFriendlyMessage(Exception ex, string context)
        {
            // 1. KIỂM TRA CHÍNH XÁC MÃ LỖI TRẢ VỀ TỪ POSTGRESQL (NPGSQL) TRƯỚC
            if (ex is Npgsql.PostgresException pgEx)
            {
                switch (pgEx.SqlState)
                {
                    case "23503": // Mã lỗi vi phạm Khóa Ngoại (Foreign Key Violation)
                        return "MÃ THẺ CHƯA ĐƯỢC ĐĂNG KÝ TRÊN HỆ THỐNG!\n\n" +
                               "• Nguyên nhân: Thẻ RFID vật lý này chưa từng được khai báo trong danh mục thẻ hoặc dữ liệu thành viên.\n" +
                               "• Hướng xử lý: Vui lòng chuyển sang tài khoản Admin, vào mục 'Quản Trị Dữ Liệu' -> chọn bảng 'RFIDCards' để đăng ký kích hoạt thẻ trước khi quẹt vào bãi.";

                    case "23505": // Mã lỗi trùng khóa chính (Unique/Primary Key Violation)
                        return "DỮ LIỆU BỊ TRÙNG LẶP HỆ THỐNG!\n\n" +
                               "• Nguyên nhân: Mã số định danh hoặc lượt xe này đã tồn tại trong cơ sở dữ liệu.\n" +
                               "• Hướng xử lý: Kiểm tra lại xem thẻ này có đang giải quyết lượt ra chưa dứt điểm hay không.";

                    case "28P01": // Sai thông tin đăng nhập database
                        return "MẬT KHẨU HOẶC TÀI KHOẢN DATABASE KHÔNG ĐÚNG!\n\nVui lòng kiểm tra lại cấu hình thông số SQL_USER / SQL_PASSWORD trong tệp cấu hình '.env'.";

                    case "3D000": // Sai tên database
                        return "CƠ SỞ DỮ LIỆU KHÔNG TỒN TẠI!\n\nVui lòng kiểm tra lại biến 'SQL_DATABASE' trong file '.env' xem có khớp với PostgreSQL server không.";
                }
            }

            string exStr = ex.ToString().ToLower();

            // 2. CHỈ KIỂM TRA LỖI KẾT NỐI MẠNG / DROP SERVER THỰC SỰ
            if (exStr.Contains("timeout") || exStr.Contains("socket") || exStr.Contains("failed to connect") ||
                exStr.Contains("network") || exStr.Contains("endpoint") || exStr.Contains("connection_failure"))
            {
                return "MẤT KẾT NỐI MÁY CHỦ CƠ SỞ DỮ LIỆU (POSTGRESQL)!\n\nVui lòng kiểm tra dây cáp mạng LAN, trạng thái container Docker hoặc đảm bảo dịch vụ PostgreSQL đang được bật.";
            }

            // 3. Kiểm tra các lỗi định dạng hoặc rỗng khác
            if (ex is ArgumentException || ex is FormatException || exStr.Contains("format") ||
                exStr.Contains("invalid input") || exStr.Contains("nullreference"))
            {
                return "DỮ LIỆU ĐẦU VÀO KHÔNG HỢP LỆ!\n\nVui lòng kiểm tra lại định dạng ký tự gõ hoặc các trường có dấu sao bắt buộc (*).";
            }

            // 4. Kiểm tra lỗi phần cứng đầu đọc RFID
            if (exStr.Contains("serialport") || exStr.Contains("port") || exStr.Contains("ioexception") || exStr.Contains("com"))
            {
                return "LỖI ĐẦU ĐỌC THẺ RFID HARDWARE!\n\nVui lòng kiểm tra lại dây cắm USB thiết bị, hoặc cấu hình lại cổng COM trên bảng giả lập.";
            }

            // Mặc định dự phòng nếu gặp lỗi lạ khác
            if (!string.IsNullOrEmpty(context))
            {
                return $"LỖI THỰC THI NGHIỆP VỤ: {context.ToUpper()}.\nVui lòng liên hệ quản trị viên để kiểm tra.";
            }

            return "HỆ THỐNG GẶP LỖI KHÔNG MONG MUỐN.\nVui lòng mở tệp nhật ký 'error.log' trong thư mục để xem chi tiết.";
        }

        private static void ShowCustomDialog(string message, string title, bool isCritical, string details = "")
        {
            try
            {
                // Ensure executing on the UI thread
                if (Application.OpenForms.Count > 0)
                {
                    Form? mainForm = Application.OpenForms[0];
                    if (mainForm != null && mainForm.InvokeRequired)
                    {
                        mainForm.Invoke(new Action(() => ShowDialogInternal(message, title, isCritical, details)));
                    }
                    else
                    {
                        ShowDialogInternal(message, title, isCritical, details);
                    }
                }
                else
                {
                    ShowDialogInternal(message, title, isCritical, details);
                }
            }
            catch
            {
                // Fallback to standard message box if custom form fails
                MessageBox.Show(message, title, MessageBoxButtons.OK, isCritical ? MessageBoxIcon.Error : MessageBoxIcon.Warning);
            }
        }

        private static void ShowDialogInternal(string message, string title, bool isCritical, string details)
        {
            using (var form = new FriendlyErrorForm(message, title, isCritical, details))
            {
                form.ShowDialog();
            }
        }
    }

    public class FriendlyErrorForm : Form
    {
        private Panel? pnlDetails;
        private LinkLabel? lnkToggle;

        // GIẢI PHÁP: Xác định kích thước CLIENT (lòng trong) cố định để không bị thanh tiêu đề Windows ăn bớt pixel
        private int compactClientHeight = 220;  // Chiều cao lòng trong khi thu gọn (65 Header + 90 Message + 65 Footer)
        private int expandedClientHeight = 440; // Chiều cao lòng trong khi mở rộng (+ 220 Details)

        public FriendlyErrorForm(string message, string title, bool isCritical, string details)
        {
            this.Text = title;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Width = 620;
            this.BackColor = Color.FromArgb(248, 250, 252); // Slate 50

            Color accentColor = isCritical ? Color.FromArgb(220, 38, 38) : Color.FromArgb(217, 119, 6); // Đỏ vs Vàng Hổ Phách
            Color darkSlate = Color.FromArgb(15, 23, 42); // Slate 900
            Color textGray = Color.FromArgb(71, 85, 105); // Slate 600

            // Header banner
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 65,
                BackColor = accentColor
            };

            Label lblTitle = new Label
            {
                Text = isCritical ? "⚠️ LỖI HỆ THỐNG NGHIÊM TRỌNG" : "⚠️ CẢNH BÁO VẬN HÀNH",
                Font = new Font("Segoe UI", 13.5F, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            pnlHeader.Controls.Add(lblTitle);

            // Message Panel (Khung chứa nội dung thông báo)
            Panel pnlMessage = new Panel
            {
                Dock = DockStyle.Top, // Đổi sang Dock Top để xếp chồng ngay ngắn
                Height = 95,          // Đặt chiều cao hợp lý
                Padding = new Padding(20, 10, 20, 2), // Thu hẹp padding để nhường không gian cho chữ wrap dòng
                BackColor = Color.FromArgb(248, 250, 252)
            };

            Label lblMessage = new Label
            {
                Text = message,
                Font = new Font("Segoe UI", 11.5F, FontStyle.Bold), // Hạ xuống 11.5F giúp hiển thị trọn vẹn không bị xén chữ
                ForeColor = darkSlate,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            pnlMessage.Controls.Add(lblMessage);

            // Footer Button Panel (Chứa nút Đồng ý)
            Panel pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 65,
                BackColor = Color.FromArgb(241, 245, 249), // Slate 100
                Padding = new Padding(0, 10, 0, 10)
            };

            Button btnOk = new Button
            {
                Text = "ĐỒNG Ý",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Width = 140,
                Height = 42,
                BackColor = accentColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Left = (this.Width - btnOk.Width) / 2;
            btnOk.Top = 11;
            btnOk.Click += (s, e) => this.Close();
            pnlFooter.Controls.Add(btnOk);

            // Đưa các bảng điều khiển cơ bản vào Form
            this.Controls.Add(pnlMessage);
            this.Controls.Add(pnlHeader);
            this.Controls.Add(pnlFooter);

            // Kiểm tra cấu hình xem chi tiết kỹ thuật
            if (isCritical && !string.IsNullOrEmpty(details))
            {
                // Thêm liên kết mở rộng vào đáy vùng thông báo
                lnkToggle = new LinkLabel
                {
                    Text = "Chi tiết lỗi ▼",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                    LinkColor = Color.FromArgb(100, 116, 139), // Slate 500
                    ActiveLinkColor = accentColor,
                    VisitedLinkColor = Color.FromArgb(100, 116, 139),
                    LinkBehavior = LinkBehavior.HoverUnderline,
                    Dock = DockStyle.Bottom,
                    Height = 22,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                lnkToggle.LinkClicked += LnkToggle_LinkClicked;
                pnlMessage.Controls.Add(lnkToggle);

                // Khởi tạo vùng hiển thị console mã lỗi
                pnlDetails = new Panel
                {
                    Dock = DockStyle.Bottom,
                    Height = 220,
                    Padding = new Padding(20, 5, 20, 10),
                    Visible = false
                };

                Label lblDetailsHeader = new Label
                {
                    Text = "Chi tiết kỹ thuật (Dành cho Quản trị viên):",
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic),
                    ForeColor = textGray,
                    Height = 25,
                    Dock = DockStyle.Top
                };

                TextBox txtDetails = new TextBox
                {
                    Multiline = true,
                    ReadOnly = true,
                    ScrollBars = ScrollBars.Vertical,
                    Text = details,
                    Font = new Font("Consolas", 9F),
                    BackColor = Color.FromArgb(15, 23, 42), // Bảng đen console
                    ForeColor = Color.FromArgb(74, 222, 128), // Chữ xanh lá cây
                    BorderStyle = BorderStyle.None,
                    Dock = DockStyle.Fill
                };

                pnlDetails.Controls.Add(txtDetails);
                pnlDetails.Controls.Add(lblDetailsHeader);
                this.Controls.Add(pnlDetails);

                // ÁP DỤNG KÍCH THƯỚC CLIENT BAN ĐẦU
                this.ClientSize = new Size(620, compactClientHeight);
            }
            else
            {
                this.ClientSize = new Size(620, compactClientHeight - 20); // Dành cho Warning cảnh báo không có details
            }

            // Sắp xếp phân lớp Z-Order chuẩn xác để tránh xung đột Docking
            pnlHeader.BringToFront();
            pnlMessage.BringToFront();
            pnlFooter.SendToBack();
            if (pnlDetails != null)
            {
                pnlDetails.SendToBack();
            }
        }

        private void LnkToggle_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            if (pnlDetails == null || lnkToggle == null) return;

            int oldHeight = this.Height;
            bool isCurrentlyVisible = pnlDetails.Visible;

            if (isCurrentlyVisible)
            {
                // Thu gọn bảng Console kỹ thuật lại
                pnlDetails.Visible = false;
                lnkToggle.Text = "Chi tiết lỗi ▼";
                this.ClientSize = new Size(620, compactClientHeight);
            }
            else
            {
                // Mở rộng bảng Console kỹ thuật ra
                pnlDetails.Visible = true;
                lnkToggle.Text = "Ẩn chi tiết ▲";
                this.ClientSize = new Size(620, expandedClientHeight);
            }

            // Đồng bộ căn giữa lại vị trí Form trên màn hình sau khi co giãn chiều cao
            this.Top -= (this.Height - oldHeight) / 2;
        }
    }
}
