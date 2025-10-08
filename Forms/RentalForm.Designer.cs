namespace Car_Rental_Management_System.Forms
{
    partial class RentalForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelCar = new Label();
            labelPricePerDay = new Label();
            txtCustomerName = new TextBox();
            txtDays = new TextBox();
            labelTotalPrice = new Label();
            btnCalculate = new Button();
            btnConfirmRental = new Button();
            SuspendLayout();
            // 
            // labelCar
            // 
            labelCar.AutoSize = true;
            labelCar.Location = new Point(184, 134);
            labelCar.Name = "labelCar";
            labelCar.Size = new Size(50, 20);
            labelCar.TabIndex = 0;
            labelCar.Text = "label1";
            // 
            // labelPricePerDay
            // 
            labelPricePerDay.AutoSize = true;
            labelPricePerDay.Location = new Point(184, 194);
            labelPricePerDay.Name = "labelPricePerDay";
            labelPricePerDay.Size = new Size(50, 20);
            labelPricePerDay.TabIndex = 1;
            labelPricePerDay.Text = "label2";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(481, 127);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(125, 27);
            txtCustomerName.TabIndex = 2;
            // 
            // txtDays
            // 
            txtDays.Location = new Point(482, 190);
            txtDays.Name = "txtDays";
            txtDays.Size = new Size(125, 27);
            txtDays.TabIndex = 3;
            // 
            // labelTotalPrice
            // 
            labelTotalPrice.AutoSize = true;
            labelTotalPrice.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalPrice.Location = new Point(182, 293);
            labelTotalPrice.Name = "labelTotalPrice";
            labelTotalPrice.Size = new Size(148, 23);
            labelTotalPrice.TabIndex = 4;
            labelTotalPrice.Text = "Total Price: $0.00";
            // 
            // btnCalculate
            // 
            btnCalculate.Location = new Point(409, 350);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(94, 29);
            btnCalculate.TabIndex = 5;
            btnCalculate.Text = "Calculate";
            btnCalculate.UseVisualStyleBackColor = true;
            // 
            // btnConfirmRental
            // 
            btnConfirmRental.Location = new Point(562, 350);
            btnConfirmRental.Name = "btnConfirmRental";
            btnConfirmRental.Size = new Size(94, 29);
            btnConfirmRental.TabIndex = 6;
            btnConfirmRental.Text = "Rent";
            btnConfirmRental.UseVisualStyleBackColor = true;
            // 
            // RentalForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnConfirmRental);
            Controls.Add(btnCalculate);
            Controls.Add(labelTotalPrice);
            Controls.Add(txtDays);
            Controls.Add(txtCustomerName);
            Controls.Add(labelPricePerDay);
            Controls.Add(labelCar);
            Name = "RentalForm";
            Text = "RentalForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelCar;
        private Label labelPricePerDay;
        private TextBox txtCustomerName;
        private TextBox txtDays;
        private Label labelTotalPrice;
        private Button btnCalculate;
        private Button btnConfirmRental;
    }
}