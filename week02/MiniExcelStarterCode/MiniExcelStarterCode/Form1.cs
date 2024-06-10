using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniExcel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // If you pass this function the name of a textbox,
        // it will grab the current value of the textbox that matches
        // the name e.g. given 'A', returns the value of TextBoxA

        // Note:  if the text box is empty or you pass a character
        // that isn't a textbox name, this will return 0.

        private double getValue(char boxName)
        {
            string value;
            switch (boxName)
            {
                case 'A':
                    value = textBoxA.Text;
                    break;
                case 'B':
                    value = textBoxB.Text;
                    break;
                case 'C':
                    value = textBoxC.Text;
                    break;
                case 'D':
                    value = textBoxD.Text;
                    break;
                case 'E':
                    value = textBoxE.Text;
                    break;
                default:
                    value = "0";
                    break;
            }

            if (value == "") { value = "0"; }

            return Convert.ToDouble(value);
        }

        // If you pass this function a textbox, it will
        // look in the textbox, and it will pull out the 
        // first character and the third character, which 
        // should both be textbox names

        // It will pull out the second character as the operator

        // It will return the 'name' of the textbox in the formula
        // in either the first position or the second position.

        private char parseFormula(TextBox formulaBox, int n)
        {
            // grab the formula from the textbox
            string formula = formulaBox.Text;

            // If there aren't 3 characters in the formula, do nothing,
            // this isn't a valid formula
            if (formula.Length<3)
            {
                return ' ';
            }

            // First character is the first value, and third character
            // is the second value.   the operator is in the second spot

            char firstValue = formula[0];
            char secondValue = formula[2];
            char op = formula[1];  // for core assignment, op is always +

            // Returning value according request, now we include operation symbol
            char textBoxName=firstValue;
            switch (n)
            {
                case 1:textBoxName = op;break;
                case 3:
                    if (formula.Length == 5)
                    {
                        op = formula[3];
                        textBoxName = op; break;
                    }
   //                 MessageBox.Show("operador");
                    break;
                case 2:
                    textBoxName = secondValue;
                    break;
                case 4:
//                    textBoxName = thirdValue;
                    if (formula.Length ==5 )
                    {
                        char thirdValue = formula[4];
                        textBoxName = thirdValue;
                    }
                    break;
            }

            return textBoxName;

        }
