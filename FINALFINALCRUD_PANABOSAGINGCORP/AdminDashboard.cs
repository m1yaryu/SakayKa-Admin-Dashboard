using System;
using System.Data;
using System.Windows.Forms;

namespace FINALFINALCRUD_PANABOSAGINGCORP
{
    public partial class AdminDashboard : Form
    {
        Connect db = new Connect();

        public AdminDashboard()
        {
            InitializeComponent();
        }

        

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadAllVehicles();
            LoadFareRates();
        }

        private void BaseFareTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void PerKmTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void WholeRideTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void OutOfRouteTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void DifferentialTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void DiscountTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void SetBtn_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = db.Open())
                {
                    conn.Open();
                    string query = @"UPDATE fare_rates SET
                             base_fare=@base,
                             pkm_fare=@pkm,
                             wholeride_fare=@whole,
                             OFR_fare=@ofr,
                             holiday_fare=@holiday,
                             discounted_fare=@discount
                             LIMIT 1";

                    var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@base", BaseFareTxt.Text);
                    cmd.Parameters.AddWithValue("@pkm", PerKmTxt.Text);
                    cmd.Parameters.AddWithValue("@whole", WholeRideTxt.Text);
                    cmd.Parameters.AddWithValue("@ofr", OutOfRouteTxt.Text);
                    cmd.Parameters.AddWithValue("@holiday", DifferentialTxt.Text);
                    cmd.Parameters.AddWithValue("@discount", DiscountTxt.Text);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Fare rates updated successfully!");
                SetTextBoxesReadOnly(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating fare rates: " + ex.Message);
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            string plate = PlateNoTxt.Text.Trim();
            string cr = CRNoTxt.Text.Trim();
            string license = LicenseTxt.Text.Trim();

            DataTable dt = db.SearchVehicles(plate, cr, license);

            if (dt.Rows.Count > 0)
            {
                dgvRegistered.DataSource = dt;
            }
            else
            {
                dgvRegistered.DataSource = null;
                MessageBox.Show("No matching records found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Clear the textboxes after search
            PlateNoTxt.Clear();
            CRNoTxt.Clear();
            LicenseTxt.Clear();
        }

        private void LoadAllVehicles()
        {
            DataTable dt = db.SearchVehicles(); // call with no parameters to get all records
            dgvRegistered.DataSource = dt;

            // Optional: make columns stretch nicely
            dgvRegistered.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            LoadAllVehicles();
        }

        // Load fare rates from database into textboxes
        private void LoadFareRates()
        {
            try
            {
                using (var conn = db.Open()) // db.Open() already opens it
                {
                    string query = "SELECT * FROM fare_rates LIMIT 1"; // assuming 1 row
                    var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conn);
                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        BaseFareTxt.Text = reader["base_fare"].ToString();
                        PerKmTxt.Text = reader["pkm_fare"].ToString();
                        WholeRideTxt.Text = reader["wholeride_fare"].ToString();
                        OutOfRouteTxt.Text = reader["OFR_fare"].ToString();
                        DifferentialTxt.Text = reader["holiday_fare"].ToString();
                        DiscountTxt.Text = reader["discounted_fare"].ToString();
                    }

                    SetTextBoxesReadOnly(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading fare rates: " + ex.Message);
            }
        }


        // Set textboxes read-only or editable
        private void SetTextBoxesReadOnly(bool readOnly)
        {
            try
            {
                // Convert textboxes to decimal safely
                decimal baseFare = decimal.Parse(BaseFareTxt.Text);
                decimal perKmFare = decimal.Parse(PerKmTxt.Text);
                decimal wholeRideFare = decimal.Parse(WholeRideTxt.Text);
                decimal outOfRouteFare = decimal.Parse(OutOfRouteTxt.Text);
                decimal holidayFare = decimal.Parse(DifferentialTxt.Text);
                decimal discountedFare = decimal.Parse(DiscountTxt.Text);

                // Use the Connect class method
                db.UpdateFareRates(baseFare, perKmFare, wholeRideFare, outOfRouteFare, holidayFare, discountedFare);

                MessageBox.Show("Fare rates updated successfully!");
                SetTextBoxesReadOnly(true);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for all fare rates.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating fare rates: " + ex.Message);
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            SetTextBoxesReadOnly(false);
        }
    }
}
