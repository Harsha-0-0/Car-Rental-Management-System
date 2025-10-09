using Car_Rental_Management_System.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Rental_Management_System.Forms
{
    public partial class CarDetailsForm : Form
    {

        private Car selectedCar;
        public CarDetailsForm(Car car)
        {
            InitializeComponent();
            selectedCar = car;
            DisplayCarDetails();
        }

        private void DisplayCarDetails()
        {
            labelBrand.Text = selectedCar.Brand;
            labelModel.Text = selectedCar.Model;
            labelPrice.Text = $"${selectedCar.PricePerDay}/day";
            pictureBoxCar.Image = Image.FromFile(selectedCar.ImagePath);
        }

        private void btnRent_Click(object sender, EventArgs e)
        {
            try
            {
                var rentalForm = new RentalForm(selectedCar);
                rentalForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening RentalForm: " + ex.Message);
            }
        }
    }
}
