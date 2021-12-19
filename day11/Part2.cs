using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021_Day11
{
    public static class Part2
    {
        public static void Main(string[] args)
        {
            var input = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526";

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
            var lines = input.Split("\r\n");
            var octos = new Dictionary<(int x, int y), int>();

            for (var i = 0; i < lines.Length; i++)
            {
                for (var j = 0; j < lines[0].Length; j++)
                {
                    octos[(j, i)] = lines[i][j] - '0';
                }
            }

            var step = 1;
            while (true)
            {
                var flashQueue = new Queue<(int x, int y)>();
                foreach (var octo in octos.Keys)
                {
                    octos[octo]++;
                    if (octos[octo] > 9) flashQueue.Enqueue(octo);
                }

                while (flashQueue.Count > 0)
                {
                    var flashingOcto = flashQueue.Dequeue();
                    if (octos[flashingOcto] == 0) continue; /* Already flashed */

                    octos[flashingOcto] = 0;

                    foreach (var adj in GetAdjacentOctos(flashingOcto))
                    {
                        if (octos.ContainsKey(adj) && octos[adj] != 0)
                        {
                            octos[adj]++;
                            if (octos[adj] > 9) flashQueue.Enqueue(adj);
                        }
                    }
                }

                if (!octos.Values.Any(x => x != 0)) break;

                step++;
            }

            return step;
        }

        private static IEnumerable<(int x, int y)> GetAdjacentOctos((int x, int y) octo)
        {
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0) yield return (octo.x + i, octo.y + j);
                }
            }
        }
    }
}