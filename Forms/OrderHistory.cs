using Car_Rental_Management_System.Data;
using Car_Rental_Management_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Car_Rental_Management_System.Forms
{
    public partial class OrderHistory : Form
    {
        private readonly string rentalsPath = Path.Combine(Application.StartupPath, "Data", "rentals.json");




        public OrderHistory()
        {
            InitializeComponent();
        }

        private void OrderHistory_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(rentalsPath))
                {
                    // Open Explorer and highlight the rentals.json file
                    System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{rentalsPath}\"");
                }
                else
                {
                    MessageBox.Show($"File not found: {rentalsPath}", "File Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open file location: " + ex.Message);
            }
        }



        private void LoadOrders()
        {
            try
            {
                string filePath = Path.Combine(Application.StartupPath, "Data", "rentals.json");
                var repo = new Repository<Rental>(filePath);
                var rentals = repo.Load();

                if (rentals == null || rentals.Count == 0)
                {
                    dgvOrders.DataSource = null;
                    lblSummary.Text = "No rentals found.";
                    return;
                }

                // ✅ Flatten nested objects for DataGridView
                var displayList = rentals.Select(r => new
                {
                    r.RentalId,
                    r.CustomerName,
                    Brand = r.RentedCar?.Brand ?? "N/A",
                    Model = r.RentedCar?.Model ?? "N/A",
                    r.StartDate,
                    r.EndDate,
                    r.TotalPrice
                })
                .OrderByDescending(r => r.StartDate)
                .ToList();

                dgvOrders.DataSource = displayList;

                var totalRevenue = displayList.Sum(r => r.TotalPrice);
                lblSummary.Text = $"Loaded {displayList.Count:N0} orders | Total revenue: ${totalRevenue:N2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading rentals: " + ex.Message);
            }
        }



        private static List<OrderRow> ReadFile(string path)
        {
            var list = new List<OrderRow>();

            foreach (var line in File.ReadLines(path))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                // Format written by RentalForm:
                // RentalId,CustomerName,Brand,Model,StartDate,EndDate,TotalPrice
                var parts = line.Split(',');
                if (parts.Length < 7) continue;

                string S(int i) => i < parts.Length ? parts[i].Trim() : string.Empty;

                list.Add(new OrderRow
                {
                    RentalId = TryParseInt(S(0)),
                    CustomerName = S(1),
                    Brand = S(2),
                    Model = S(3),
                    StartDate = TryParseDateTime(S(4)),
                    EndDate = TryParseDateTime(S(5)),
                    TotalPrice = TryParseDecimal(S(6))
                });
            }

            return list.OrderByDescending(r => r.StartDate).ToList();
        }

        private static int TryParseInt(string s)
            => int.TryParse(s, NumberStyles.Integer, CultureInfo.CurrentCulture, out var v)
               || int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out v)
               ? v : 0;

        private static DateTime TryParseDateTime(string s)
        {
            if (DateTime.TryParse(s, CultureInfo.CurrentCulture, DateTimeStyles.None, out var dt)) return dt;
            if (DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dt)) return dt;
            return DateTime.MinValue;
        }

        private static decimal TryParseDecimal(string s)
        {
            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out var d)) return d;
            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out d)) return d;
            return 0m;
        }

        private class OrderRow
        {
            public int RentalId { get; set; }
            public string CustomerName { get; set; }
            public string Brand { get; set; }
            public string Model { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public decimal TotalPrice { get; set; }
        }
    }
}
