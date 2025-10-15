namespace Car_Rental_Management_System.Forms
{
    partial class AddCarForm
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
            lblBrand = new Label();
            lblModel = new Label();
            lblPrice = new Label();
            lblImage = new Label();
            txtBrand = new TextBox();
            txtModel = new TextBox();
            txtPrice = new TextBox();
            txtImagePath = new TextBox();
            btnBrowse = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // lblBrand
            // 
            lblBrand.AutoSize = true;
            lblBrand.Location = new Point(101, 65);
            lblBrand.Name = "lblBrand";
            lblBrand.Size = new Size(51, 20);
            lblBrand.TabIndex = 0;
            lblBrand.Text = "Brand:";
            // 
            // lblModel
            // 
            lblModel.AutoSize = true;
            lblModel.Location = new Point(101, 129);
            lblModel.Name = "lblModel";
            lblModel.Size = new Size(55, 20);
            lblModel.TabIndex = 1;
            lblModel.Text = "Model:";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(101, 209);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(100, 20);
            lblPrice.TabIndex = 2;
            lblPrice.Text = "Price per Day:";
            // 
            // lblImage
            // 
            lblImage.AutoSize = true;
            lblImage.Location = new Point(101, 288);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(86, 20);
            lblImage.TabIndex = 3;
            lblImage.Text = "Image Path:";
            // 
            // txtBrand
            // 
            txtBrand.Location = new Point(466, 58);
            txtBrand.Name = "txtBrand";
            txtBrand.Size = new Size(125, 27);
            txtBrand.TabIndex = 4;
            // 
            // txtModel
            // 
            txtModel.Location = new Point(466, 126);
            txtModel.Name = "txtModel";
            txtModel.Size = new Size(125, 27);
            txtModel.TabIndex = 5;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(466, 202);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(125, 27);
            txtPrice.TabIndex = 6;
            // 
            // txtImagePath
            // 
            txtImagePath.Location = new Point(466, 281);
            txtImagePath.Name = "txtImagePath";
            txtImagePath.Size = new Size(125, 27);
            txtImagePath.TabIndex = 7;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(630, 280);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(94, 29);
            btnBrowse.TabIndex = 8;
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(409, 409);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 9;
            btnSave.Text = "Save Car";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(554, 409);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(630, 204);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(73, 24);
            checkBox1.TabIndex = 11;
            checkBox1.Text = "Luxury";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // AddCarForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(checkBox1);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(btnBrowse);
            Controls.Add(txtImagePath);
            Controls.Add(txtPrice);
            Controls.Add(txtModel);
            Controls.Add(txtBrand);
            Controls.Add(lblImage);
            Controls.Add(lblPrice);
            Controls.Add(lblModel);
            Controls.Add(lblBrand);
            Name = "AddCarForm";
            Text = "AddCarForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBrand;
        private Label lblModel;
        private Label lblPrice;
        private Label lblImage;
        private TextBox txtBrand;
        private TextBox txtModel;
        private TextBox txtPrice;
        private TextBox txtImagePath;
        private Button btnBrowse;
        private Button btnSave;
        private Button btnCancel;
        private CheckBox checkBox1;
    }
}