using PRMSDesktop.Helpers;
using PRMSDesktop.Models.Properties;
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
using System.Xml.Linq;

namespace PRMSDesktop.Forms.Properties
{
    public partial class FrmAddProperty : Form
    {
        private readonly PropertyApiService _propertyApiService;
        private readonly PropertyViewModel? _property;
        private readonly bool _isEditMode;

        public FrmAddProperty()
        {
            InitializeComponent();
            _propertyApiService = new PropertyApiService();
            _isEditMode = false;
        }

        public FrmAddProperty(PropertyViewModel property)
        {
            InitializeComponent();
            _propertyApiService = new PropertyApiService();
            _property = property;
            _isEditMode = true;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (!SessionManager.IsLoggedIn)
            {
                MessageBox.Show("Session ended, please login again", "Unauthorized",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Close();
            }
        }

        private void FrmAddProperty_Load(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;

            if (_isEditMode && _property != null)
            {
                Text = "Property Rental - Edit Property";

                txtPropertyName.Text = _property.Name;
                txtAddress.Text = _property.Address;
                txtCity.Text = _property.City;
                txtMonthlyPrice.Text = _property.MonthlyPrice.ToString("0.##");
                chkMarkAsAvailable.Checked = _property.IsAvailable;
                btnSave.Text = "Update Property";
            }
            else
            {
                Text = "Property Rental - Add Property";
                chkMarkAsAvailable.Checked = true;
                btnSave.Text = "Save Property";
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            await SaveAsync();
        }

        private async Task SaveAsync()
        {
            lblError.Text = string.Empty;

            if (!ValidateForm())
                return;

            ToggleInputs(false);

            try
            {
                decimal price = decimal.Parse(txtMonthlyPrice.Text.Trim());

                var request = new SavePropertyRequest
                {
                    Id = _property.Id,
                    Name = txtPropertyName.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    City = txtCity.Text.Trim(),
                    MonthlyPrice = price,
                    IsAvailable = chkMarkAsAvailable.Checked
                };

                if (_isEditMode && _property != null)
                    await _propertyApiService.UpdateAsync(_property.Id, request);
                else
                    await _propertyApiService.CreateAsync(request);

                MessageBox.Show(_isEditMode ? "Updateed Sucessfully" : "Added Sucessfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Session ended, please login again", "Unauthorized",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                SessionManager.Clear();
                ApiClient.ClearAuthorizationHeader();
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            finally
            {
                ToggleInputs(true);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtPropertyName.Text))
            {
                lblError.Text = "Property Name is required";
                txtPropertyName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                lblError.Text = "Address is required";
                txtAddress.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCity.Text))
            {
                lblError.Text = "City is required";
                txtCity.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMonthlyPrice.Text))
            {
                lblError.Text = "Monthly Price is required";
                txtMonthlyPrice.Focus();
                return false;
            }

            if (!decimal.TryParse(txtMonthlyPrice.Text.Trim(), out decimal price))
            {
                lblError.Text = "Monthly Price must be numeric";
                txtMonthlyPrice.Focus();
                return false;
            }

            if (price <= 0)
            {
                lblError.Text = "Monthly Price must be larger than zero";
                txtMonthlyPrice.Focus();
                return false;
            }

            return true;
        }

        private void ToggleInputs(bool enabled)
        {
            txtPropertyName.Enabled = enabled;
            txtAddress.Enabled = enabled;
            txtCity.Enabled = enabled;
            txtMonthlyPrice.Enabled = enabled;
            chkMarkAsAvailable.Enabled = enabled;
            btnSave.Enabled = enabled;
            btnCancel.Enabled = enabled;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtMonthlyPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && txtMonthlyPrice.Text.Contains("."))
            {
                e.Handled = true;
            }
        }
    }
}
