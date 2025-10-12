using Car_Rental_Management_System.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace Car_Rental_Management_System.Forms
{
    public partial class AddCarForm : Form
    {
        private string carsFilePath;
        private string selectedImageFullPath;

        public AddCarForm()
        {
            InitializeComponent();
            carsFilePath = Path.Combine(Application.StartupPath, "Data", "cars.json");

            btnBrowse.Click += BtnBrowse_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    selectedImageFullPath = ofd.FileName;
                    txtImagePath.Text = Path.GetFileName(selectedImageFullPath);
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtBrand.Text) ||
                string.IsNullOrWhiteSpace(txtModel.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(selectedImageFullPath))
            {
                MessageBox.Show("Please fill in all fields and choose an image.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price))
            {
                MessageBox.Show("Invalid price format.");
                return;
            }

            // Load existing cars
            List<Car> cars = new List<Car>();
            if (File.Exists(carsFilePath))
            {
                string json = File.ReadAllText(carsFilePath);
                cars = JsonSerializer.Deserialize<List<Car>>(json) ?? new List<Car>();
            }

            // Ensure Images folder exists
            string destinationFolder = Path.Combine(Application.StartupPath, "Images");
            Directory.CreateDirectory(destinationFolder);

            // Copy image into /Images
            string imageFileName = Path.GetFileName(selectedImageFullPath);
            string destinationPath = Path.Combine(destinationFolder, imageFileName);

            // Avoid overwriting unless needed
            if (!File.Exists(destinationPath))
            {
                File.Copy(selectedImageFullPath, destinationPath);
            }

            // Save relative path
            string relativeImagePath = $"Images/{imageFileName}";

            // Create new car
            Car newCar = new Car
            {
                CarId = cars.Count > 0 ? cars[^1].CarId + 1 : 1,
                Brand = txtBrand.Text.Trim(),
                Model = txtModel.Text.Trim(),
                PricePerDay = price,
                ImagePath = relativeImagePath
            };

            cars.Add(newCar);

            // Save back to JSON
            string updatedJson = JsonSerializer.Serialize(cars, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(carsFilePath, updatedJson);

            MessageBox.Show("Car added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;  // notify CarListForm to refresh
            this.Close();
        }
    }
}
