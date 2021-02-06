using System;

namespace AgeInSeconds
{
    class Program
    {
        static void Main(string[] args) {
            do {
                Console.Clear();
                Console.WriteLine("When were you born?");
                Console.WriteLine("Format: yyyy-mm-dd [hh:mm:ss]");
                Console.Write("> ");
                try {
                    DateTime born = DateTime.Parse(Console.ReadLine());
                    TimeSpan span = DateTime.Now - born;
                    Console.WriteLine("You are {0:F2} seconds old!", span.TotalSeconds);
                } catch (Exception) {
                    Console.WriteLine("Incorrect format.");
                }
                Console.WriteLine("Press escape to quit. Any other key to try a different date.");
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }
}
