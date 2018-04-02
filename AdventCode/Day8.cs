using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    public static class Day8
    {
        public static void RunDay8()
        {
            var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Inputs/input8.txt"));
            var linesWithSpliter = lines.Select(x => x.Split(' '));

            var registers = new Dictionary<string, int>();

            var highestValueHeld = int.MinValue; //Part Two

            foreach (var splitedValues in linesWithSpliter)
            {
                if (!registers.ContainsKey(splitedValues[0]))
                    registers.Add(splitedValues[0], 0);

                if (!registers.ContainsKey(splitedValues[4]))
                    registers.Add(splitedValues[4], 0);

                if (Compere(splitedValues[5], registers[splitedValues[4]], int.Parse(splitedValues[6])))
                {
                    registers[splitedValues[0]] += IncDecValue(splitedValues[1], int.Parse(splitedValues[2]));
                }

                //Part two
                if (registers.Values.Max() > highestValueHeld)
                    highestValueHeld = registers.Values.Max();

            }
            Console.WriteLine($"8.1 puzzle answer is: {registers.Values.Max()}");
            Console.WriteLine($"8.2 puzzle answer is: {highestValueHeld}");
        }

        public static int IncDecValue(this string instruction, int value)
        {
            return string.Equals(instruction, "INC", StringComparison.OrdinalIgnoreCase) ? value : -value;
        }

        public static bool Compere(this string operation, int x, int y)
        {
            switch (operation)
            {
                case ">": return x > y;
                case ">=": return x >= y;
                case "<": return x < y;
                case "<=": return x <= y;
                case "!=": return x != y;
                case "==": return x == y;
                default: throw new Exception("Invalid operation in Day8 puzzle");
            }
        }
    }
}