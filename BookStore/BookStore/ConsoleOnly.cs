using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore
{
    public class ConsoleOnly
    {
        public static double TypeDouble()
        {
            string stringCheck = "";
            double outputValue;
            ConsoleKeyInfo inputKey;
            do
            {
                inputKey = Console.ReadKey(true);
                if (inputKey.Key == ConsoleKey.Decimal)
                {
                    if (!stringCheck.Contains(","))
                    {
                        stringCheck += ",";
                        Console.Write(",");
                    }
                }
                else if (inputKey.Key == ConsoleKey.OemPeriod)
                {
                    if (!stringCheck.Contains(","))
                    {
                        stringCheck += ",";
                        Console.Write(",");
                    }
                }
                else if (inputKey.Key == ConsoleKey.Backspace)
                {
                    if (stringCheck.Length > 0)
                    {
                        stringCheck = stringCheck.Remove(stringCheck.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    double typeCheck = 0;
                    bool check = double.TryParse(inputKey.KeyChar.ToString(), out typeCheck);
                    if (check)
                    {
                        stringCheck += inputKey.KeyChar.ToString();
                        Console.Write(inputKey.KeyChar);
                    }
                }
            }
            while (inputKey.Key != ConsoleKey.Enter);
            Console.Write("\n");
            if (stringCheck == "")
            {
                outputValue = 0;
            }
            else
            {
                outputValue = Convert.ToDouble(stringCheck);
            }
            return outputValue;
        }
        public static int TypeInt()
        {
            string stringCheck = "";
            int outputValue = 0;
            ConsoleKeyInfo inputKey;
            do
            {
                inputKey = Console.ReadKey(true);
                if (inputKey.Key != ConsoleKey.Backspace)
                {
                    int typeCheck = 0;
                    bool check = int.TryParse(inputKey.KeyChar.ToString(), out typeCheck);
                    if (check)
                    {
                        stringCheck += inputKey.KeyChar;
                        Console.Write(inputKey.KeyChar);
                    }
                }
                else
                {
                    if (inputKey.Key == ConsoleKey.Backspace && stringCheck.Length > 0)
                    {
                        stringCheck = stringCheck.Remove(stringCheck.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }            
            while (inputKey.Key != ConsoleKey.Enter);
            Console.Write("\n");

            if (stringCheck == "")
            {
                outputValue = 0;
            }
            else
            {
                outputValue = Convert.ToInt32(stringCheck);
            }            
            return outputValue;
        }
        public static string TypeString()
        {
            string stringCheck = "";
            ConsoleKeyInfo inputKey;
            do
            {
                inputKey= Console.ReadKey(true);
                if (inputKey.Key != ConsoleKey.Backspace)
                {
                    stringCheck += inputKey.KeyChar;
                    Console.Write(inputKey.KeyChar);
                }                
                else
                {
                    if (inputKey.Key == ConsoleKey.Backspace && stringCheck.Length > 0)
                    {
                        stringCheck = stringCheck.Remove(stringCheck.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            } while (inputKey.Key != ConsoleKey.Enter);
            string outputValue = stringCheck;
            return outputValue;
        }
    }
}
