using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AOC2021_Day10
{
    public static class Part2
    {
        public static void Main(string[] args)
        {
            var input = @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]";

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
            var points = new Dictionary<char, long>
            {
                [')'] = 1L,
                [']'] = 2L,
                ['}'] = 3L,
                ['>'] = 4L
            };

            var pairs = new Dictionary<char, char>
            {
                [')'] = '(',
                [']'] = '[',
                ['}'] = '{',
                ['>'] = '<'
            };

            // Or maybe ) = '(' + 1 etc.
            var reversePairs = pairs.ToDictionary(x => x.Value, x => x.Key);

            var scores = new List<long>();
            foreach (var line in (string[])input.Split("\r\n"))
            {
                var stack = new Stack<char>();

                int i;
                for (i = 0; i < line.Length; i++)
                {
                    var b = line[i];
                    if (!points.ContainsKey(b))
                    {
                        stack.Push(b);
                        continue;
                    }

                    if (stack.Pop() != pairs[b])
                    {
                        break;
                    }
                }

                // Incomplete line
                if (i == line.Length)
                {
                    var completionString = stack.Aggregate(new StringBuilder(), (builder, bracket) => builder.Append(reversePairs[bracket])).ToString();
                    scores.Add(stack.Aggregate(0L, (totalScore, bracket) => points[reversePairs[bracket]] + (totalScore * 5L)));
                }
            }

            return scores.OrderBy(x => x).ToList()[scores.Count / 2];
        }
    }
}