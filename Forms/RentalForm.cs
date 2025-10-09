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

                btnCalculate.Click += BtnCalculate_Click;
                btnConfirmRental.Click += BtnConfirmRental_Click;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing RentalForm: " + ex.Message);
            }
        }


        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDays.Text, out int days) || days <= 0)
            {
                MessageBox.Show("Please enter a valid number of days.");
                return;
            }

            decimal total = days * selectedCar.PricePerDay;
            labelTotalPrice.Text = $"Total Price: ${total:F2}"; // show 2 decimal places
        }


        private void BtnConfirmRental_Click(object sender, EventArgs e)
        {
            string customerName = txtCustomerName.Text.Trim();
            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Please enter customer name.");
                return;
            }

            if (!int.TryParse(txtDays.Text, out int days) || days <= 0)
            {
                MessageBox.Show("Please enter a valid number of days.");
                return;
            }

            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(days);
            decimal totalPrice = days * selectedCar.PricePerDay;

            Rental rental = new Rental
            {
                RentalId = new Random().Next(1000, 9999),
                RentedCar = selectedCar,
                CustomerName = customerName,
                StartDate = startDate,
                EndDate = endDate,
                TotalPrice = totalPrice
            };

            SaveRentalToFile(rental);
            MessageBox.Show("Rental confirmed!");
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
