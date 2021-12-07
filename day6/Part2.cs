using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021_Day5
{
    public static class Part2
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
            // Result: 1650309278600. Execution time: 17 ms 

            Console.ReadKey();
        }

        public static long RunPart(string input)
        {
            var queue = new List<long>(9);
            queue.AddRange(Enumerable.Repeat(0L, 9));

            input.Split(",")
                .Select(x => long.Parse(x))
                .ToList()
                .ForEach(x => queue[(int)x]++);

            const int Days = 256;
            for (var i = 0; i < Days; i++)
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