namespace Car_Rental_Management_System.Forms
{
    partial class CarDetailsForm
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
            labelBrand = new Label();
            labelModel = new Label();
            labelPrice = new Label();
            pictureBoxCar = new PictureBox();
            buttonRent = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCar).BeginInit();
            SuspendLayout();
            // 
            // labelBrand
            // 
            labelBrand.AutoSize = true;
            labelBrand.Location = new Point(128, 47);
            labelBrand.Name = "labelBrand";
            labelBrand.Size = new Size(50, 20);
            labelBrand.TabIndex = 0;
            labelBrand.Text = "label1";
            // 
            // labelModel
            // 
            labelModel.AutoSize = true;
            labelModel.Location = new Point(128, 121);
            labelModel.Name = "labelModel";
            labelModel.Size = new Size(50, 20);
            labelModel.TabIndex = 1;
            labelModel.Text = "label2";
            // 
            // labelPrice
            // 
            labelPrice.AutoSize = true;
            labelPrice.Location = new Point(128, 189);
            labelPrice.Name = "labelPrice";
            labelPrice.Size = new Size(50, 20);
            labelPrice.TabIndex = 2;
            labelPrice.Text = "label3";
            // 
            // pictureBoxCar
            // 
            pictureBoxCar.Location = new Point(355, 32);
            pictureBoxCar.Name = "pictureBoxCar";
            pictureBoxCar.Size = new Size(232, 177);
            pictureBoxCar.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxCar.TabIndex = 3;
            pictureBoxCar.TabStop = false;
            // 
            // buttonRent
            // 
            buttonRent.Location = new Point(355, 267);
            buttonRent.Name = "buttonRent";
            buttonRent.Size = new Size(232, 29);
            buttonRent.TabIndex = 4;
            buttonRent.Text = "Rent This Car";
            buttonRent.UseVisualStyleBackColor = true;
            // 
            // CarDetailsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonRent);
            Controls.Add(pictureBoxCar);
            Controls.Add(labelPrice);
            Controls.Add(labelModel);
            Controls.Add(labelBrand);
            Name = "CarDetailsForm";
            Text = "CarDetailsForm";
            ((System.ComponentModel.ISupportInitialize)pictureBoxCar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelBrand;
        private Label labelModel;
        private Label labelPrice;
        private PictureBox pictureBoxCar;
        private Button buttonRent;
    }
}