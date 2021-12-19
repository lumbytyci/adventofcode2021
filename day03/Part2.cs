using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021_Day3
{
    public static class Part2
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
            //1877139

            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            var result = RunPart(input);
            Console.WriteLine($"Result: {result}. Execution time: {watch.ElapsedMilliseconds} ms");

            Console.ReadKey();
        }

        public static int RunPart(string input)
        {
            var numberLines = input.Split("\r\n").Select(x => x.Trim()).ToList();
            var oxygenRating = CalculateRating(numberLines);
            var scrubberRating = CalculateRating(numberLines, mostCommon: false);

            return oxygenRating * scrubberRating;
        }

        private static int CalculateRating(List<string> numberLines, bool mostCommon = true)
        {
            var filteredList = new List<string>(numberLines);
            for (var position = 0; filteredList.Count > 1; position++)
            {
                var onesCount = 0;
                for (int i = 0; i < filteredList.Count; i++)
                {
                    onesCount += filteredList[i][position] == '1' ? 1 : 0;
                }

                var mostCommonDigit = onesCount >= (Math.Ceiling(filteredList.Count / 2.0M)) ? '1' : '0';
                var leastCommonDigit = mostCommonDigit == '1' ? '0' : '1';

                filteredList = filteredList.Where(x => x[position] == (mostCommon ? mostCommonDigit : leastCommonDigit)).ToList();
            }

            return Convert.ToInt32(filteredList.FirstOrDefault(), 2);
        }
    }
}