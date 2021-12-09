using System;
using System.IO;
using System.Linq;

namespace AOC2021_Day6
{
    public static class Part2
    {
        public static void Main(string[] args)
        {
            var input = "16,1,2,0,4,2,7,1,2,14";

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
            var levels = input.Split(",").Select(int.Parse).ToList();

            var lowestEnergy = int.MaxValue;
            for (var i = levels.Min(); i < levels.Max() + 1; i++)
            {
                var energy = 0;
                foreach (var level in levels)
                {
                    var delta = Math.Abs(level - i);
                    energy += delta + (delta * (delta - 1) / 2);
                }

                if (energy < lowestEnergy) lowestEnergy = energy;
            }

            return lowestEnergy;
        }
    }
}