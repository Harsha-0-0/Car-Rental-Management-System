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

            CreateMenu();

            LoadCarsGallery();
        }

        private void CreateMenu()
        {
            // Create menu strip
            MenuStrip menuStrip = new MenuStrip();
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



        private void LoadCarsGallery()
        {
            flowLayoutPanel1.Controls.Clear();

            // Load cars from JSON
            string filePath = Path.Combine(Application.StartupPath, "Data", "cars.json");
            cars = new List<Car>();

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Cars data file not found!");
                return;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                cars = JsonSerializer.Deserialize<List<Car>>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading cars.json: " + ex.Message);
                return;
            }

            if (cars == null || cars.Count == 0)
            {
                MessageBox.Show("No cars found in the JSON file.");
                return;
            }

           


            foreach (var car in cars)
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

                // View Details button
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

                // Add card to FlowLayoutPanel
                flowLayoutPanel1.Controls.Add(card);
               
            }
          

            // FlowLayoutPanel styling
            flowLayoutPanel1.BackColor = Color.LightGray;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Dock = DockStyle.Fill;
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
