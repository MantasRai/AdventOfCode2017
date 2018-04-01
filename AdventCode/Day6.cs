using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    public class Day6
    {
        public static void RunDay6()
        {
            int[] input = { 11, 11, 13, 7, 0, 15, 5, 5, 4, 4, 1, 1, 7, 1, 15, 11 };
            Console.WriteLine($"6.1 puzzle answer is: {PartOne(input)}");
            Console.WriteLine($"6.2 puzzle answer is: {PartTwo(input)}");
        }

        public static string PartOne(int[] input)
        {
            var cycles = new List<int[]>();

            while (!cycles.Any(x => x.SequenceEqual(input)))
            {
                cycles.Add(input.ToArray());
                RedistributionCycles(input);
            }

            return cycles.Count.ToString();
        }

        private static void RedistributionCycles(IList<int> banks)
        {
            var index = banks.IndexOf(banks.Max());
            var blocks = banks[index];
            banks[index++] = 0;

            while (blocks > 0)
            {
                if (index >= banks.Count)
                {
                    index = 0;
                }
                banks[index++]++;
                blocks--;
            }
        }

        public static string PartTwo(int[] banks)
        {
            var cycles = new List<int[]>();

            while (!cycles.Any(x => x.SequenceEqual(banks)))
            {
                cycles.Add((int[])banks.Clone());
                RedistributionCycles(banks);
            }

            return (cycles.Count - cycles.IndexOf(cycles.First(x => x.SequenceEqual(banks)))).ToString();
        }
    }
}
