using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021_Day9
{
    public static class Part1
    {
        public static void Main(string[] args)
        {
            var input = @"2199943210
3987894921
9856789892
8767896789
9899965678
";

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
            var heights = new Dictionary<(int x, int y), int>();
            foreach (var line in input.Split("\r\n").Select((val, x) => (x, val)))
            {
                foreach (var c in line.val.Select((val, y) => (y, val - '0')))
                {
                    heights[(line.x, c.y)] = c.Item2;
                }
            }

            var minimas = 0;
            foreach (var item in heights)
            {
                var (x, y) = item.Key;
                if (heights.DefaultGet((x - 1, y), 10) > item.Value &&
                    heights.DefaultGet((x + 1, y), 10) > item.Value &&
                    heights.DefaultGet((x, y - 1), 10) > item.Value &&
                    heights.DefaultGet((x, y + 1), 10) > item.Value)
                {
                    minimas += item.Value + 1;
                }
            }

            return minimas;
        }

        private static int DefaultGet(this Dictionary<(int, int), int> dict, (int, int) value, int defaultValue)
        {
            if (dict.TryGetValue(value, out int output)) return output;

            return defaultValue;
        }
    }
}