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
            labelTotalPrice = new Label();
            btnConfirmRental = new Button();
            dtpStartDate = new DateTimePicker();
            dtpEndDate = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            labelLuxury = new Label();
            SuspendLayout();
            // 
            // labelCar
            // 
            labelCar.AutoSize = true;
            labelCar.Location = new Point(124, 36);
            labelCar.Name = "labelCar";
            labelCar.Size = new Size(50, 20);
            labelCar.TabIndex = 0;
            labelCar.Text = "label1";
            // 
            // labelPricePerDay
            // 
            labelPricePerDay.AutoSize = true;
            labelPricePerDay.Location = new Point(217, 36);
            labelPricePerDay.Name = "labelPricePerDay";
            labelPricePerDay.Size = new Size(50, 20);
            labelPricePerDay.TabIndex = 1;
            labelPricePerDay.Text = "label2";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(492, 96);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(125, 27);
            txtCustomerName.TabIndex = 2;
            // 
            // labelTotalPrice
            // 
            labelTotalPrice.AutoSize = true;
            labelTotalPrice.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelTotalPrice.Location = new Point(197, 386);
            labelTotalPrice.Name = "labelTotalPrice";
            labelTotalPrice.Size = new Size(148, 23);
            labelTotalPrice.TabIndex = 4;
            labelTotalPrice.Text = "Total Price: $0.00";
            // 
            // btnConfirmRental
            // 
            btnConfirmRental.Location = new Point(492, 380);
            btnConfirmRental.Name = "btnConfirmRental";
            btnConfirmRental.Size = new Size(76, 29);
            btnConfirmRental.TabIndex = 6;
            btnConfirmRental.Text = "Rent";
            btnConfirmRental.UseVisualStyleBackColor = true;
            // 
            // dtpStartDate
            // 
            dtpStartDate.Location = new Point(483, 251);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(250, 27);
            dtpStartDate.TabIndex = 7;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Location = new Point(483, 284);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(250, 27);
            dtpEndDate.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(197, 255);
            label1.Name = "label1";
            label1.Size = new Size(76, 20);
            label1.TabIndex = 9;
            label1.Text = "Start Date";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(197, 291);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 10;
            label2.Text = "End Date";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(197, 103);
            label3.Name = "label3";
            label3.Size = new Size(49, 20);
            label3.TabIndex = 11;
            label3.Text = "Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(197, 165);
            label4.Name = "label4";
            label4.Size = new Size(46, 20);
            label4.TabIndex = 12;
            label4.Text = "Email";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(492, 165);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 13;
            // 
            // labelLuxury
            // 
            labelLuxury.AutoSize = true;
            labelLuxury.Location = new Point(197, 421);
            labelLuxury.Name = "labelLuxury";
            labelLuxury.Size = new Size(50, 20);
            labelLuxury.TabIndex = 14;
            labelLuxury.Text = "label5";
            // 
            // RentalForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelLuxury);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dtpEndDate);
            Controls.Add(dtpStartDate);
            Controls.Add(btnConfirmRental);
            Controls.Add(labelTotalPrice);
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
        private Label labelTotalPrice;
        private Button btnConfirmRental;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private Label labelLuxury;
    }
}