using System;

namespace NameGenerator
{
    class Program
    {
        static Random random;
        static string[] syllables =
        {
            "ai", "ad", "an", "ae", "ad", "as", "am", "ar", "arth",
            "bal", "band", "bar", "bel", "breth", "brith", "bra", "bor",
            "ca", "cal", "car", "cel", "cer", "cirth",
            "del", "din", "dúr", "da", "dil",
            "eg", "en", "eb", "ed", "e", "ech", "eith", "el",
            "fal", "far", "fin", "fir", "for", "fuin",
            "glos", "gol", "gor", "gal", "gil", "gurth", "gaur", "gwa",
            "lómë", "lu", "laer",
            "il",
            "nath", "nu", "nan", "na", "nden", "ni", "nin", "naug",
            "oth", "ona", "od", "on", "or",
            "par", "pel", "per", "pal", "parf",
            "qua", "quen", "quel",
            "rog",
            "sil", "sau",
            "ma", "mel", "min", "mith", "muin",
            "to", "than", "tan", "tar", "thas", "ten", "tin", "thal", "tui", "taur",
            "u", "úl", "ul", "ur",
            "va", "val"
        };
        static void Main(string[] args) {
            random = new Random();
            ConsoleKey key;
            Console.WriteLine("Press <Q> or <ESC> to quit. Any other key to generate another name");
            while(true) {
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.Q || key == ConsoleKey.Escape)
                    break;
                Console.Clear();
                Console.WriteLine("Press <Q> or <ESC> to quit. Any other key to generate another name");
                Console.Write(GenerateName(random.Next(2, 5)));
            }
        }

        static string GenerateName(int syllables) {
            string[] syllableList = new string[syllables];
            for (int i = 0; i < syllables; i++) {
                syllableList[i] = SampleSyllable();
            }
            var name = string.Concat(syllableList);
            return name[0].ToString().ToUpper() + name.Substring(1);
        }

        static string SampleSyllable() {
            return syllables[random.Next(0, syllables.Length - 1)];
        }
    }
}
