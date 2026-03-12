namespace PRMSDesktop.Forms.Auth
{
    partial class FrmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtEmail = new TextBox();
            txtPassword = new TextBox();
            lnlEmail = new Label();
            lblPassword = new Label();
            btnLogin = new Button();
            btnExit = new Button();
            chkShowPassword = new CheckBox();
            lblError = new Label();
            SuspendLayout();
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(98, 65);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(197, 27);
            txtEmail.TabIndex = 0;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(98, 122);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(197, 27);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lnlEmail
            // 
            lnlEmail.AutoSize = true;
            lnlEmail.Location = new Point(26, 72);
            lnlEmail.Name = "lnlEmail";
            lnlEmail.Size = new Size(46, 20);
            lnlEmail.TabIndex = 2;
            lnlEmail.Text = "Email";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(13, 129);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(73, 20);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Passwoed";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(98, 197);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(94, 29);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(201, 197);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(94, 29);
            btnExit.TabIndex = 5;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Location = new Point(98, 152);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(132, 24);
            chkShowPassword.TabIndex = 6;
            chkShowPassword.Text = "Show Password";
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Location = new Point(12, 244);
            lblError.Name = "lblError";
            lblError.Size = new Size(41, 20);
            lblError.TabIndex = 7;
            lblError.Text = "Error";
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(353, 488);
            Controls.Add(lblError);
            Controls.Add(chkShowPassword);
            Controls.Add(btnExit);
            Controls.Add(btnLogin);
            Controls.Add(lblPassword);
            Controls.Add(lnlEmail);
            Controls.Add(txtPassword);
            Controls.Add(txtEmail);
            MaximizeBox = false;
            Name = "FrmLogin";
            Text = "Login";
            Load += FrmLogin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtEmail;
        private TextBox txtPassword;
        private Label lnlEmail;
        private Label lblPassword;
        private Button btnLogin;
        private Button btnExit;
        private CheckBox chkShowPassword;
        private Label lblError;
    }
}