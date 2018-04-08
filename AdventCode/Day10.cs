using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventCode
{
    public static class Day10
    {
        public static void RunDay10()
        {
            var input = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Inputs/input10.txt"));
            var integerStrings = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var partOneResult = PartOne(integerStrings);
            var partTwoResult = PartTwo(input);

            Console.WriteLine($"9.1 puzzle answer is: {partOneResult}");
            Console.WriteLine($"9.2 puzzle answer is: {partTwoResult}");
        }

        private static int PartOne(IReadOnlyList<string> integerStrings)
        {
            var integers = new int[integerStrings.Count];
            for (var n = 0; n < integerStrings.Count; n++)
                integers[n] = int.Parse(integerStrings[n]);

            var list = new int[256];
            for (var i = 0; i < list.Length; i++)
                list[i] = i;

            var position = 0;
            var skipIndex = 0;

            RunReverse(list, integers, ref position, ref skipIndex);

            return list[0] * list[1];
        }

        public static string PartTwo(string input)
        {
            var lengths = ConvertToBytes(input);

            lengths = lengths.Concat(new[] { 17, 31, 73, 47, 23 }).ToArray();

            var list = new int[256];
            for (var i = 0; i < list.Length; i++)
                list[i] = i;

            var position = 0;
            var skipSize = 0;

            for (var i = 0; i < 64; i++)
            {
                RunReverse(list, lengths, ref position, ref skipSize);
            }

            return GetHex(GetHash(list));
        }

        private static int[] ConvertToBytes(string input)
        {
            var lengths = new int[input.Length];
            var i = 0;
            foreach (var c in input)
            {
                lengths[i] = (byte)c;
                i++;
            }
            return lengths;
        }

        private static void RunReverse(IList<int> list, IEnumerable<int> lengths, ref int position, ref int skipIndex)
        {
            foreach (var length in lengths)
            {
                var elementsToReverse = new List<int>();
                var currentPosition = position;
                for (var i = 0; i < length; i++)
                {
                    elementsToReverse.Add(list[currentPosition]);
                    currentPosition = (currentPosition + 1) % list.Count;
                }

                elementsToReverse.Reverse();

                currentPosition = position;
                for (var i = 0; i < length; i++)
                {
                    list[currentPosition] = elementsToReverse[i];
                    currentPosition = (currentPosition + 1) % list.Count;
                }

                position = (position + length + skipIndex++) % list.Count;
            }
        }

        private static int[] GetHash(IReadOnlyList<int> list)
        {
            var denseHash = new int[16];
            for (var i = 0; i < 256; i += 16)
            {
                var xoredValue = list[i];
                for (var j = 1; j < 16; j++)
                {
                    xoredValue ^= list[j + i];
                }
                denseHash[i / 16] = xoredValue;
            }
            return denseHash;
        }

        private static string GetHex(int[] hash)
        {
            var hex = new StringBuilder();
            for (var i = 0; i < 16; i++)
            {
                hex.Append(hash[i].ToString("X2"));
            }
            return hex.ToString().ToLower();
        }
    }
}