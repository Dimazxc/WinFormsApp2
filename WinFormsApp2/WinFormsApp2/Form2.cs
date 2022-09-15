using System;
using System.Windows.Forms;
using WinFormsApp2.Constants;
using WinFormsApp2.Helpers;

namespace WinFormsApp2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var isHeightSelected = int.TryParse(textBox1.Text, out var height);
            var isWidthSelected = int.TryParse(textBox2.Text, out var width);

            if (isHeightSelected 
                && isWidthSelected
                && IsRangeValid(height, width))
            {
                GameBoardOptions.SetOptions(height, width);
                FormSwitcher.SwitchForm(this, new Form1());
            }

            MessageBox.Show("Invalid input data");
        }

        private bool IsRangeValid(int height, int width)
        {
            return height >= GameBoardOptions.BoardHeightMin
                && width >= GameBoardOptions.BoardWidthMin
                && height >= width;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormSwitcher.SwitchForm(this, new Form3());
        }
    }
}
