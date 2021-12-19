using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021_Day5
{
    public static class Part2
    {
        public static void Main(string[] args)
        {
            var input = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2";

            /* Run with sample input if any argument is supplied */
            if (args.Length == 1)
            {
                input = File.ReadAllText("./input.txt");
            }

            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            var result = RunPart(input);
            Console.WriteLine($"Result: {result}. Execution time: {watch.ElapsedMilliseconds} ms");

            Console.ReadKey();
        }

        public static int RunPart(string input)
        {
            Dictionary<(int, int), int> grid = new();
            foreach (var line in input.Split("\r\n"))
            {
                var split = line
                    .Split(" -> ")
                    .Select(x => x.Split(","))
                    .Select(x => (x: int.Parse(x[0]), y: int.Parse(x[1])))
                    .ToList();

                var p1 = split[0];
                var p2 = split[1];

                if (p1.x == p2.x)
                {
                    foreach (var y in Enumerable.Range(Math.Min(p1.y, p2.y), Math.Max(p1.y, p2.y) - Math.Min(p1.y, p2.y) + 1))
                    {
                        grid.IncrementPoint((p1.x, y));
                    }
                }
                else if (p1.y == p2.y)
                {
                    foreach (var x in Enumerable.Range(Math.Min(p1.x, p2.x), Math.Max(p1.x, p2.x) - Math.Min(p1.x, p2.x) + 1))
                    {
                        grid.IncrementPoint((x, p1.y));
                    }
                }
                else
                {
                    var point = (p1.x, p1.y);
                    var dx = p1.x < p2.x ? 1 : -1;
                    var dy = p1.y < p2.y ? 1 : -1;

                    while (point != p2)
                    {
                        grid.IncrementPoint(point);
                        point.x += dx;
                        point.y += dy;
                    }

                    grid.IncrementPoint(point);
                }
            }

            return grid.Values.Count(x => x > 1);
        }

        private static void IncrementPoint(this Dictionary<(int, int), int> dict, (int, int) point)
        {
            if (!dict.ContainsKey(point))
            {
                dict[point] = 1;
            }
            else
            {
                dict[point]++;
            }
        }
    }
}