using System;
using System.IO;
using System.Linq;

namespace AOC2021_Day3
{
    public static class Part1
    {
        public static void Main(string[] args)
        {
            var input = @"00100
                11110
                10110
                10111
                10101
                01111
                00111
                11100
                10000
                11001
                00010
                01010";

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
            var numberLines = input.Split("\r\n").Select(x => x.Trim()).ToArray();
            var onesCount = new int[numberLines[0].Length];

            foreach (var line in numberLines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    onesCount[i] += line[i] == '1' ? 1 : 0;
                }
            }

            var gamma = 0;
            var epsilon = 0;
            int mostCommonThreshold = numberLines.Length / 2;

            foreach (var count in onesCount)
            {
                gamma <<= 1;
                epsilon <<= 1;

                if (count >= mostCommonThreshold)
                {
                    gamma++;
                }
                else
                {
                    epsilon++;
                }
            }

            return gamma * epsilon;
        }
    }
}