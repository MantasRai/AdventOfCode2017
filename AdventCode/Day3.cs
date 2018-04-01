using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCode
{
    internal static class Day3
    {
        public static void RunDay3()
        {
            ThirdDay1();
            ThirdDay2();
        }
        public static void ThirdDay1()
        {
            const int puzzleInput = 325489;
            var matrix = CreateMatrix(571, 571);
            var finalMatrix = new int[571, 571];

            foreach (var c in matrix)
            {
                finalMatrix[c.X, c.Y] = c.Val;
            }
            var finishPoint = CoordinatesOf(finalMatrix, puzzleInput);
            var startPoint = CoordinatesOf(finalMatrix, 1);
            Console.WriteLine($"3.1 puzzle answer is: {CountSum(finishPoint, startPoint)}");
        }

        public static void ThirdDay2()
        {
            const int puzzleInput = 325489;

            var matrix = CreateMatrix(10, 10);
            var finalMatrix = new int[11, 11];

            foreach (var c in matrix)
            {
                if (c.X == 0 || c.Y == 0) continue;
                if (c.Val == 1)
                {
                    finalMatrix[c.X, c.Y] = 1;
                }
                else
                {
                    finalMatrix[c.X, c.Y] = CountNeighboars(finalMatrix, c.X, c.Y);
                }
            }

            var answer = finalMatrix.Cast<int>().Where(x => x > puzzleInput).OrderBy(x => x).FirstOrDefault();

            Console.WriteLine($"3.2 puzzle answer is: {answer}");
            // PrintArray(matrix2);
        }

        private static int CountNeighboars(int[,] matrix, int i, int j)
        {
            var sum = 0;

            sum += matrix[i - 1, j];
            sum += matrix[i, j - 1];
            sum += matrix[i - 1, j - 1];
            sum += matrix[i + 1, j];
            sum += matrix[i, j + 1];
            sum += matrix[i + 1, j + 1];
            sum += matrix[i + 1, j - 1];
            sum += matrix[i - 1, j + 1];

            return sum;
        }

        public static int CountSum(Point p1, Point p2)
        {
            var sum = 0;

            if (p1.X > p2.X)
            {
                sum += p1.X - p2.X;
            }
            else
            {
                sum += p2.X - p2.X;
            }

            if (p1.Y > p2.Y)
            {
                sum += p1.Y - p2.Y;
            }
            else
            {
                sum += p2.Y - p1.Y;
            }
            return sum;
        }


        public static Point CoordinatesOf<T>(this T[,] matrix, T value)
        {
            var w = matrix.GetLength(0);
            var h = matrix.GetLength(1);

            for (var x = 0; x < w; ++x)
            {
                for (var y = 0; y < h; ++y)
                {
                    if (!matrix[x, y].Equals(value)) continue;
                    var points = new Point
                    {
                        X = x,
                        Y = y
                    };
                    return points;
                }
            }

            return new Point();
        }

        public struct Point
        {
            public Point(int x, int y, int val) : this()
            {
                X = x;
                Y = y;
                Val = val;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int Val { get; set; }
        }

        private static IEnumerable<Point> CreateMatrix(int width, int height)
        {
            var numElements = width * height + 1;
            var points = new Point[numElements];

            var x = 0;
            var y = 0;
            var val = width * height;
            var dx = 1;
            var dy = 0;
            var xLimit = width - 0;
            var yLimit = height - 1;
            var counter = 0;

            var currentLength = 1;
            while (counter < numElements)
            {
                points[counter] = new Point(x, y, val--);
                x += dx;
                y += dy;
                currentLength++;
                if (dx > 0)
                {
                    if (currentLength >= xLimit)
                    {
                        dx = 0;
                        dy = 1;
                        xLimit--;
                        currentLength = 0;
                    }
                }
                else if (dy > 0)
                {
                    if (currentLength >= yLimit)
                    {
                        dx = -1;
                        dy = 0;
                        yLimit--;
                        currentLength = 0;
                    }
                }
                else if (dx < 0)
                {
                    if (currentLength >= xLimit)
                    {
                        dx = 0;
                        dy = -1;
                        xLimit--;
                        currentLength = 0;
                    }
                }
                else if (dy < 0)
                {
                    if (currentLength >= yLimit)
                    {
                        dx = 1;
                        dy = 0;
                        yLimit--;
                        currentLength = 0;
                    }
                }
                counter++;
            }

            Array.Reverse(points);
            return points;
        }

        public static void PrintArray(int[,] data)
        {
            var rowLength = data.GetLength(0);
            var colLength = data.GetLength(1);
            var max = data.Cast<int>().Max().ToString().Length;

            for (var i = 0; i < rowLength; i++)
            {
                for (var j = 0; j < colLength; j++)
                {
                    Console.Write($"{data[i, j].ToString().PadLeft(max, '0')} ");
                }
                Console.Write(Environment.NewLine);
            }
        }
    }
}
