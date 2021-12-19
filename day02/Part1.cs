using System;
using System.IO;
using System.Linq;

namespace AOC2021_Day2
{
    public static class Part1
    {
        public static void Main(string[] args)
        {
            var input = @"forward 5
                down 5
                forward 8
                up 3
                down 8
                forward 2";

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
            var position = (x: 0, y: 0);
            var movements = input.Split("\r\n")
                .Select(x => x.Trim().Split(' '))
                .Select(x => new { direction = x[0], units = int.Parse(x[1]) });

            foreach (var m in movements)
            {
                if (m.direction == "forward")
                {
                    position.x += m.units;
                }
                else if (m.direction == "down")
                {
                    position.y += m.units;
                }
                else if (m.direction == "up")
                {
                    position.y -= m.units;
                }
            }

            return position.x * position.y;
        }
    }
}