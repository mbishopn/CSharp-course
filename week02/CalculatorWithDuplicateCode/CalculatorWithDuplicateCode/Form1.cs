using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorWithDuplicateCode
{
    public partial class Form1 : Form
    {
        double answerSoFar = 0;
        string operation = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // All Clear Button

            // Reset the Answer to Zero and Clear the Display and the Operation
            operation = "";
            answerSoFar = 0;
            textBoxDisplay.Text = "";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = textBoxDisplay.Text + '1';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = textBoxDisplay.Text + '2';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = textBoxDisplay.Text + '3';
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = textBoxDisplay.Text + '4';
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = textBoxDisplay.Text + '5';
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = textBoxDisplay.Text + '6';
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = textBoxDisplay.Text + '7';
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = textBoxDisplay.Text + '8';
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = textBoxDisplay.Text + '9';
        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = textBoxDisplay.Text + '0';
        }

        private void decimalButton_Click(object sender, EventArgs e)
        {
            textBoxDisplay.Text = textBoxDisplay.Text + '.';
        }

        private void plusbutton_Click(object sender, EventArgs e)
        {
            // If there isn't already an operation, we need to store the entered number in the answerSoFar.
            // If there *is* an operation, then we have to perform that operation between the answerSoFar
            // and the number sitting in the display.

            // This will *totally* not do what you think if I just keep hitting the + button.

            switch(operation)
            {
                case "+":
                    answerSoFar += Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "-":
                    answerSoFar -= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "*":
                    answerSoFar *= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "/":
                    answerSoFar /= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                default:
                    answerSoFar = Convert.ToDouble(textBoxDisplay.Text);
                    break;
            }

            // Clear the Textbox Display
            textBoxDisplay.Text = "";

            // Change the operation to plus

            operation = "+";
            
        }

        private void minusbutton_Click(object sender, EventArgs e)
        {

            // If there isn't already an operation, we need to store the entered number in the answerSoFar.
            // If there *is* an operation, then we have to perform that operation between the answerSoFar
            // and the number sitting in the display.

            // This will *totally* not do what you think if I just keep hitting the + button.
            switch (operation)
            {
                case "+":
                    answerSoFar += Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "-":
                    answerSoFar -= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "*":
                    answerSoFar *= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "/":
                    answerSoFar /= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                default:
                    answerSoFar = Convert.ToDouble(textBoxDisplay.Text);
                    break;
            }

            // Clear the Textbox Display
            textBoxDisplay.Text = "";

            // Change the operation to minus

            operation = "-";
        }

        private void timesbutton_Click(object sender, EventArgs e)
        {

            // If there isn't already an operation, we need to store the entered number in the answerSoFar.
            // If there *is* an operation, then we have to perform that operation between the answerSoFar
            // and the number sitting in the display.

            // This will *totally* not do what you think if I just keep hitting the + button.
            switch (operation)
            {
                case "+":
                    answerSoFar += Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "-":
                    answerSoFar -= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "*":
                    answerSoFar *= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "/":
                    answerSoFar /= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                default:
                    answerSoFar = Convert.ToDouble(textBoxDisplay.Text);
                    break;
            }

            // Clear the Textbox Display
            textBoxDisplay.Text = "";

            // Change the operation to times

            operation = "*";
        }

        private void dividebutton_Click(object sender, EventArgs e)
        {

            // If there isn't already an operation, we need to store the entered number in the answerSoFar.
            // If there *is* an operation, then we have to perform that operation between the answerSoFar
            // and the number sitting in the display.

            // This will *totally* not do what you think if I just keep hitting the + button.

            switch (operation)
            {
                case "+":
                    answerSoFar += Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "-":
                    answerSoFar -= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "*":
                    answerSoFar *= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "/":
                    answerSoFar /= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                default:
                    answerSoFar = Convert.ToDouble(textBoxDisplay.Text);
                    break;
            }

            // Clear the Textbox Display
            textBoxDisplay.Text = "";

            // Change the operation to divide

            operation = "/";
        }

        private void equalsbutton_Click(object sender, EventArgs e)
        {
            // We need to perform the stored operation, clear the operation, 
            // and display the answerSoFar
            switch (operation)
            {
                case "+":
                    answerSoFar += Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "-":
                    answerSoFar -= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "*":
                    answerSoFar *= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                case "/":
                    answerSoFar /= Convert.ToDouble(textBoxDisplay.Text);
                    break;
                default:
                    answerSoFar = Convert.ToDouble(textBoxDisplay.Text);
                    break;
            }

            operation = "";
            textBoxDisplay.Text = Convert.ToString(answerSoFar);
        }

    }
}
