using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021_Day4
{
    public static class Part1
    {
        public static void Main(string[] args)
        {
            var input = @"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7";

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

        private sealed class Board
        {
            public int[][] Digits { get; }
            public HashSet<int> Unmarked { get; }

            public Board(string serializedBoard)
            {
                Unmarked = new();
                Digits = new int[5][];

                var linesEnumerator = serializedBoard.Split("\r\n").GetEnumerator();
                for (var i = 0; linesEnumerator.MoveNext(); i++)
                {
                    Digits[i] = linesEnumerator.Current.ToString().Trim().Split(' ').Where(x => x != "").Select(x => int.Parse(x)).ToArray();
                    Unmarked.UnionWith(Digits[i]);
                }
            }

            public bool HasWon()
            {
                for (var i = 0; i < 5; i++)
                {
                    int j;
                    for (j = 0; j < 5; j++)
                    {
                        if (Unmarked.Contains(Digits[i][j]))
                        {
                            break;
                        }
                    }
                    if (j == 5) return true;

                    for (j = 0; j < 5; j++)
                    {
                        if (Unmarked.Contains(Digits[j][i]))
                        {
                            break;
                        }
                    }
                    if (j == 5) return true;
                }

                return false;
            }
        }

        public static int RunPart(string input)
        {
            var splitedInput = input.Split("\r\n\r\n");
            var calls = splitedInput[0].Split(",").Select(x => int.Parse(x));
            var serializedBoards = splitedInput.Skip(1);

            var boards = serializedBoards.Select(x => new Board(x)).ToList();

            foreach (var call in calls)
            {
                foreach (var board in boards)
                {
                    board.Unmarked.Remove(call);
                    if (board.HasWon())
                    {
                        return call * board.Unmarked.Sum();
                    }
                }
            }

            return 0;
        }
    }
}