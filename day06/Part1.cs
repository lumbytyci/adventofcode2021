using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021_Day5
{
    public static class Part1
    {
        public static void Main(string[] args)
        {
            var input = "3,4,3,1,2";

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
            var queue = new List<int>(9);
            queue.AddRange(Enumerable.Repeat(0, 9));

            input.Split(",")
                .Select(x => int.Parse(x))
                .ToList()
                .ForEach(x => queue[x]++);

            const int Days = 80;
            for(var i = 0; i < Days; i++)
            {
                var fishesToReplicate = queue[0];
                queue.RemoveAt(0);
                queue.Add(fishesToReplicate);
                queue[6] += fishesToReplicate;
            }

            return queue.Sum();
        }
    }
}