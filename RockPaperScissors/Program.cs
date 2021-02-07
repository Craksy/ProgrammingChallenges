using System;
using System.Collections.Generic;
using ProgrammingChallenges;


namespace RockPaperScissors
{
    class Program
    {
        static bool?[] matches = {null, false, true, false, true};
        static string[] names = { "Rock", "Paper", "Scissor", "Spock", "Lizard" };
        static Random random;

        static void Main(string[] args) {
            int mode;
            random = new Random();
            //Behold this marvel of indentation fuckery
            while ((mode = ConsoleUtils.DisplayMenu(
                    "What would you like to play?",
                    "Classic Rock, Paper, Scissor",
                    "Extended (+Spock, Lizard)",
                    "Exit")) != 2)
                Play(mode);
        }

        static void Play(int mode) {
            int maxIndex = 3 + 2 * mode; //this is the clean way. don't question it.
            do {
                Console.Clear();
                string[] choices = names[0..maxIndex];
                int playerChoice = ConsoleUtils.DisplayMenu("Pick your hand:", choices);
                int computerChoice = random.Next(maxIndex);
                bool? result = GetMatchResult(playerChoice, computerChoice);
                Console.WriteLine("Computer picked {0}", names[computerChoice]);
                if (result == true)
                    Console.WriteLine("Well yeah, {0} beats {1}, although you probably cheated... Let's try again?", names[playerChoice], names[computerChoice]);
                else if (result == false)
                    Console.WriteLine("{1} beats {0}. TAKE THAT HUMA... i mean\n*BEEP BOOP* Computer won. Play again?", names[playerChoice], names[computerChoice]);
                else
                    Console.WriteLine("We both picked {0}. It's a tie", names[playerChoice]);
                Console.ReadKey();
            } while (ConsoleUtils.DisplayMenu("Play again or go back?", "Let's go again", "Main menu, please") != 1);
        }

        static bool? GetMatchResult(int hand1, int hand2) {
            //return true if hand1 beats hand2, false if it loses, null if it's a tie
            int diff = hand2 - hand1;
            int matchIndex = (diff % 5 + 5) % 5; //C# may be the only language I've seen to implement a "remainder" operator rather than actual modulus.
            return matches[matchIndex];
        }
    }
}
