using System;
using System.Windows.Forms;

namespace SmartParking
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Bảo đảm DB đã được khởi tạo khi app khởi chạy
            DatabaseHelper.InitializeDatabase();
            txtUsername.Focus();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra thông tin đăng nhập từ SQLite
            string? role = DatabaseHelper.ValidateUser(username, password);

            if (role != null)
            {
                // Đăng nhập thành công
                txtPassword.Clear();
                this.Hide();

                if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                {
                    AdminForm adminForm = new AdminForm(this);
                    adminForm.Show();
                }
                else if (role.Equals("Guard", StringComparison.OrdinalIgnoreCase))
                {
                    GuardForm guardForm = new GuardForm(this);
                    guardForm.Show();
                }
            }
            else
            {
                // Sai thông tin đăng nhập
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLogin_Click(this, EventArgs.Empty);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
