using System;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args) {
            for (int i = 1; i <= 100; i++) {
                string? num = null;
                if (i%3 == 0)
                    num += "Fizz";
                if (i % 5 == 0)
                    num += "Buzz";
                Console.WriteLine(num ?? i.ToString());
            }
        }
    }
}
