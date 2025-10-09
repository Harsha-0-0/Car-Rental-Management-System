namespace Car_Rental_Management_System.Forms
{
    partial class OrderHistory
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblSummary;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblSummary = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToDeleteRows = false;
            this.dgvOrders.AutoGenerateColumns = false;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrders.Location = new System.Drawing.Point(0, 46);
            this.dgvOrders.MultiSelect = false;
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.RowHeadersVisible = false;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(984, 513);
            this.dgvOrders.TabIndex = 0;
            // Define columns
            var colId = new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "RentalId", HeaderText = "Rental ID", Width = 90 };
            var colCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "CustomerName", HeaderText = "Customer", Width = 170 };
            var colBrand = new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Brand", HeaderText = "Brand", Width = 120 };
            var colModel = new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "Model", HeaderText = "Model", Width = 140 };
            var colStart = new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "StartDate", HeaderText = "Start Date/Time", Width = 150, DefaultCellStyle = { Format = "g" } };
            var colEnd = new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "EndDate", HeaderText = "End Date/Time", Width = 150, DefaultCellStyle = { Format = "g" } };
            var colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn { DataPropertyName = "TotalPrice", HeaderText = "Total ($)", Width = 90, DefaultCellStyle = { Format = "N2", Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight } };
            this.dgvOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colId, colCustomer, colBrand, colModel, colStart, colEnd, colTotal });

            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.btnOpenFolder);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(984, 46);
            this.panelTop.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(8, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 30);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(104, 8);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(110, 30);
            this.btnOpenFolder.TabIndex = 1;
            this.btnOpenFolder.Text = "Open Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // lblSummary
            // 
            this.lblSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblSummary.Location = new System.Drawing.Point(0, 559);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblSummary.Size = new System.Drawing.Size(984, 28);
            this.lblSummary.TabIndex = 2;
            this.lblSummary.Text = "Ready";
            this.lblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OrderHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 587);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.lblSummary);
            this.Name = "OrderHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Order History";
            this.Load += new System.EventHandler(this.OrderHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
