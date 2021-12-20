using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021_Day14
{
    public static class Part1
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

        public static int RunPart(string input)
        {
            var lines = input.Split("\r\n\r\n");
            var rules = new Dictionary<string, string>();
            var polymer = new Stack<string>(lines[0].Select(x => x.ToString()));

            foreach (var rule in lines[1].Split("\r\n"))
            {
                var lineSplit = rule.Split(" -> ");
                rules.Add(lineSplit[0], lineSplit[1]);
            }

            for (var i = 0; i < 10; i++)
            {
                var newPolymer = new Stack<string>();
                foreach (var part in polymer.Reverse())
                {
                    if (newPolymer.Count == 0)
                    {
                        newPolymer.Push(part);
                        continue;
                    }

                    newPolymer.Push(rules[newPolymer.Peek() + part]);
                    newPolymer.Push(part);
                }

                polymer = newPolymer;
            }

            var ordered = polymer
                .GroupBy(x => x)
                .OrderByDescending(x => x.Count());

            return ordered.First().Count() - ordered.Last().Count();
        }
    }
}