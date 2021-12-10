using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021_Day8
{
    public static class Part2
    {
        public static void Main(string[] args)
        {
            var input = @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce";

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
            var lengths = new List<int>() { 2, 3, 4, 7 }; // 1, 7, 4, 8 Digits
            var lines = input
                .Split("\r\n")
                .Select(x =>
                    {
                        var s = x.Split("|");
                        return new
                        {
                            Signals = s[0].Trim().Split(" ").Select(x => AscSortString(x)).ToList(),
                            Outputs = s[1].Trim().Split(" ").Select(x => AscSortString(x)).ToList()
                        };
                    });

            var output = 0;
            foreach (var line in lines)
            {
                var mappings = new Dictionary<int, string>
                {
                    [1] = line.Signals.Single(x => x.Length == 2),
                    [4] = line.Signals.Single(x => x.Length == 4),
                    [7] = line.Signals.Single(x => x.Length == 3),
                    [8] = line.Signals.Single(x => x.Length == 7)
                };

                mappings[2] = line.Signals.Single(x => x.Intersect(mappings[1]).Count() == 1 &&
                    x.Intersect(mappings[4]).Count() == 2 &&
                    x.Intersect(mappings[7]).Count() == 2);

                mappings[3] = line.Signals.Single(x => x.Length == 5 && x.Intersect(mappings[1]).Count() == 2);
                mappings[6] = line.Signals.Single(x => x.Length == 6 && x.Intersect(mappings[7]).Count() == 2);
                mappings[5] = line.Signals.Single(x => x.Length == 5 && !mappings.Values.Contains(x));
                mappings[0] = line.Signals.Single(x => x.Length == 6 && x.Intersect(mappings[5]).Count() == 4);
                mappings[9] = line.Signals.Single(x => x.Length == 6 && !mappings.Values.Contains(x));

                var reverseMappings = mappings.ToDictionary(x => x.Value, x => x.Key.ToString());
                var lineOutput = string.Concat(line.Outputs.Select(x => reverseMappings[x]));

                output += int.Parse(lineOutput);
            }

            return output;
        }

        private static string AscSortString(string input)
        {
            var chars = input.ToArray();
            Array.Sort(chars);

            return new string(chars);
        }
    }
}