using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021_Day12
{
    public static class Part1
    {
        public static void Main(string[] args)
        {
            var input = @"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc";

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
            var lines = input.Split("\r\n");
            var graph = new Dictionary<string, HashSet<string>>();

            foreach (var line in lines)
            {
                var path = line.Split("-");
                var from = path[0]; var to = path[1];

                graph.DefaultAdd(to, from);
                graph.DefaultAdd(from, to);
            }

            var totalPaths = 0;
            var toVisit = new Stack<List<string>>();
            toVisit.Push(new List<string> { "start" });

            while (toVisit.Count > 0)
            {
                var ongoingPath = toVisit.Pop();

                if (ongoingPath.Last() == "end")
                {
                    totalPaths++;
                    continue;
                }

                foreach (var adj in graph[ongoingPath.Last()])
                {
                    var isLower = !adj.Any(char.IsUpper);
                    if ((isLower && !ongoingPath.Contains(adj)) || !isLower)
                    {
                        var newPath = new List<string>();
                        newPath.AddRange(ongoingPath);
                        newPath.Add(adj);
                        toVisit.Push(newPath);
                    }
                }
            }

            return totalPaths;
        }

        private static void DefaultAdd(this Dictionary<string, HashSet<string>> dict, string key, string value)
        {
            if (dict.TryGetValue(key, out var stack))
            {
                stack.Add(value);
                return;
            }

            dict[key] = new HashSet<string>() { value };
        }
    }
}