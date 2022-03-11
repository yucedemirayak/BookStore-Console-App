using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore
{
     public class MenuHorizontal
    {
        private int SelectedIndex;
        private string[] Options;
        private string Prompt;

        public MenuHorizontal(string _Prompt, string[] _Options)
        {
            Prompt = _Prompt;
            Options = _Options;
            SelectedIndex = 0;
        }

        private void DisplayOptions ()
        {
            Console.WriteLine(Prompt);
            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                if (i == SelectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.Write($"<<{currentOption}>>");
                if (i < Options.Length - 1)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("  ");
                }
            }
            Console.ResetColor();
        }
        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);                
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.RightArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.LeftArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = 0;
                    }
                }

            } while (keyPressed != ConsoleKey.Enter);
            return SelectedIndex;
        }
    }
}
