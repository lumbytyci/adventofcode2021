namespace AOC2021_Day1;

public static class Part1
{
    public static void Main(string[] args)
    {
        var input = @"199    
            200 
            208
            210
            200
            207
            240
            269
            260
            263";

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
        var depths = input.Split("\r\n").Select(x => int.Parse(x));
        return depths.Zip(depths.Skip(1)).Count(x => x.Second - x.First > 0);
    }
}