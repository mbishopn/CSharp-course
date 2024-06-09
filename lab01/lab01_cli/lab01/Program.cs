//          Intro to C# COMP1012
//          LAB01 - Part A - Console program
//          MANUEL BISHOP NORIEGA
//          STUDENT ID: 4362207

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab01
{
    class Program
    {
        static void Main(string[] args)
        {
            int number01, number02, number03;
            char op = ' ';
            Console.WriteLine("Enter number 1:");
            number01 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter number 2:");
            number02 = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Adding " + number01 + " + " + number02 + " = " + Convert.ToString(number01+number02));

            Console.WriteLine("Wanting to add another value to previous result? Please enter it");
            number03 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("But this time chose your operator '+' or '-': ");
            op = Convert.ToChar(Console.ReadLine());
            if (op == '+')
                Console.WriteLine("The final result is: " + Convert.ToString(number01 + number02 + number03));
            else
                if (op == '-')
                Console.WriteLine("The final result is: " + Convert.ToString(number01 + number02 - number03));
            else
                Console.WriteLine("Not a valid operation");

            /*
             •	Question 1:  What happens if you enter your name instead of a number?   Any idea why?
                Answer: I get an error (exception), because we are explicitly casting data type from string (readline default) to integer,
                        so if I enter a string I should convert it to string type.
            •	Question 2:  What happens if you enter a really really long number?
                Answer: I get an overflow exception, meaning that value entered overflows datatype variable capacity.
             */
        }
    }
}
