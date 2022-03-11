using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore
{
    public class BookTypeMenu
    {
        private int selectedIndex;
        private List<string> bookTypeList = new List<string>();
        public BookTypeMenu()
        {
            foreach (int i in Enum.GetValues(typeof(BookTypeEnums)))
            {
                bookTypeList.Add(Enum.GetName(typeof(BookTypeEnums), i));
            }
            selectedIndex = 0;
        }
        private void DisplaySelected()
        {
            string currentType = bookTypeList[selectedIndex];
            Console.Write("Type : ");
            Console.Write($"<--{currentType}-->");            
        }
        private static void ClearCurrentConsoleLine()
        {
            Console.WriteLine("");
            Console.SetCursorPosition(7, Console.CursorTop - 1);
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
                    if (selectedIndex == bookTypeList.Count)
                    {
                        selectedIndex = 0;
                    }                
                }
                else if (keyPressed == ConsoleKey.LeftArrow)
                {
                    selectedIndex--;
                    if (selectedIndex == -1)
                    {
                        selectedIndex = bookTypeList.Count - 1;
                    }                    
                }
                if (keyPressed != ConsoleKey.Enter)
                {
                    ClearCurrentConsoleLine();
                }
            } while (keyPressed != ConsoleKey.Enter);
            Console.WriteLine("");
            return selectedIndex;
        }
    }
}
