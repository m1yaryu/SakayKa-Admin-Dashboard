using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace FINALFINALCRUD_PANABOSAGINGCORP
{
    internal class Connect
    {
        private readonly string server = "127.0.0.1";
        private readonly string database = "panabosaging";
        private readonly string username = "root";
        private readonly string password = "";

        private MySqlConnection conn;

        public Connect()
        {
            string connString = $"Server={server};Database={database};User ID={username};Password={password};";
            conn = new MySqlConnection(connString);
        }

        public MySqlConnection Open()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            return conn;
        }

        public void Close()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        public bool LoginPlain(string username, string password)
        {
            bool valid = false;
            try
            {
                string query = "SELECT COUNT(*) FROM login WHERE username=@username AND password=@password";
                using (MySqlCommand cmd = new MySqlCommand(query, Open()))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    valid = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            finally { Close(); }
            return valid;
        }

        public DataTable SearchVehicles(string plateNo = null, string crNo = null, string licenseNo = null)
        {
            string query = "SELECT plateNo, bodyNo, driverName, licenseNo, crNo, vehicleModel, status, permitExpiry FROM vehicle_drivers WHERE 1=1";
            var parameters = new List<MySqlParameter>();

            if (!string.IsNullOrEmpty(plateNo))
            {
                query += " AND plateNo LIKE @plateNo";
                parameters.Add(new MySqlParameter("@plateNo", "%" + plateNo + "%"));
            }
            if (!string.IsNullOrEmpty(crNo))
            {
                query += " AND crNo LIKE @crNo";
                parameters.Add(new MySqlParameter("@crNo", "%" + crNo + "%"));
            }
            if (!string.IsNullOrEmpty(licenseNo))
            {
                query += " AND licenseNo LIKE @licenseNo";
                parameters.Add(new MySqlParameter("@licenseNo", "%" + licenseNo + "%"));
            }

            return ExecuteQueryWithParams(query, parameters.ToArray());
        }


        public DataTable ExecuteQueryWithParams(string query, MySqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, Open()))
                {
                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            finally { Close(); }
            return dt;
        }

        public DataRow GetFareRates()
        {
            string query = "SELECT * FROM fare_rates LIMIT 1";
            DataTable dt = ExecuteQueryWithParams(query, null);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        public void UpdateFareRates(decimal baseFare, decimal perKmFare, decimal wholeRideFare, decimal outOfRouteFare, decimal holidayFare, decimal discountedFare)
        {
            string query = @"UPDATE fare_rates SET
                                base_fare = @baseFare,
                                pkm_fare = @perKmFare,
                                wholeride_fare = @wholeRideFare,
                                OFR_fare = @outOfRouteFare,
                                holiday_fare = @holidayFare,
                                discounted_fare = @discountedFare
                             LIMIT 1";

            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@baseFare", baseFare),
                new MySqlParameter("@perKmFare", perKmFare),
                new MySqlParameter("@wholeRideFare", wholeRideFare),
                new MySqlParameter("@outOfRouteFare", outOfRouteFare),
                new MySqlParameter("@holidayFare", holidayFare),
                new MySqlParameter("@discountedFare", discountedFare)
            };

            ExecuteNonQuery(query, parameters);
        }

        public void ExecuteNonQuery(string query, MySqlParameter[] parameters)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand(query, Open()))
                {
                    if (parameters != null && parameters.Length > 0)
                        cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();
                }
            }
            finally { Close(); }
        }
    }
}
