using System;
using System.Collections.Generic;
using System.IO;

namespace AOC2021_Day10
{
    public static class Part1
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

        public static int RunPart(string input)
        {
            var points = new Dictionary<char, int>
            {
                [')'] = 3,
                [']'] = 57,
                ['}'] = 1197,
                ['>'] = 25137
            };

            var pairs = new Dictionary<char, char>
            {
                [')'] = '(',
                [']'] = '[',
                ['}'] = '{',
                ['>'] = '<'
            };

            var totalScore = 0;
            foreach (var line in (string[])input.Split("\r\n"))
            {
                var stack = new Stack<int>();
                stack.Push(line[0]);

                foreach (var b in line)
                {
                    if (!points.ContainsKey(b))
                    {
                        stack.Push(b);
                        continue;
                    }

                    if (stack.Pop() != pairs[b])
                    {
                        totalScore += points[b];
                        break;
                    }
                }
            }

            return totalScore;
        }
    }
}