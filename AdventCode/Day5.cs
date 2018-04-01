using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventCode
{
    public class Day5
    {
        public static void RunDay5()
        {
            var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Inputs/input5.txt"));
            PartOne(lines.Select(int.Parse).ToList());
            PartTwo(lines.Select(int.Parse).ToList());
        }

        private static void PartOne(IList<int> jumps)
        {
            var possition = 0;
            var steps = 0;
            while (possition >= 0 && possition < jumps.Count)
            {
                possition += jumps[possition]++;
                steps++;
            }
            Console.WriteLine($"5.1 puzzle answer is: {steps}");
        }

        private static void PartTwo(IList<int> jumps)
        {
            var possition = 0;
            var steps = 0;
            while (possition >= 0 && possition < jumps.Count)
            {
                var nextPosition = possition + jumps[possition];

                if (jumps[possition] >= 3)
                {
                    jumps[possition]--;
                }
                else
                {
                    jumps[possition]++;
                }

                possition = nextPosition;
                steps++;
            }
            Console.WriteLine($"5.2 puzzle answer is: {steps}");
        }
    }
}
