using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class CellControl : UserControl
    {
        public int X { get; set; }

        public int Y { get; set; }

        public bool IsSelected { get; set; }

        public CellControl()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            pictureBox1.BackColor = Color.White;
            IsSelected = false;
        }

        private void pictureBox1_Click(object sender, System.EventArgs e)
        {
            pictureBox1.BackColor = pictureBox1.BackColor == Color.Red
                ? Color.White
                : Color.Red;

            IsSelected = !IsSelected;
        }
    }
}
