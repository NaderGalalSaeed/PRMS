using PRMSDesktop.Forms.Auth;
using PRMSDesktop.Helpers;
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

namespace PRMSDesktop.Forms.Properties
{
    public partial class FrmPropertiesList : Form
    {
        private readonly PropertyApiService _propertyApiService;

        public FrmPropertiesList()
        {
            InitializeComponent();
            _propertyApiService = new PropertyApiService();
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

        private async void FrmPropertiesList_Load(object sender, EventArgs e)
        {
            ConfigureGrid();
            await LoadPropertiesAsync();
        }

        private void ConfigureGrid()
        {
            dgvProperties.AutoGenerateColumns = false;
            dgvProperties.AllowUserToAddRows = false;
            dgvProperties.AllowUserToDeleteRows = false;
            dgvProperties.ReadOnly = true;
            dgvProperties.MultiSelect = false;
            dgvProperties.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProperties.RowHeadersVisible = false;
            dgvProperties.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvProperties.Columns.Count > 0)
                return;

            dgvProperties.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "ID",
                DataPropertyName = "Id",
                FillWeight = 50
            });

            dgvProperties.Columns["colId"].Visible = false;

            dgvProperties.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colName",
                HeaderText = "Name",
                DataPropertyName = "Name",
                FillWeight = 130
            });

            dgvProperties.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAddress",
                HeaderText = "Address",
                DataPropertyName = "Address",
                FillWeight = 180
            });

            dgvProperties.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCity",
                HeaderText = "City",
                DataPropertyName = "City",
                FillWeight = 110
            });

            dgvProperties.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMonthlyPrice",
                HeaderText = "$/mo",
                DataPropertyName = "MonthlyPrice",
                FillWeight = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" }
            });

            dgvProperties.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "colIsAvailable",
                HeaderText = "Avail",
                DataPropertyName = "IsAvailable",
                FillWeight = 60
            });
        }

        private async Task LoadPropertiesAsync()
        {
            try
            {
                ToggleButtons(false);
                lblStatus.Text = "Loading properties...";

                var properties = await _propertyApiService.GetAllAsync();
                dgvProperties.DataSource = null;
                dgvProperties.DataSource = properties;

                lblStatus.Text = $"{properties.Count} record(s)";
            }
            catch (UnauthorizedAccessException)
            {
                HandleUnauthorized();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Faild to load data.\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                lblStatus.Text = "Load failed";
            }
            finally
            {
                ToggleButtons(true);
            }
        }

        private long? GetSelectedPropertyId()
        {
            if (dgvProperties.CurrentRow == null)
                return null;

            var value = dgvProperties.CurrentRow.Cells["colId"].Value;
            if (value == null)
                return null;

            return Convert.ToInt64(value);
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using var frm = new FrmAddProperty();

            if (frm.ShowDialog() == DialogResult.OK)
                await LoadPropertiesAsync();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            var selectedId = GetSelectedPropertyId();
            if (selectedId == null)
            {
                MessageBox.Show("Please select a property", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var property = await _propertyApiService.GetByIdAsync(selectedId.Value);
                if (property == null)
                {
                    MessageBox.Show("Faild to find property data", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var frm = new FrmAddProperty(property);
                if (frm.ShowDialog() == DialogResult.OK)
                    await LoadPropertiesAsync();
            }
            catch (UnauthorizedAccessException)
            {
                HandleUnauthorized();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Faild to open Edit Screen\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadPropertiesAsync();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedId = GetSelectedPropertyId();
            if (selectedId == null)
            {
                MessageBox.Show("Please select a property", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Do you want to delete the property", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                await _propertyApiService.DeleteAsync(selectedId.Value);

                MessageBox.Show("Property deleted sucessfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadPropertiesAsync();
            }
            catch (UnauthorizedAccessException)
            {
                HandleUnauthorized();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falid delete data\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            SessionManager.Clear();
            ApiClient.ClearAuthorizationHeader();

            Hide();

            var loginForm = new FrmLogin();
            loginForm.FormClosed += (s, args) => Close();
            loginForm.Show();
        }

        private void ToggleButtons(bool enabled)
        {
            btnAdd.Enabled = enabled;
            btnEdit.Enabled = enabled;
            btnDelete.Enabled = enabled;
            btnRefresh.Enabled = enabled;
            btnLogOut.Enabled = enabled;
        }

        private void HandleUnauthorized()
        {
            MessageBox.Show("Session ended, please login again", "Unauthorized",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

            SessionManager.Clear();
            ApiClient.ClearAuthorizationHeader();

            Hide();

            var loginForm = new FrmLogin();
            loginForm.FormClosed += (s, args) => Close();
            loginForm.Show();
        }
    }
}
