using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021_Day13
{
    public static class Part2
    {
        public static void Main(string[] args)
        {
            var input = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";

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
            var dots = new HashSet<(int x, int y)>(lines[0].Split("\r\n")
                .Select(line =>
                {
                    var split = line.Split(",");
                    return (x: int.Parse(split[0]), y: int.Parse(split[1]));
                }).ToList());

            var folds = lines[1].Split("\r\n")
                .Select(line =>
                {
                    var instruction = line.Split("=");
                    return (axis: instruction[0].Last().ToString(), position: int.Parse(instruction[1]));
                });

            foreach (var (axis, position) in folds)
            {
                if (axis == "x")
                {
                    var foldedDots = new HashSet<(int x, int y)>();

                    foreach (var (x, y) in dots)
                    {
                        if (x > position)
                        {
                            var newPosition = (x: (2 * position) - x, y);
                            foldedDots.Add(newPosition);
                        }
                        else
                        {
                            foldedDots.Add((x, y));
                        }
                    }
                    dots = foldedDots;
                }
                else
                {
                    var foldedDots = new HashSet<(int x, int y)>();

                    foreach (var (x, y) in dots)
                    {
                        if (y > position)
                        {
                            var newPosition = (x, y: (2 * position) - y);
                            foldedDots.Add(newPosition);
                        }
                        else
                        {
                            foldedDots.Add((x, y));
                        }
                    }
                    dots = foldedDots;
                }
            }

            DisplayMessage(dots);

            return 0;
        }

        private static void DisplayMessage(HashSet<(int x, int y)> dots)
        {
            var maxH = dots.Max(dot => dot.x);
            var maxV = dots.Max(dot => dot.y);
            for (var i = 0; i <= maxH; i++)
            {
                for (var j = 0; j <= maxV; j++)
                {
                    var symbol = " ";
                    if (dots.Contains((i, j)))
                    {
                        symbol = "#";
                    }

                    Console.Write(symbol);
                }
                Console.WriteLine("");
            }
        }
    }
}