/*
        // get the first name from a formula in a formula box (e.g. 'C' from C+D)
        private char getFirstFormulaBoxName(TextBox formulaBox)
        {
            return parseFormula(formulaBox, 1);
        }

        // get the second name from a formula in a formula box (e.g. 'D' from C+D)
        private char getSecondFormulaBoxName(TextBox formulaBox)
        {
            return parseFormula(formulaBox, 3);
        }

        // get the third name from a formula in a formula box (e.g. 'A' from C+D+A
        private char getThirdFormulaBoxName(TextBox formulaBox)
        {
            return parseFormula(formulaBox, 5);
        }

        private char getOperator(TextBox formulaBox,int n)
        {
            if (n == 1)
                return parseFormula(formulaBox, 2);
            else
                return parseFormula(formulaBox, 4);
        }
*/
        private char getFormulaElement(TextBox formulaBox, int n)
        {
            return parseFormula(formulaBox, n);
        }


        /*
        * FOR CORE TASK, I realized that event handler code generated automatically by double clicking the textbox could be renamed
        *          as "recalculate" then I could call it to handle all textbox.textchanged events, to do so I selected recalculate
        *          in properties>Events for each textbox. Then I just added code for printing all results according the corresponding formula.
	*	Initially I used the code below:

        *  private void recalculate(object sender, EventArgs e)
        *  {
        *        textBoxX.Text = Convert.ToString(getValue(getFirstFormulaBoxName(textBoxFormulaW)) + getValue(getSecondFormulaBoxName(textBoxFormulaW)));
        *        textBoxX.Text = Convert.ToString(getValue(getFirstFormulaBoxName(textBoxFormulaX)) + getValue(getSecondFormulaBoxName(textBoxFormulaX)));
        *        textBoxY.Text = Convert.ToString(getValue(getFirstFormulaBoxName(textBoxFormulaY)) + getValue(getSecondFormulaBoxName(textBoxFormulaY)));
        *        textBoxZ.Text = Convert.ToString(getValue(getFirstFormulaBoxName(textBoxFormulaZ)) + getValue(getSecondFormulaBoxName(textBoxFormulaZ)));
        *  }
        */

        /*          FOR EXTENSIONS:
         *          
         *          1) Because the function is the same, I decided to use the same event handler "recalculate" to handle formula changes, same procedure
         *              to link event/handler
         *              
         *          2) added an extra case for switch in getValue function
         *          
         *          3) added the function getOperator(), modified paseFormula() to reasign values to each element in formulas. then in recalculate()
         *              I added a switch to evaluate operator returnd by getOperator() and calculate accordingly.
         *              At this point, the code was growing up and I didn't want to write that much so I found a way to avoid repeating operator
         *              evaluation and printing results for each textBoxX by using nested foreach loops 1 for reading formula 1 for selecting the right
         *              textbox to print result.
         *          
         *          4) this one was really hard! FIRST , I added a function getThirdFormulaBoxName()to get 3rd name, modified parseFormula() as suggested,
         *             after that, and trying to simplify, I created a function to get all elements in formula and perform any operation
         *             (I don't know if I this really simplified things, might be just got more complicated :D
         *                
         *          5) done.
        */
        private void recalculate(object sender, EventArgs e)
        {
         // when an input or formula textbox changes its content, text in textbox for results is updated here
          foreach (TextBox tbx in this.Controls.OfType<TextBox>().Where(t => t.Name.StartsWith("textBoxFormula")))  // let's chek every formula textbox
            foreach (TextBox result in this.Controls.OfType<TextBox>().Where(x => x.Name.Last() == tbx.Name.Last() && x.Name.Length == 8)) // and use the right textbox for results
              if (tbx.TextLength == 3 || tbx.TextLength == 5)
              {
                 double total = 0;
                 for (int i = 1; i < tbx.TextLength; i+=2)
                 {
                   switch (getFormulaElement(tbx, i))
                   {
                      case '+':
                        if (i == 1)   // addition of members 1 and 2 in formula
                          total = getValue(getFormulaElement(tbx, 0)) + getValue(getFormulaElement(tbx, 2));
                        else          // if there's a 3rd member, it's added here to result of first 2 members operation
                          total=Convert.ToDouble(result.Text) + getValue(getFormulaElement(tbx, i + 1));
                      break;

                      case '*':
                        if (i == 1)   // multiplication of 2 firt members in formula
                          total = getValue(getFormulaElement(tbx, 0)) * getValue(getFormulaElement(tbx, 2));
                        else         // if a 3rd member exists in formula, then multiply it to result of first 2 members operation.
                          total = Convert.ToDouble(result.Text) * getValue(getFormulaElement(tbx, i + 1));
                      break;

                   }
                   result.Text = Convert.ToString(total);
                 }
              }

                            //if (tbx.TextLength == 3)
                            //    foreach (TextBox result in this.Controls.OfType<TextBox>().Where(x => x.Name.Last() == tbx.Name.Last() && x.Name.Length == 8))

                            //        switch (getOperator(tbx,1))
                            //        {
                            //            case '+':
                            //                result.Text = Convert.ToString(getValue(getFirstFormulaBoxName(tbx)) + getValue(getSecondFormulaBoxName(tbx)));

                            //                break;
                            //            case '*':
                            //                result.Text = Convert.ToString(getValue(getFirstFormulaBoxName(tbx)) * getValue(getSecondFormulaBoxName(tbx)));

                            //                break;
                            //        }
                            //else if (tbx.TextLength == 5)
                            //    foreach (TextBox result in this.Controls.OfType<TextBox>().Where(x => x.Name.Last() == tbx.Name.Last() && x.Name.Length == 8))
                            //       switch (getOperator(tbx,1))
                            //            case '+'
                            //                  result.Text = Convert.ToString( getValue(getFirstFormulaBoxName(tbx)) + getValue(getSecondFormulaBoxName(tbx)) + getValue(getThirdFormulaBoxName(tbx)) );


        }
    }
}

//switch (tbx.Name.Last())
//{
//    case 'W':
//        switch (getOperator(tbx))
//        {
//            case '+': textBoxW.Text = Convert.ToString(getValue(getFirstFormulaBoxName(tbx)) + getValue(getSecondFormulaBoxName(tbx))); break;
//            case '*': textBoxW.Text = Convert.ToString(getValue(getFirstFormulaBoxName(tbx)) * getValue(getSecondFormulaBoxName(tbx))); break;
//        }
//        break;
//    case 'X':
//        switch (getOperator(tbx))
//        {
//            case '+': textBoxX.Text = Convert.ToString(getValue(getFirstFormulaBoxName(tbx)) + getValue(getSecondFormulaBoxName(tbx))); break;
//            case '*': textBoxX.Text = Convert.ToString(getValue(getFirstFormulaBoxName(tbx)) * getValue(getSecondFormulaBoxName(tbx))); break;
//        }
//        break;

//    case 'Y':
//        switch (getOperator(tbx))
//        {
//            case '+': textBoxY.Text = Convert.ToString(getValue(getFirstFormulaBoxName(tbx)) + getValue(getSecondFormulaBoxName(tbx))); break;
//            case '*': textBoxY.Text = Convert.ToString(getValue(getFirstFormulaBoxName(tbx)) * getValue(getSecondFormulaBoxName(tbx))); break;
//        }
//        break;

//    case 'Z':
//        switch (getOperator(tbx))
//        {
//            case '+': textBoxZ.Text = Convert.ToString(getValue(getFirstFormulaBoxName(tbx)) + getValue(getSecondFormulaBoxName(tbx))); break;
//            case '*': textBoxZ.Text = Convert.ToString(getValue(getFirstFormulaBoxName(tbx)) * getValue(getSecondFormulaBoxName(tbx))); break;
//        }
//        break;
