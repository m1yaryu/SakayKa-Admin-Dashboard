using System;
using System.Windows.Forms;

namespace FINALFINALCRUD_PANABOSAGINGCORP
{
    public partial class SignIn : Form
    {
        Connect db = new Connect();

        public SignIn()
        {
            InitializeComponent();
            PwdTxt.UseSystemPasswordChar = true; // Hide password characters
        }

        // ✅ Fix: Added this event handler so the designer can process it
        private void SignIn_Load(object sender, EventArgs e)
        {
            // Optional: any startup code when form loads
            UsrTxt.Clear();
            PwdTxt.Clear();
        }

        private void SignUpBtn_Click(object sender, EventArgs e)
        {
            string username = UsrTxt.Text.Trim();
            string password = PwdTxt.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Missing Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                db.Open();
                db.Close();

                if (db.LoginPlain(username, password))
                {
                    MessageBox.Show("Login Successful!\nDatabase connected!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AdminDashboard dash = new AdminDashboard();
                    dash.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection failed: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
