using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021_Day9
{
    public static class Part2
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

            var basins = new List<int>();
            foreach (var item in heights)
            {
                var (x, y) = item.Key;
                if (heights.DefaultGet((x - 1, y), 9) > item.Value &&
                    heights.DefaultGet((x + 1, y), 9) > item.Value &&
                    heights.DefaultGet((x, y - 1), 9) > item.Value &&
                    heights.DefaultGet((x, y + 1), 9) > item.Value)
                {
                    var visited = new List<(int, int)>();
                    var toVisit = new Queue<(int x, int y)>();
                    toVisit.Enqueue(item.Key);

                    while (toVisit.Count > 0)
                    {
                        var next = toVisit.Dequeue();

                        foreach (var adj in new List<(int x, int y)> { (next.x - 1, next.y),
                            (next.x + 1, next.y), (next.x, next.y - 1), (next.x, next.y + 1)})
                        {
                            if (!visited.Contains(adj) && heights.DefaultGet(adj, 9) != 9)
                            {
                                toVisit.Enqueue(adj);
                            }
                        }
                        visited.Add(next);
                    }

                    basins.Add(visited.Distinct().ToArray().Length);
                }
            }

            return basins.OrderByDescending(x => x).Take(3).Aggregate(1, (x, y) => x * y);
        }

        private static int DefaultGet(this Dictionary<(int, int), int> dict, (int, int) value, int defaultValue)
        {
            if (dict.TryGetValue(value, out int output)) return output;

            return defaultValue;
        }
    }
}