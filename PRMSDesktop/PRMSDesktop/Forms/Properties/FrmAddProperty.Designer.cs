namespace PRMSDesktop.Forms.Properties
{
    partial class FrmAddProperty
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
            lblPropertyName = new Label();
            txtPropertyName = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblCity = new Label();
            txtCity = new TextBox();
            lblMonthlyPrice = new Label();
            txtMonthlyPrice = new TextBox();
            btnCancel = new Button();
            btnSave = new Button();
            chkMarkAsAvailable = new CheckBox();
            lblError = new Label();
            SuspendLayout();
            // 
            // lblPropertyName
            // 
            lblPropertyName.AutoSize = true;
            lblPropertyName.Location = new Point(101, 9);
            lblPropertyName.Name = "lblPropertyName";
            lblPropertyName.Size = new Size(119, 20);
            lblPropertyName.TabIndex = 4;
            lblPropertyName.Text = "Property Name *";
            // 
            // txtPropertyName
            // 
            txtPropertyName.Location = new Point(101, 32);
            txtPropertyName.Name = "txtPropertyName";
            txtPropertyName.PlaceholderText = "e.g. Villa Taiz";
            txtPropertyName.Size = new Size(197, 27);
            txtPropertyName.TabIndex = 3;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(101, 70);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(72, 20);
            lblAddress.TabIndex = 6;
            lblAddress.Text = "Address *";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(101, 93);
            txtAddress.Name = "txtAddress";
            txtAddress.PlaceholderText = "e.g. Gamal St.";
            txtAddress.Size = new Size(197, 27);
            txtAddress.TabIndex = 5;
            // 
            // lblCity
            // 
            lblCity.AutoSize = true;
            lblCity.Location = new Point(101, 130);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(44, 20);
            lblCity.TabIndex = 8;
            lblCity.Text = "City *";
            // 
            // txtCity
            // 
            txtCity.Location = new Point(101, 153);
            txtCity.Name = "txtCity";
            txtCity.PlaceholderText = "e.g. Taiz";
            txtCity.Size = new Size(197, 27);
            txtCity.TabIndex = 7;
            // 
            // lblMonthlyPrice
            // 
            lblMonthlyPrice.AutoSize = true;
            lblMonthlyPrice.Location = new Point(101, 195);
            lblMonthlyPrice.Name = "lblMonthlyPrice";
            lblMonthlyPrice.Size = new Size(109, 20);
            lblMonthlyPrice.TabIndex = 10;
            lblMonthlyPrice.Text = "Monthly Price *";
            // 
            // txtMonthlyPrice
            // 
            txtMonthlyPrice.Location = new Point(101, 218);
            txtMonthlyPrice.Name = "txtMonthlyPrice";
            txtMonthlyPrice.PlaceholderText = "e.g. 500.00";
            txtMonthlyPrice.Size = new Size(197, 27);
            txtMonthlyPrice.TabIndex = 9;
            txtMonthlyPrice.KeyPress += txtMonthlyPrice_KeyPress;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(204, 292);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(101, 292);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // chkMarkAsAvailable
            // 
            chkMarkAsAvailable.AutoSize = true;
            chkMarkAsAvailable.Location = new Point(101, 256);
            chkMarkAsAvailable.Name = "chkMarkAsAvailable";
            chkMarkAsAvailable.Size = new Size(150, 24);
            chkMarkAsAvailable.TabIndex = 13;
            chkMarkAsAvailable.Text = "Mark As Available";
            chkMarkAsAvailable.UseVisualStyleBackColor = true;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Location = new Point(101, 349);
            lblError.Name = "lblError";
            lblError.Size = new Size(41, 20);
            lblError.TabIndex = 14;
            lblError.Text = "Error";
            // 
            // FrmAddProperty
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblError);
            Controls.Add(chkMarkAsAvailable);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(lblMonthlyPrice);
            Controls.Add(txtMonthlyPrice);
            Controls.Add(lblCity);
            Controls.Add(txtCity);
            Controls.Add(lblAddress);
            Controls.Add(txtAddress);
            Controls.Add(lblPropertyName);
            Controls.Add(txtPropertyName);
            MaximizeBox = false;
            Name = "FrmAddProperty";
            Text = "Property Rental - Add Property";
            Load += FrmAddProperty_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPropertyName;
        private TextBox txtPropertyName;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblCity;
        private TextBox txtCity;
        private Label lblMonthlyPrice;
        private TextBox txtMonthlyPrice;
        private Button btnCancel;
        private Button btnSave;
        private CheckBox chkMarkAsAvailable;
        private Label lblError;
    }
}