using Car_Rental_Management_System.Models;
using Car_Rental_Management_System.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Car_Rental_Management_System.Forms
{
    public partial class CarDetailsForm : Form
    {
        private readonly Car selectedCar;

        // UI Elements
        private PictureBox pictureBoxCar;
        private Label lblBrand, lblModel, lblPrice, lblType, lblRating, lblAvailability, lblSpecsTitle, lblFeaturesTitle;
        private FlowLayoutPanel pnlSpecs, pnlFeatures;
        private Button btnRent, btnWishlist, btnClose;

        public CarDetailsForm(Car car)
        {
            InitializeComponent();
            selectedCar = car;
            UIStyleHelper.ApplyDefaultStyle(this);
            BuildLayout();
            DisplayCarDetails();
        }

        private void BuildLayout()
        {
            // Form
            this.Text = "Car Details";
            this.Size = new Size(750, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Car Image
            pictureBoxCar = new PictureBox
            {
                Size = new Size(300, 200),
                Location = new Point(40, 40),
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(pictureBoxCar);

            // Brand + Model
            lblBrand = new Label
            {
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(370, 50),
                AutoSize = true
            };
            Controls.Add(lblBrand);

            lblModel = new Label
            {
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.Black,
                Location = new Point(370, 95),
                AutoSize = true
            };
            Controls.Add(lblModel);

            // Price
            lblPrice = new Label
            {
                Font = new Font("Segoe UI", 13, FontStyle.Italic),
                ForeColor = Color.DarkGreen,
                Location = new Point(370, 130),
                AutoSize = true
            };
            Controls.Add(lblPrice);

            // Type (Luxury/Standard)
            lblType = new Label
            {
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.DarkGoldenrod,
                Location = new Point(370, 165),
                AutoSize = true
            };
            Controls.Add(lblType);

            // Availability
            lblAvailability = new Label
            {
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.MediumSeaGreen,
                Location = new Point(370, 195),
                AutoSize = true
            };
            Controls.Add(lblAvailability);

            // ⭐ Rating
            lblRating = new Label
            {
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.Goldenrod,
                Location = new Point(370, 225),
                AutoSize = true
            };
            Controls.Add(lblRating);

            // Specs Title
            lblSpecsTitle = new Label
            {
                Text = "Specifications",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(40, 270),
                AutoSize = true
            };
            Controls.Add(lblSpecsTitle);

            // Specs Panel
            pnlSpecs = new FlowLayoutPanel
            {
                Location = new Point(40, 300),
                Size = new Size(650, 80),
                AutoScroll = true,
                WrapContents = true
            };
            Controls.Add(pnlSpecs);

            // Features Title
            lblFeaturesTitle = new Label
            {
                Text = "Key Features",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(40, 390),
                AutoSize = true
            };
            Controls.Add(lblFeaturesTitle);

            // Features Panel
            pnlFeatures = new FlowLayoutPanel
            {
                Location = new Point(40, 420),
                Size = new Size(650, 70),
                WrapContents = true,
                AutoScroll = true
            };
            Controls.Add(pnlFeatures);

            // Rent Button
            
            btnRent = new Button
            {
                Text = "Rent",
                Size = new Size(180, 45)
                
            };
            Location = new Point(130, 510);
            UIStyleHelper.StyleButton(btnRent, Location);

            btnRent.Click += BtnRent_Click;
            Controls.Add(btnRent);


            // Close
            btnClose = new Button
            {
                Text = "Close",
               
            };
            Location = new Point(270, 510);
            UIStyleHelper.StyleButton(btnClose, Location);


            btnClose.Click += (s, e) => this.Close();
            Controls.Add(btnClose);
        }

        private void DisplayCarDetails()
        {
            lblBrand.Text = selectedCar.Brand;
            lblModel.Text = selectedCar.Model;
            lblPrice.Text = $"${selectedCar.PricePerDay:N2} / day";

            if (selectedCar is LuxuryCar lux)
            {
                lblType.Text = $"Luxury • Tax Rate: {lux.LuxuryTaxRate:P0}";
                lblType.ForeColor = Color.DarkGoldenrod;
            }
            else
            {
                lblType.Text = "Standard Vehicle";
                lblType.ForeColor = Color.Gray;
            }

            // Random rating for demo (you can replace with real data later)
            int rating = new Random().Next(3, 6);
            lblRating.Text = $"Rating: {"★".PadRight(rating, '★').PadRight(5, '☆')}";

            lblAvailability.Text = "✅ Available for Rent";

            // Image
            string fullPath = Path.Combine(Application.StartupPath, selectedCar.ImagePath);
            if (File.Exists(fullPath))
                pictureBoxCar.Image = Image.FromFile(fullPath);
            else
                pictureBoxCar.BackColor = Color.LightGray;

            // Sample specs
            AddSpec("Fuel Type: Petrol");
            AddSpec("Seats: 5");
            AddSpec("Transmission: Automatic");
            AddSpec("Mileage: 25 MPG");

            // Sample features
            AddFeature("Bluetooth");
            AddFeature("GPS Navigation");
            AddFeature("Backup Camera");
            AddFeature("Air Conditioning");
        }

        private void AddSpec(string text)
        {
            Label lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.DimGray,
                AutoSize = true,
                Padding = new Padding(5)
            };
            pnlSpecs.Controls.Add(lbl);
        }

        private void AddFeature(string feature)
        {
            Label lbl = new Label
            {
                Text = feature,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Black,
                AutoSize = true,
                BackColor = Color.LightGray,
                Padding = new Padding(5, 2, 5, 2),
                Margin = new Padding(5),
            };
            pnlFeatures.Controls.Add(lbl);
        }

        private void BtnRent_Click(object sender, EventArgs e)
        {
            var rentForm = new RentalForm(selectedCar);
            this.Hide();
            rentForm.ShowDialog();
            this.Close();
        }
    }
}
