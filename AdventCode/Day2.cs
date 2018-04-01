using System;
using System.IO;
using System.Linq;

namespace AdventCode
{
    public class Day2
    {
        public static void RunDay2()
        {
            PartOneAndTwo();
        }

        private static void PartOneAndTwo()
        {
            var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Inputs/input2.txt"));
            var arrays = lines.Select(line =>
            {
                return line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => Convert.ToInt64(s)).Select(x => x).ToArray();
            }).ToList();

            var sum = arrays.Select(array => new {array, max = array.Max()})
                .Select(x => new {x, min = x.array.Min()})
                .Select(x => x.x.max - x.min).Sum();

            Console.WriteLine($"2.1 puzzle answer is: {sum}");

            var partTwoSum = arrays.SelectMany(array => array, (array, t1) => new {array, t1})
                .SelectMany(x => x.array, (x, t) => new {x, t})
                .Where(x => x.x.t1 != x.t)
                .Where(x => x.x.t1 % x.t == 0)
                .Select(x => x.x.t1 / x.t).Sum();
            Console.WriteLine($"2.2 puzzle answer is: {partTwoSum}");
        }
    }
}
