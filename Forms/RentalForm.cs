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

            // Calculate total
            decimal total = days * selectedCar.PricePerDay;

            labelTotalPrice.ForeColor = System.Drawing.Color.Black;
            labelTotalPrice.Text = $"Total: ${total}";
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

            // Calculate total cost
            decimal totalPrice = days * selectedCar.PricePerDay;

            // Create rental record
            Rental rental = new Rental
            {
                RentalId = new Random().Next(1000, 9999),
                RentedCar = selectedCar,
                CustomerName = customerName,
                StartDate = startDate,
                EndDate = endDate,
                TotalPrice = totalPrice
            };

            // Save rental info (you can later connect this to file or DB)
            SaveRentalToFile(rental);

            MessageBox.Show($"Rental confirmed for {customerName}!\n" +
                            $"Car: {selectedCar.Brand} {selectedCar.Model}\n" +
                            $"Total: ${totalPrice}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);


            this.Close();
        }


        private void SaveRentalToFile(Rental rental)
        {
            string filePath = Path.Combine(Application.StartupPath, "rentals.txt");
            string line = $"{rental.RentalId},{rental.CustomerName},{rental.RentedCar.Brand},{rental.RentedCar.Model},{rental.StartDate},{rental.EndDate},{rental.TotalPrice}";
            File.AppendAllText(filePath, line + Environment.NewLine);
        }

    }
}
