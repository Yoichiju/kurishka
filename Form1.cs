using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WinFormsApp5;

namespace WinFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            using (var brush = new LinearGradientBrush(
                this.ClientRectangle,
                System.Drawing.Color.FromArgb(30, 58, 138),
                System.Drawing.Color.FromArgb(59, 130, 246),
                90f))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtLogin.Text == "admin" && txtPassword.Text == "admin")
            {
                var mainForm = new MainFormcs();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка авторизации",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus();
            }
        }
    }
}
