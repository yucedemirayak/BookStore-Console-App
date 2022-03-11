using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore
{
    public class LineMenu
    {
        private int selectedIndex;
        private string[] Options;
        private string Prompt;
        int promptLength;

        public LineMenu(string _Prompt, string[] _Options)
        {
            Prompt = _Prompt;
            Options = _Options;
            selectedIndex = 0;
            promptLength = _Options.Length;
        }
        private void DisplaySelected()
        {
            Console.Write(Prompt + " ");
            string currentOption = Options[selectedIndex];
            Console.Write($"<-{currentOption}->");
        }
        private static void ClearCurrentConsoleLine(int _promptLength)
        {            
            Console.SetCursorPosition(_promptLength, Console.CursorTop);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                DisplaySelected();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.RightArrow)
                {
                    selectedIndex++;
                    if (selectedIndex == Options.Length)
                    {
                        selectedIndex = 0;
                    }
                }
                else if (keyPressed == ConsoleKey.LeftArrow)
                {
                    selectedIndex--;
                    if (selectedIndex == -1)
                    {
                        selectedIndex = Options.Length - 1;
                    }
                }
                if (keyPressed != ConsoleKey.Enter)
                {
                    ClearCurrentConsoleLine(promptLength);
                }
            } while (keyPressed != ConsoleKey.Enter);
            Console.WriteLine("");
            return selectedIndex;
        }
    }
}
