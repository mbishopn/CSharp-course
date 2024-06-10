using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InputValidation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            maskedTextBox2.Mask = "00/00/0000";
            maskedTextBox2.ValidatingType = typeof(System.DateTime);
            toolTip1.IsBalloon = true;

            // Hint A1 -- set the mask and the validating type of the Age entry box here
            //------ CORE TASKS 1 AND 2 ---- adding a new MaskedTextBox for two-digit numbers of type int32
            maskedTextBox3.Mask = "00";
            maskedTextBox3.ValidatingType = typeof(System.Int32);
            maskedTextBox3.MaskInputRejected += maskedTextBox3_MaskInputRejected;
            maskedTextBox3.TypeValidationCompleted += maskedTextBox3_TypeValidationCompleted;
           

        }



        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (maskedTextBox2.MaskFull)
            {
                toolTip2.ToolTipTitle = "Input Rejected -- Too much data";
                toolTip2.Show("input mask is full, stop typing!", maskedTextBox2, 0, 50, 2000);
            }
            else if (e.Position == maskedTextBox2.Mask.Length)
            {
                toolTip2.ToolTipTitle = "Input Rejected";
                toolTip2.Show("You cannot add any more characters at the end of this field", maskedTextBox2, 0, 50);

            }
            else
            {
                toolTip2.ToolTipTitle = "Input Rejected";
                toolTip2.Show("You can only add numeric characters into this date field", maskedTextBox2, 0, 50);
            }
        }

        // Hint A2 -- create a MaskInputRejected event handler for your age entry box
        // Just show a generic error message for anything saying that you can only enter numbers of up to two digits
        //-------------   C O R E   T A S K   3 ------------ MaskInputRejected event handler
        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (maskedTextBox3.MaskFull)
            {
                toolTip2.ToolTipTitle = "Input rejected -- field already full";
                toolTip2.Show("don't type anymore, I'm full", maskedTextBox3, 0, 20, 1500);
            }
            else if (e.Position == maskedTextBox3.Mask.Length)
            {
                toolTip2.ToolTipTitle = "Input Rejected -- field already full";
                toolTip2.Show("Only 2 characters required", maskedTextBox3, 0, 20, 1500);
            }
            else
            {
                toolTip2.ToolTipTitle = "Input Rejected";
                toolTip2.Show("Only digits here please", maskedTextBox3, 0, 20, 1500);
            }
        }


        private void maskedTextBox2_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if(!e.IsValidInput)
            {
                toolTip1.ToolTipTitle = "Invalid Date";
                toolTip1.Show("That isn't a valid date in the format specified", maskedTextBox2, 0, -70, 2000);
            } else
            {
                // Here we can perform additional validation -- e.g. make sure that the date is in the future
                DateTime userDate = (DateTime)e.ReturnValue;
                if(userDate < DateTime.Now)
                {
                    toolTip1.ToolTipTitle = "Invalid Date";
                    toolTip1.Show("Only dates in the future are accepted, sorry!", maskedTextBox2, 0, -70, 2000);
                    e.Cancel = true;
                }
            }
        }
        // Hint A3 -- create a TypeValidationCompleted handler for your age entry box
        // First, make sure that it is in fact an integer -- although hard to get that entry error past the entry mask
        // Second, make sure that the age is >=13 and if not show a tooltip about this being a PG-13 form.
        //---------- C O R E    T A S K    4 ----------
        private void maskedTextBox3_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            if (!e.IsValidInput)
            {
                toolTip1.ToolTipTitle = "Invalid Age";
                toolTip1.Show("That isn't a valid age in the format specified", maskedTextBox3, 0, 0, 1500);
            }
            else
            {
                int age = (int)e.ReturnValue;
                if (age<13)
                {
                    toolTip1.ToolTipTitle = "Forbidden for your Age";
                    toolTip1.Show("This content can't be show to people below 13yo", maskedTextBox2, 0, 0, 1500);
                    e.Cancel = true;
                }
            }

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidEmailAddress(textBox1.Text, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                textBox1.Select(0, textBox1.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                this.errorProvider1.SetError(textBox1, errorMsg);
            }
        }

        public bool ValidEmailAddress(string emailAddress, out string errorMessage)
        {
            // Confirm that the email address string is not empty.
            if (emailAddress.Length == 0)
            {
                errorMessage = "email address is required.";
                return false;
            }

            // Confirm that there is an "@" and a "." in the email address, and in the correct order.
            if (emailAddress.IndexOf("@") > -1)
            {
                if (emailAddress.IndexOf(".", emailAddress.IndexOf("@")) > emailAddress.IndexOf("@"))
                {
                    errorMessage = "";
                    return true;
                }
            }

            errorMessage = "email address must be valid email address format.\n" +
               "For example 'someone@example.com' ";
            return false;
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            // If all conditions have been met, clear the ErrorProvider of errors.
            errorProvider1.SetError(textBox1, "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int firstValue;
            if(!Int32.TryParse(textBox2.Text,out firstValue))
            {
                textBox2.BackColor = Color.Red;
                textBox4.Text = "Error: first operand could not be parsed";
                return;
            }

            textBox2.BackColor = Color.White;
            int secondValue;

            try
            {
                secondValue = Int32.Parse(textBox3.Text);

                if (secondValue < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                int answer = firstValue + secondValue;

                textBox4.Text = answer.ToString();
            } catch (FormatException ex)
            {
                MessageBox.Show("Second operand is not well formatted, please in put an integer!", "Format Error");
            } catch (OverflowException ex)
            {
                MessageBox.Show("Second operand is either too large or too small!", "Overflow Error");
            } catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Second operand is below zero.  I don't add negatives.", "Negative Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unanticipated Error", "Weird Error");
            }


        }


        private int calculateFunction(string[] formulaBits)
        {

            // Hint B3 -- the formula should be an integer, then a + or -, then another integer.
            // So we are expecting an array of length 3
            // If the array is *not* of length 3, throw a new  Argument exception
            //if (formulaBits.Length != 3)
            //{
            //    throw new ArgumentException();  //---- C O R E   T A S K   7 -----
            //}

            int firstValue = Int32.Parse(formulaBits[0]);
            int secondValue = Int32.Parse(formulaBits[2]);

            // Hint B5 -- if either are below zero, throw the ArgumentOutOfRange Exception
            //if(firstValue<0 || secondValue<0)
            //{
            //    throw new ArgumentOutOfRangeException("both operands must be positive");
            //}
            int answer;

            switch(formulaBits[1])
            {
                case "+":
                    answer = firstValue + secondValue;
                    break;
                case "-":
                    answer = firstValue - secondValue;
                    break;
                case "/": /// ----------------C O R E   T A S K   8   AND   E X T E N S I O N    2 ------------
                case "*":
                    //answer = 0;
                    throw new InvalidOperationException("Operation not supported");
                    // Hint B4 -- If we didn't get an expected Operator, let's not calculate
                    // Instead, we can throw a new InvalidOperationException
                    break;
                default:
                    throw new InvalidOperationException("is that at least a valid operator?");
            }

            // Hint B6 -- if the answer is below zero, throw a new ArgumentOutOfRange Exception
            if (answer <0)
            {
                throw new ArgumentOutOfRangeException("I don't like negative answers :D");
            }
            return answer;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string formula = textBox5.Text;
            string[] formulaBits = formula.Split(' ');

            //------ C O R E   T A S K   5 -----
            // Hint B1 Need to wrap the below in a try - catch block
            try
            {
                textBox6.Text = calculateFunction(formulaBits).ToString();

            }
            //------ C O R E   T A S K   6 -----
            catch (FormatException ex)
            {
                MessageBox.Show("wrong format in operands, only use positive integers", "FORMAT ERROR");
            }
            catch (OverflowException ex)
            {
                MessageBox.Show("that number is too big...", "OVERFLOW ERROR");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // ---------- E X T E N S I O N   1 ------- with this exception I found that I can use ParamName too
                //MessageBox.Show(ex.Message);
                MessageBox.Show(ex.ParamName, "OUT OF RANGE ARGUMENTS/ANSWER");
            }

            // --------- C O R E   T A S K   7 ----- I've found that I should put this after ArgumentOutOfRangeException
            //----- otherwise all argument execptions are handled here
            catch (ArgumentException ex)
            {
                MessageBox.Show("Formula MUST have 3 arguments (2 operands and 1 operator)", "INCORRECT NUMBER OF ARGS");
            }

            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "INVALID OPERATION EXCEPTION");
            }

            finally
            {

            }
            // Hint B2  
            // Make sure you catch the FormatException and OverflowException that might be thrown by the Parse function

            // Make sure you catch the Argument exception in case there formula doesn't have the right structure  Hint B3a
            // Make sure you catch the InvalidOperationException in case the formula doesn't have the right symbol. Hint B4a
            // Make sure you catch the ArgumentOutOfRangeException because we don't like numbers below zero. Hint B5a / B6a

            // In each different exception that you catch, use a MessageBox.Show() statement like in the button1_Click handler.
            
        }

        // -----------------------      E X T E N S I O N    4         -------------------------
        // ----------  R e w r i t i n g   c a l c u l a t e   F u n c t i o n    ---------------

        // calcf2 returns now boolean value true if everything's OK or false if something fails.
        // add to out arguments (they must be declared outside function), answer holds integer value
        // if operations is correct and ErrMessage hold any error messages if there's a problem.
        private bool calcF2(string[] formulaBits, out int answer, out string ErrMessage)
        {
            answer = 0;ErrMessage = "ok";

            if (formulaBits.Length != 3)
            { ErrMessage = "Incorrect number of arguments in formula"; return false; }

            int firstValue, secondValue;

            // it turns out that if I use next commented line for if statement, I must initialize secondValue or when I try to run
            // it gives error saying that secondValue is unassigned therefore I can't use it inside an if statement so I have 2
            // options, initializing secondValue in the previous line or splitting condition to check operands separatedly

            // if (!(Int32.TryParse(formulaBits[0],out firstValue) && Int32.TryParse(formulaBits[2],out secondValue)))

            if (!Int32.TryParse(formulaBits[0], out firstValue))
            {
                ErrMessage = "Invalid Operand 1, use only positive integers";
                //return false;
            }
            if (!Int32.TryParse(formulaBits[2], out secondValue))
            {
                ErrMessage = "Invalid Operand 2, use only positive integers";
            }

            // ------ H E R E   I    G E T   T H E   E R R O R  for unassigned secondValue-------
            if (firstValue < 0 || secondValue < 0)
            {
                ErrMessage = "both operands must be positive";
            }

            switch (formulaBits[1])
            {
                case "+":
                    answer = firstValue + secondValue;
                    break;
                case "-":
                    answer = firstValue - secondValue;
                    break;
                case "/":
                case "*":
                    ErrMessage = "Operation not supported";
                    break;
                default:
                    ErrMessage = "is that at least a valid operator?";
                    break;
            }

            // Hint B6 -- if the answer is below zero, throw a new ArgumentOutOfRange Exception
            if (answer < 0)
            {
                ErrMessage = "I don't like negative answers :D";
                return false;
            }

            if(ErrMessage!="ok")
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            string formula = textBox5.Text;
            string[] formulaBits = formula.Split(' ');
            int answer;
            string ErrMessage;

            if (calcF2(formulaBits, out answer, out ErrMessage))
            { 
                textBox6.Text=answer.ToString();
            }
            else {
                MessageBox.Show(ErrMessage, "ERROR DETECTED");
            }
        }
    }
}
