using Car_Rental_Management_System.Utility;
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
            UIStyleHelper.ApplyDefaultStyle(this);
            carsFilePath = Path.Combine(Application.StartupPath, "Data", "cars.json");

            btnBrowse.Click += BtnBrowse_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => this.Close();

            // Apply button styles
            UIStyleHelper.StyleButton(btnSave, btnSave.Location);
            UIStyleHelper.StyleButton(btnCancel, btnCancel.Location);
            UIStyleHelper.StyleButton(btnBrowse, btnBrowse.Location);
            UIStyleHelper.StyleLabel(lblBrand);
            UIStyleHelper.StyleLabel(lblModel);
            UIStyleHelper.StyleLabel(lblPrice);
            UIStyleHelper.StyleLabel(lblImage);

            checkBox1.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            checkBox1.ForeColor = Color.FromArgb(45, 45, 45);
            checkBox1.Cursor = Cursors.Hand;


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

            List<Car> cars = new List<Car>();

            // 🟢 Read existing JSON manually to preserve LuxuryCar objects
            if (File.Exists(carsFilePath))
            {
                string json = File.ReadAllText(carsFilePath);
                JsonDocument doc = JsonDocument.Parse(json);

                foreach (var element in doc.RootElement.EnumerateArray())
                {
                    if (element.TryGetProperty("LuxuryTaxRate", out var taxProp))
                    {
                        cars.Add(new LuxuryCar
                        {
                            CarId = element.GetProperty("CarId").GetInt32(),
                            Brand = element.GetProperty("Brand").GetString(),
                            Model = element.GetProperty("Model").GetString(),
                            PricePerDay = element.GetProperty("PricePerDay").GetDecimal(),
                            ImagePath = element.GetProperty("ImagePath").GetString(),
                            LuxuryTaxRate = taxProp.GetDecimal()
                        });
                    }
                    else
                    {
                        cars.Add(new Car
                        {
                            CarId = element.GetProperty("CarId").GetInt32(),
                            Brand = element.GetProperty("Brand").GetString(),
                            Model = element.GetProperty("Model").GetString(),
                            PricePerDay = element.GetProperty("PricePerDay").GetDecimal(),
                            ImagePath = element.GetProperty("ImagePath").GetString()
                        });
                    }
                }
            }

            // Ensure /Images folder exists
            string destinationFolder = Path.Combine(Application.StartupPath, "Images");
            Directory.CreateDirectory(destinationFolder);

            // Copy image
            string imageFileName = Path.GetFileName(selectedImageFullPath);
            string destinationPath = Path.Combine(destinationFolder, imageFileName);
            if (!File.Exists(destinationPath))
            {
                File.Copy(selectedImageFullPath, destinationPath);
            }

            string relativeImagePath = $"Images/{imageFileName}";

            Car newCar;

            if (checkBox1.Checked)
            {
                newCar = new LuxuryCar
                {
                    CarId = cars.Count > 0 ? cars[^1].CarId + 1 : 1,
                    Brand = txtBrand.Text.Trim(),
                    Model = txtModel.Text.Trim(),
                    PricePerDay = price,
                    ImagePath = relativeImagePath,
                    LuxuryTaxRate = 0.15m  // or allow user to enter custom value
                };
            }
            else
            {
                newCar = new Car
                {
                    CarId = cars.Count > 0 ? cars[^1].CarId + 1 : 1,
                    Brand = txtBrand.Text.Trim(),
                    Model = txtModel.Text.Trim(),
                    PricePerDay = price,
                    ImagePath = relativeImagePath
                };
            }

            cars.Add(newCar);


            var jsonElements = new List<Dictionary<string, object>>();

            foreach (var car in cars)
            {
                var carData = new Dictionary<string, object>
                {
                    ["CarId"] = car.CarId,
                    ["Brand"] = car.Brand,
                    ["Model"] = car.Model,
                    ["PricePerDay"] = car.PricePerDay,
                    ["ImagePath"] = car.ImagePath
                };

                // If it's a LuxuryCar, add the LuxuryTaxRate
                if (car is LuxuryCar luxuryCar)
                    carData["LuxuryTaxRate"] = luxuryCar.LuxuryTaxRate;

                jsonElements.Add(carData);
            }

            // Serialize manually
            string updatedJson = JsonSerializer.Serialize(jsonElements, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(carsFilePath, updatedJson);

            

            MessageBox.Show("Car added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}
