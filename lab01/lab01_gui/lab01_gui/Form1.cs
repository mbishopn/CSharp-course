using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab01_gui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int total = 0, v3 = 0, v4 = 0;
            
            total = Convert.ToInt32(val01.Text) + Convert.ToInt32(val02.Text);
            
            // I decided to try a function I googled to verify if value in textbox
            // is a number instead of assigning value of 0 when empty
            if (int.TryParse(val03.Text, out v3))  
                total += v3;
            if (int.TryParse(val04.Text, out v4))
                total += v4;

            result.Text = Convert.ToString(total);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            result.Text = Convert.ToString(Convert.ToInt32(val01.Text) - Convert.ToInt32(val02.Text));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            result.Text = Convert.ToString(Convert.ToInt32(val01.Text) * Convert.ToInt32(val02.Text));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            result.Text = Convert.ToString(Convert.ToDecimal(val01.Text) / Convert.ToDecimal(val02.Text));
        }
    }
}
