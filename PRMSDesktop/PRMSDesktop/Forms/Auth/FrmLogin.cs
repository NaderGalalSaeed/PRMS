using PRMSDesktop.Forms.Properties;
using PRMSDesktop.Helpers;
using PRMSDesktop.Models.Auth;
using PRMSDesktop.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRMSDesktop.Forms.Auth
{
    public partial class FrmLogin : Form
    {
        private readonly AuthApiService _authApiService;

        public FrmLogin()
        {
            InitializeComponent();
            _authApiService = new AuthApiService();

        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            await LoginAsync();
        }

        private async Task LoginAsync()
        {
            lblError.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                lblError.Text = "Email is required";
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = "Password is required";
                txtPassword.Focus();
                return;
            }
            
            ToggleInputs(false);

            try
            {
                var request = new LoginRequest
                {
                    Email = txtEmail.Text.Trim(),
                    Password = txtPassword.Text
                };

                var result = await _authApiService.LoginAsync(request);

                if (result == null || string.IsNullOrWhiteSpace(result.Token))
                {
                    lblError.Text = "Login details are incorrect or unable to connect to the server";
                    return;
                }

                SessionManager.SetSession(result.Token, result.ExpiresIn);
                ApiClient.ApplyAuthorizationHeader();

                Hide();

                var frmPropertiesList = new FrmPropertiesList();
                frmPropertiesList.FormClosed += (s, e) => Close();
                frmPropertiesList.Show();
            }
            catch (Exception ex)
            {
                lblError.Text = $"Unkown error: {ex.Message}";
                //lblError.Text = $"Unkown error";
            }
            finally
            {
                ToggleInputs(true);
            }
        }

        private void ToggleInputs(bool enabled)
        {
            txtEmail.Enabled = enabled;
            txtPassword.Enabled = enabled;
            chkShowPassword.Enabled = enabled;
            btnLogin.Enabled = enabled;
            btnExit.Enabled = enabled;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
