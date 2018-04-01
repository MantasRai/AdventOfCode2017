using System;
using System.IO;
using System.Linq;

namespace AdventCode
{
    public class Day4
    {
        public static void RunDay4()
        {
            var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Inputs/input4.txt"));

            var sum = lines.Select(line => line.Split(' ').ToList()).Count(tags => tags.Distinct().Count() == tags.Count());
            Console.WriteLine($"4.1 puzzle answer is: {sum}");

            sum = 0;
            foreach (var line in lines)
            {
                var tags = line.Split(' ').ToList();

                var isUnique = tags.Distinct().Count() == tags.Count();

                var isAnagram = false;

                if (!isUnique) continue;
                for (var x = 0; x < tags.Count; ++x)
                {
                    for (var y = x + 1; y < tags.Count; ++y)
                    {
                        if (!IsAnagram(tags[x], tags[y])) continue;
                        isAnagram = true;
                        break;
                    }
                }
                if (!isAnagram)
                    sum += 1;
            }
            Console.WriteLine($"4.2 puzzle answer is: {sum}");
        }


        public static bool IsAnagram(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return false;
            if (s1.Length != s2.Length)
                return false;

            foreach (var c in s2)
            {
                var ix = s1.IndexOf(c);
                if (ix >= 0)
                    s1 = s1.Remove(ix, 1);
                else
                    return false;
            }
            return string.IsNullOrEmpty(s1);
        }
    }
}
