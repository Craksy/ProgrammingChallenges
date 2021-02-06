using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace TemperatureConverter
{
    class Program
    {

        enum TemperatureUnit { Celsius, Farenheit, Kelvin }
        static Dictionary<TemperatureUnit, string> unitNames;
        static Regex exprgx; //Expregexion. Yeah.

        static void Main(string[] args) {
            exprgx = new Regex(@"(?<value>\d+(\.\d*)?)\s?(?<from>[a-zA-Z]+) ((as|to) )?(?<to>[a-zA-Z]+)");
            unitNames = new Dictionary<TemperatureUnit, string>();
            unitNames.Add(TemperatureUnit.Celsius, "celsius");
            unitNames.Add(TemperatureUnit.Farenheit, "farenheit");
            unitNames.Add(TemperatureUnit.Kelvin, "kelvin");
            do {
                Console.Clear();
                Console.Write("Input>");
                ParseConversionString(Console.ReadLine());
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        static void ParseConversionString(string input) {
            Match match = exprgx.Match(input);
            GroupCollection groups = match.Groups;
            TemperatureUnit from, to;
            double value;
            try {
                from = ParseUnit(groups["from"].Value);
                to = ParseUnit(groups["to"].Value);
                value = double.Parse(groups["value"].Value);
            }catch(Exception e) {
                Console.WriteLine("Error: "+e.Message);
                return;
            }

            double result = Convert(value, from, to);
            Console.WriteLine("{0} {1} is equal to {2:F3} {3}", value, unitNames[from], result, unitNames[to]);
        }

        static TemperatureUnit ParseUnit(string unit) {
            unit.ToLower();
            foreach(var tempUnit in unitNames) {
                string name = tempUnit.Value;
                if (unit == name || unit == name.Substring(0, 1))
                    return tempUnit.Key;
            }
            throw new Exception("Unable to parse unit " + unit);
        }

        static double Convert(double value, TemperatureUnit from, TemperatureUnit to) {
            double result = 0;
            switch (from) {
                case TemperatureUnit.Celsius:
                    result = FromCelsius(value, to);
                    break;
                case TemperatureUnit.Farenheit:
                    result = FromFarenheit(value, to);
                    break;
                case TemperatureUnit.Kelvin:
                    result = FromKelvin(value, to);
                    break;
            }
            return result;
        }

        static double FromCelsius(double value, TemperatureUnit unit) {
            if (unit == TemperatureUnit.Farenheit)
                return 9f / 5f * value + 32;
            return value + 273;
        }

        static double FromFarenheit(double value, TemperatureUnit unit) {
            value = 5f / 9f * (value - 32);
            if (unit == TemperatureUnit.Kelvin)
                return value + 273;
            return value;
        }

        static double FromKelvin(double value, TemperatureUnit unit) {
            if (unit == TemperatureUnit.Celsius)
                return value - 273;
            return FromCelsius(value - 273, TemperatureUnit.Farenheit);
        }

    }
}
