using System;
using System.IO;

namespace AdventCode
{
    public static class Day9
    {
        public static void RunDay9()
        {
            var input = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Inputs/input9.txt"));

            int garbageCount;
            var score = CountScore(input, out garbageCount);
            
            Console.WriteLine($"9.1 puzzle answer is: {score}");
            Console.WriteLine($"9.2 puzzle answer is: {garbageCount}");
        }

        public static int CountScore(string input, out int garbageCount)
        {
            var score = 0;
            var deepLevel = 0;
            garbageCount = 0;

            for (var index = 0; index < input.Length;)
            {
                switch (input[index])
                {
                    case '!':
                        index += 2;
                        break;
                    case '{':
                        deepLevel++;
                        index++;
                        break;
                    case '}':
                        score += deepLevel;
                        deepLevel--;
                        index++;
                        break;
                    case '<':
                        index = DealWithGarbage(input, index, ref garbageCount);
                        break;
                    default: index++;
                        break;
                }
            }
            return score;
        }

        private static int DealWithGarbage(this string input, int index, ref int garbageCounter)
        {
            var i = index + 1;
            while (true)
            {
                switch (input[i])
                {
                    case '!':
                        i += 2;
                        break;
                    case '>':
                        return i + 1;
                    default:
                        i++;
                        garbageCounter++;
                        break;
                }
            }
        }
    }
}