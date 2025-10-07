using Car_Rental_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Car_Rental_Management_System.Forms
{
    public partial class CarListForm : Form
    {
        private List<Car> cars;

        public CarListForm()
        {
            InitializeComponent();
            LoadCarsGallery();
        }

        private void LoadCarsGallery()
        {
            // Clear existing items
            flowLayoutPanel1.Controls.Clear();

            cars = new List<Car>
    {
        new Car { CarId = 1, Brand = "Toyota", Model = "Corolla", PricePerDay = 50, ImagePath="Images/toyota.jpg" },
        new Car { CarId = 2, Brand = "Honda", Model = "Civic", PricePerDay = 55, ImagePath="Images/honda.jpg" }
    };

            foreach (var car in cars)
            {
                Panel card = new Panel();
                card.Width = 200;
                card.Height = 250;
                card.Margin = new Padding(10);
                card.BorderStyle = BorderStyle.FixedSingle;

                PictureBox pic = new PictureBox();
                pic.Width = 180;
                pic.Height = 120;
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.Top = 10;
                pic.Left = 10;

                string fullPath = Path.Combine(Application.StartupPath, car.ImagePath);
                if (File.Exists(fullPath))
                    pic.Image = Image.FromFile(fullPath);

                Label lblName = new Label();
                lblName.Text = $"{car.Brand} {car.Model}";
                lblName.AutoSize = false;
                lblName.Width = 180;
                lblName.Top = 140;
                lblName.Left = 10;
                lblName.TextAlign = ContentAlignment.MiddleCenter;
                lblName.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                Label lblPrice = new Label();
                lblPrice.Text = $"${car.PricePerDay}/day";
                lblPrice.AutoSize = false;
                lblPrice.Width = 180;
                lblPrice.Top = 170;
                lblPrice.Left = 10;
                lblPrice.TextAlign = ContentAlignment.MiddleCenter;
                lblPrice.Font = new Font("Segoe UI", 9, FontStyle.Italic);

                Button btn = new Button();
                btn.Text = "View Details";
                btn.Width = 120;
                btn.Top = 200;
                btn.Left = 40;
                btn.Tag = car;
                btn.Click += Btn_Click;

                card.Controls.Add(pic);
                card.Controls.Add(lblName);
                card.Controls.Add(lblPrice);
                card.Controls.Add(btn);

                flowLayoutPanel1.Controls.Add(card);
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Car selectedCar = (Car)btn.Tag;

            var detailsForm = new CarDetailsForm(selectedCar);
            detailsForm.ShowDialog();
        }


        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Car selectedCar = (Car)btn.Tag;

            var detailsForm = new CarDetailsForm(selectedCar);
            detailsForm.ShowDialog();
        }
    }
}
