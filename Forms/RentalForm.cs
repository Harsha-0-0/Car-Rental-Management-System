using Car_Rental_Management_System.Data;
using Car_Rental_Management_System.Interface;
using Car_Rental_Management_System.Models;
using System;
using System.IO;
using System.Windows.Forms;

namespace Car_Rental_Management_System.Forms
{
    public partial class RentalForm : Form
    {
        private Car selectedCar;

        public RentalForm(Car car)
        {
            InitializeComponent();
            try
            {
                selectedCar = car;
                labelCar.Text = $"{selectedCar.Brand} {selectedCar.Model}";
                labelPricePerDay.Text = $"${selectedCar.PricePerDay}/day";

                // Default to today and tomorrow
                dtpStartDate.Value = DateTime.Today;
                dtpEndDate.Value = DateTime.Today.AddDays(1);

                // Hook up event handlers to update total dynamically
                dtpStartDate.ValueChanged += DateChanged;
                dtpEndDate.ValueChanged += DateChanged;

                if (selectedCar is LuxuryCar luxury)
                {
                    labelLuxury.Text = $"Luxury Tax: {luxury.LuxuryTaxRate:P0}";
                    labelLuxury.Visible = true;
                }
                else
                {
                    labelLuxury.Visible = false;
                }

                // Calculate once on load
                CalculateTotalCost();

                btnConfirmRental.Click += BtnConfirmRental_Click;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing RentalForm: " + ex.Message);
            }
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

            // Calculate number of days
            TimeSpan duration = dtpEndDate.Value.Date - dtpStartDate.Value.Date;
            int days = duration.Days == 0 ? 1 : duration.Days; // at least 1 day

           
            decimal total = selectedCar.GetTotalPrice(days);

            labelTotalPrice.ForeColor = System.Drawing.Color.Black;
            labelTotalPrice.Text = $"Total: ${total:F2}";
        }






        private void BtnConfirmRental_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text.Trim();
            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Please enter customer name.", "Missing Info",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime startDate = dtpStartDate.Value.Date;
            DateTime endDate = dtpEndDate.Value.Date;

            if (endDate < startDate)
            {
                MessageBox.Show("End date cannot be before start date.", "Invalid Dates",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calculate rental duration
            TimeSpan duration = endDate - startDate;
            int days = duration.Days == 0 ? 1 : duration.Days; // minimum 1 day


            PriceCalculateInterface pricedCar = selectedCar;
            decimal total = pricedCar.CalculatePrice(selectedCar.PricePerDay, days);



            // Create rental record
            Rental rental = new Rental
            {
                RentalId = new Random().Next(1000, 9999),
                RentedCar = selectedCar,
                CustomerName = customerName,
                StartDate = startDate,
                EndDate = endDate,
                TotalPrice = total
            };

            // Save rental info
            SaveRentalToFile(rental);

            MessageBox.Show($"Rental confirmed for {customerName}!\n" +
                            $"Car: {selectedCar.Brand} {selectedCar.Model}\n" +
                            $"Total: ${total}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            this.Close();
        }


        private void SaveRentalToFile(Rental rental)
        {
            string filePath = Path.Combine(Application.StartupPath, "Data", "rentals.json");
            var repo = new Repository<Rental>(filePath);

            var rentals = repo.Load();
            rentals.Add(rental);
            repo.Save(rentals);
        }


    }
}
