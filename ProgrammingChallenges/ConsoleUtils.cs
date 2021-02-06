using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammingChallenges
{
    public class ConsoleUtils
    {
        public static int DisplayMenu(string prompt, params string[] items) {
            int index = 0;
            int n = items.Length;
            Console.Clear();
            ConsoleKey key = ConsoleKey.NoName;
            do {
                if (key == ConsoleKey.UpArrow) {
                    index--;
                } else if (key == ConsoleKey.DownArrow) {
                    index++;
                }
                index = (index % n + n) % n; // neat way to handle wrapping at the start and end. I'm so fking clever sometimes.

                Console.Clear();
                Console.WriteLine(prompt);
                for (int i = 0; i < items.Length; i++) {
                    if (i == index) {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write(">");
                        Console.WriteLine(items[i]);
                        Console.ResetColor();
                    } else {
                        Console.Write(" ");
                        Console.WriteLine(items[i]);
                    }
                }

            } while ((key = Console.ReadKey().Key) != ConsoleKey.Enter);
            return index;
        }
    }
}
