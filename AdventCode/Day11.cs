using System;
using System.IO;

namespace AdventCode
{
    public static class Day11
    {
        public static void RunDay11()
        {
            var input = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Inputs/input11.txt"));
            var directions = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var x = 0;
            var y = 0;
            var z = 0;
            var maxDistance = 0;

            foreach (var direction in directions)
            {
                Coordinates(direction, ref x, ref y, ref z);

                var newDistance = Distance(x, y, z);
                if (newDistance > maxDistance)
                {
                    maxDistance = newDistance;
                }
            }

            Console.WriteLine($"9.1 puzzle answer is: {Distance(x, y, z)}");
            Console.WriteLine($"9.2 puzzle answer is: {maxDistance}");
        }

        private static void Coordinates(string direction, ref int x, ref int y, ref int z)
        {
            if (direction == "n")
            {
                y++;
                z--;
            }
            else if (direction == "ne")
            {
                x++;
                z--;
            }
            else if (direction == "nw")
            {
                x--;
                y++;
            }
            else if (direction == "s")
            {
                y--;
                z++;
            }
            else if (direction == "se")
            {
                x++;
                y--;
            }
            else if (direction == "sw")
            {
                x--;
                z++;
            }
        }

        private static int Distance(int x, int y, int z)
        {
            return (Math.Abs(x) + Math.Abs(y) + Math.Abs(z)) / 2;
        }
    }
}