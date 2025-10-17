using Car_Rental_Management_System.Data;
using Car_Rental_Management_System.Interface;
using Car_Rental_Management_System.Models;
using Car_Rental_Management_System.Utility;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Car_Rental_Management_System.Forms
{
    public partial class RentalForm : Form
    {
        private Car selectedCar;
        private Label labelLuxury;
        private Label labelTotalPrice;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private TextBox txtCustomerName;
        private Button btnConfirmRental;
        private Button btnCancel;

        public RentalForm(Car car)
        {
            InitializeComponent();

            selectedCar = car;
            UIStyleHelper.ApplyDefaultStyle(this); // Apply consistent style

            // === Form layout ===
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 80, 30, 30), 
                ColumnCount = 2,
                AutoSize = true
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));

            // === Title ===
            Label title = new Label
            {
                Text = $"{selectedCar.Brand} {selectedCar.Model}",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(title);

            // === Fields ===
            layout.Controls.Add(new Label { Text = "Price per Day:", Font = new Font("Segoe UI", 11), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
            layout.Controls.Add(new Label { Text = $"${selectedCar.PricePerDay}", Font = new Font("Segoe UI", 11, FontStyle.Bold), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 1, 0);

            labelLuxury = new Label
            {
                Text = selectedCar is LuxuryCar luxury ? $"Luxury Tax: {luxury.LuxuryTaxRate:P0}" : "",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkGoldenrod,
                Visible = selectedCar is LuxuryCar,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            layout.Controls.Add(labelLuxury, 1, 1);

            layout.Controls.Add(new Label { Text = "Customer Name:", Font = new Font("Segoe UI", 11), Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleLeft }, 0, 2);
            txtCustomerName = new TextBox { Font = new Font("Segoe UI", 11), Dock = DockStyle.Fill };
            layout.Controls.Add(txtCustomerName, 1, 2);

            layout.Controls.Add(new Label { Text = "Start Date:", Font = new Font("Segoe UI", 11), Dock = DockStyle.Fill }, 0, 3);
            dtpStartDate = new DateTimePicker { Font = new Font("Segoe UI", 11), Dock = DockStyle.Fill };
            layout.Controls.Add(dtpStartDate, 1, 3);

            layout.Controls.Add(new Label { Text = "End Date:", Font = new Font("Segoe UI", 11), Dock = DockStyle.Fill }, 0, 4);
            dtpEndDate = new DateTimePicker { Font = new Font("Segoe UI", 11), Dock = DockStyle.Fill };
            layout.Controls.Add(dtpEndDate, 1, 4);

            layout.Controls.Add(new Label { Text = "Total Price:", Font = new Font("Segoe UI", 11), Dock = DockStyle.Fill }, 0, 5);
            labelTotalPrice = new Label { Font = new Font("Segoe UI", 11, FontStyle.Bold), Dock = DockStyle.Fill, ForeColor = Color.DarkGreen };
            layout.Controls.Add(labelTotalPrice, 1, 5);

            // === Buttons ===
            var buttonPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Bottom,
                Height = 60
            };

            btnConfirmRental = new Button { Text = "Confirm Rental" };
            btnCancel = new Button { Text = "Cancel" };

            UIStyleHelper.StyleButton(btnConfirmRental, new Point(0, 0), new Size(160, 40));
            UIStyleHelper.StyleButton(btnCancel, new Point(0, 0), new Size(100, 40));

            btnConfirmRental.Click += BtnConfirmRental_Click;
            btnCancel.Click += (s, e) => this.Close();

            buttonPanel.Controls.Add(btnConfirmRental);
            buttonPanel.Controls.Add(btnCancel);

            this.Controls.Add(layout);
            this.Controls.Add(buttonPanel);

            // === Logic ===
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today.AddDays(1);
            dtpStartDate.ValueChanged += DateChanged;
            dtpEndDate.ValueChanged += DateChanged;
            CalculateTotalCost();
        }

        private void DateChanged(object sender, EventArgs e)
        {
            CalculateTotalCost();
        }

        private void CalculateTotalCost()
        {
            if (dtpEndDate.Value.Date < dtpStartDate.Value.Date)
            {
                labelTotalPrice.Text = "Invalid Dates";
                labelTotalPrice.ForeColor = System.Drawing.Color.Red;
                return;
            }

            TimeSpan duration = dtpEndDate.Value.Date - dtpStartDate.Value.Date;
            int days = duration.Days == 0 ? 1 : duration.Days;

            decimal total = selectedCar.GetTotalPrice(days);
            labelTotalPrice.ForeColor = Color.DarkGreen;
            labelTotalPrice.Text = $"${total:F2}";
        }

        private void BtnConfirmRental_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text.Trim();
            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Please enter customer name.", "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;
            if (endDate < startDate)
            {
                MessageBox.Show("End date cannot be before start date.", "Invalid Dates", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TimeSpan duration = endDate - startDate;
            int days = duration.Days == 0 ? 1 : duration.Days;

            PriceCalculateInterface pricedCar = selectedCar;
            decimal total = pricedCar.CalculatePrice(selectedCar.PricePerDay, days);

            Rental rental = new Rental
            {
                RentalId = new Random().Next(1000, 9999),
                RentedCar = selectedCar,
                CustomerName = customerName,
                StartDate = startDate,
                EndDate = endDate,
                TotalPrice = total
            };

            string filePath = Path.Combine(Application.StartupPath, "Data", "rentals.json");
            var repo = new Repository<Rental>(filePath);
            var rentals = repo.Load();
            rentals.Add(rental);
            repo.Save(rentals);

            MessageBox.Show($"Rental confirmed for {customerName}!\nTotal: ${total}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
