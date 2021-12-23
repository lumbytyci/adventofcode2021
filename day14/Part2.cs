using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021_Day14
{
    public static class Part2
    {
        public static void Main(string[] args)
        {
            var input = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

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

        public static long RunPart(string input)
        {
            var lines = input.Split("\r\n\r\n");
            var rules = new Dictionary<string, string>();

            foreach (var rule in lines[1].Split("\r\n"))
            {
                var lineSplit = rule.Split(" -> ");
                rules.Add(lineSplit[0], lineSplit[1]);
            }

            // Fill with pairs
            var pairs = new Dictionary<string, long>();
            for (var i = 0; i < lines[0].Length - 1; i++)
            {
                pairs.AddDefault(lines[0][i].ToString() + lines[0][i + 1]);
            }

            for (var i = 0; i < 40; i++)
            {
                var newPairs = new Dictionary<string, long>();
                foreach(var pair in pairs.Keys)
                {
                    var rule = rules[pair];
                    newPairs.AddDefault(pair[0].ToString() + rule, pairs[pair]);
                    newPairs.AddDefault(rule + pair[1], pairs[pair]);
                }

                pairs = newPairs;
            }

            var counts = new Dictionary<string, long>();
            var lastPart = lines[0].Last().ToString();
            counts.AddDefault(lastPart);
            foreach(KeyValuePair<string, long> pair in pairs)
            {
                counts.AddDefault(pair.Key[0].ToString(), pair.Value);
            }

            return counts.Values.Max() - counts.Values.Min();
        }

        private static void AddDefault(this Dictionary<string, long> dict, string key, long value = 1)
        {
            if (!dict.TryGetValue(key, out long _))
            {
                dict[key] = 0;
            }

            dict[key] += value;
        }
    }
}