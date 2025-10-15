using System.Drawing;
using System.Windows.Forms;

namespace Car_Rental_Management_System.Utility
{
    public static class UIStyleHelper
    {
        // Apply default window and UI styling
        public static void ApplyDefaultStyle(Form form, bool isMainForm = false)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            form.BackColor = Color.WhiteSmoke;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = false;
            form.MinimizeBox = false;

            if (isMainForm)
            {
                // Main application window — full screen
                form.WindowState = FormWindowState.Maximized;
                form.FormBorderStyle = FormBorderStyle.Sizable;
                form.BackColor = Color.White;
            }
            else
            {
                // Medium-size forms (AddCar, History, etc.)
                form.Size = new Size(800, 650); // Bigger than before
                form.MinimumSize = form.Size;
                form.MaximumSize = form.Size;
            }
        }

        // Consistent button styling with optional size and location
        public static void StyleButton(Button btn, Point location, Size? size = null)
        {
            btn.Location = location;
            btn.Size = size ?? new Size(120, 40); // default size

            btn.BackColor = Color.SteelBlue;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
        }


        // Consistent label styling
        public static void StyleLabel(Label lbl)
        {
            lbl.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(40, 40, 40);
        }

      
    }
}
