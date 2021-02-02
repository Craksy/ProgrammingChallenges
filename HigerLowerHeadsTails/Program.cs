using System;

namespace HigerLowerHeadsTails
{
    class Program
    {
        static Random random;
        static void Main(string[] args) {
            random = new Random();
            int selection;
            while ((selection = DisplayMenu("Hello. What would you like to play", "Heads or Tails", "Higher or Lower", "Exit")) != 2) {
                if (selection == 0)
                    PlayHeadsAndTails();
                else if (selection == 1)
                    PlayHigherLower();
            }
        }

        static void PlayHeadsAndTails() {
            int coin, guess; //heads is 0, tails is 1
            do {
                guess = DisplayMenu("Make your guess!", "Heads", "Tails");
                coin = random.Next(2);
                string result = guess == coin ? "Way to go! You win!" : "Bummer. You guessed wrong.";
                Console.WriteLine(result);
                Console.ReadKey();
            } while (PlayAgainMenu());
        }

        static void PlayHigherLower() {
            int number, guess, attempts;
            string response;
            do {
                Console.Clear();
                number = random.Next(101);
                attempts = 0;
                Console.WriteLine("I'm thinking of a number between 0 and 100 (both inclusive)");
                Console.WriteLine("How many tries will you need to guess it?");
                while((guess = int.Parse(Console.ReadLine())) != number) {
                    attempts++;
                    response = number > guess ? "You need to go higher." : "No, it's lower than that";
                    Console.WriteLine("{0}: {1}", guess, response);
                }
                Console.WriteLine("You guessed it in {0} attempts.", attempts);
                Console.ReadKey();
            } while (PlayAgainMenu());
        }

        static bool PlayAgainMenu() { //GudNameSet()
            return DisplayMenu("Play again or return to main menu?", "Let's go again!", "Back to menu") == 0;
        }

        static int DisplayMenu(string prompt, params string[] items) {
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
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
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
