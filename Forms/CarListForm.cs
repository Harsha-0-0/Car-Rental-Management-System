using Car_Rental_Management_System.Data;
using Car_Rental_Management_System.Utility;
using Car_Rental_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace Car_Rental_Management_System.Forms
{
    public partial class CarListForm : Form
    {
        private List<Car> cars;

        public CarListForm()
        {
            InitializeComponent();

            UIStyleHelper.ApplyDefaultStyle(this, isMainForm: true);

            CreateMenu();

            CreateDropdown();

            LoadCarsGallery();
        }

        
        MenuStrip menuStrip = new MenuStrip();
        private void CreateDropdown()
        {
            comboBox1 = new ComboBox
            {
                Width = 300,
                Top = menuStrip.Bottom + 10,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI Semibold", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(40, 40, 40),
                BackColor = Color.White,
            };

            comboBox1.Items.Add("All Cars");
            comboBox1.Items.Add("Luxury Cars Only");
            comboBox1.Items.Add("Standard Cars Only");
            comboBox1.Items.Add("Sort by Price (Low → High)");
            comboBox1.Items.Add("Sort by Price (High → Low)");
            comboBox1.SelectedIndex = 0;

            // Position it at the right end
            comboBox1.Left = this.ClientSize.Width - comboBox1.Width - 20; // 20px padding from right

            // Optional: make it stay at right on resize
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;

            this.Controls.Add(comboBox1);
            comboBox1.BringToFront();
        }


        private void CreateMenu()
        {
            
            this.MainMenuStrip = menuStrip;

            // Create top-level menu items
            ToolStripMenuItem addCarMenu = new ToolStripMenuItem("Add New Car");
            ToolStripMenuItem orderHistoryMenu = new ToolStripMenuItem("Order History");
            ToolStripMenuItem exitMenu = new ToolStripMenuItem("Exit");


            // Assign click events
            addCarMenu.Click += AddCarItem_Click;
            orderHistoryMenu.Click += OrderHistoryItem_Click;
            exitMenu.Click += (s, e) => this.Close();

            // directly to the menu bar
            menuStrip.Items.Add(addCarMenu);
            menuStrip.Items.Add(orderHistoryMenu);
            menuStrip.Items.Add(exitMenu);


            // Add MenuStrip to the form
            this.Controls.Add(menuStrip);
            menuStrip.Dock = DockStyle.Top;
            menuStrip.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            menuStrip.BackColor = Color.WhiteSmoke;

        }


        private void AddCarItem_Click(object sender, EventArgs e)
        {
            var addCarForm = new AddCarForm();  
            if (addCarForm.ShowDialog() == DialogResult.OK)
            {
                LoadCarsGallery(); // refresh the car list after adding a new car
            }
        }

        private void OrderHistoryItem_Click(object sender, EventArgs e)
        {
            using (var historyForm = new OrderHistory())
            {
                historyForm.ShowDialog();
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cars == null) return;

            List<Car> filteredList = cars.ToList();

            switch (comboBox1.SelectedItem.ToString())
            {
                case "Luxury Cars Only":
                    filteredList = filteredList.Where(c => c is LuxuryCar).ToList();
                    break;

                case "Standard Cars Only":
                    filteredList = filteredList.Where(c => c is not LuxuryCar).ToList();
                    break;

                case "Sort by Price (Low → High)":
                    filteredList = filteredList.OrderBy(c => c.PricePerDay).ToList();
                    break;

                case "Sort by Price (High → Low)":
                    filteredList = filteredList.OrderByDescending(c => c.PricePerDay).ToList();
                    break;

                case "All Cars":
                default:
                    break;
            }

            BuildCarsGallery(filteredList);
        }

        private Repository<Car> carRepo;

       


        private void LoadCarsGallery()
    {
        flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.Top = comboBox1.Bottom + 20; // 20px spacing below dropdown
            flowLayoutPanel1.Left = 0;
            flowLayoutPanel1.Width = this.ClientSize.Width;
            flowLayoutPanel1.Height = this.ClientSize.Height - flowLayoutPanel1.Top;
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;


            string filePath = Path.Combine(Application.StartupPath, "Data", "cars.json");
        carRepo = new Repository<Car>(filePath);

        cars = new List<Car>();

        // If file doesn't exist
        if (!File.Exists(filePath))
        {
            MessageBox.Show("Cars data file not found!");
            return;
        }

        try
        {
            // Read the JSON manually to handle both Car and LuxuryCar
            string json = File.ReadAllText(filePath);
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
        catch (Exception ex)
        {
            MessageBox.Show("Error reading cars.json: " + ex.Message);
            return;
        }

        if (cars.Count == 0)
        {
            MessageBox.Show("No cars found in the JSON file.");
            return;
        }

        // Continue building the gallery as before
        BuildCarsGallery(cars);
    }


    private void BuildCarsGallery(List<Car> carList)
        {
            flowLayoutPanel1.Controls.Clear();

            foreach (var car in carList)
            {
                

                // Card panel
                Panel card = new Panel
                {
                    Width = 220,
                    Height = 320,
                    Margin = new Padding(15),
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle
                };
                if (car is LuxuryCar luxury)
                {
                    card.BackColor = Color.LightGoldenrodYellow;

                    Label lblLuxury = new Label
                    {
                        Text = "LUXURY",
                        ForeColor = Color.DarkGoldenrod,
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        Width = 220,
                        Top = 10,
                        TextAlign = ContentAlignment.TopCenter
                    };
                    card.Controls.Add(lblLuxury);
                }

                // Car image
                PictureBox pic = new PictureBox
                {
                    Width = 200,
                    Height = 140,
                    Top = 10,
                    Left = 10,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.Fixed3D
                };
                string fullPath = Path.Combine(Application.StartupPath, car.ImagePath);
                if (File.Exists(fullPath))
                    pic.Image = Image.FromFile(fullPath);
                card.Controls.Add(pic);

                // Brand & model
                Label lblName = new Label
                {
                    Text = $"{car.Brand} {car.Model}",
                    Width = 200,
                    Height = 30,
                    Top = 160,
                    Left = 10,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.DarkBlue
                };
                card.Controls.Add(lblName);

                // Price
                Label lblPrice = new Label
                {
                    Text = $"${car.PricePerDay}/day",
                    Width = 200,
                    Height = 25,
                    Top = 200,
                    Left = 10,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10, FontStyle.Italic),
                    ForeColor = Color.DarkGreen
                };
                card.Controls.Add(lblPrice);
                // Example for button:
                Button btn = new Button
                {
                    Text = "View Details",
                    Width = 140,
                    Height = 35,
                    Top = 240,
                    Left = 40,
                    BackColor = Color.LightSkyBlue,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Tag = car
                };
                btn.Click += Btn_Click;
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


        private void Btn_ViewHistory(object sender, EventArgs e)
        {

            var detailsForm = new OrderHistory();
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
