namespace AOC2021_Day1;

public static class Part2
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
        //Source: 1 2 3 4
        //1 2 (first, second) - Zip 1
        //(1 2) 3 (first (first second), second) - Zip 2
        //((1 2) 3) 4 (first (first (first second)), second) - Zip 3
        var depths = input.Split("\r\n").Select(x => int.Parse(x.Trim()));
        return depths.Zip(depths.Skip(1)).Zip(depths.Skip(2)).Zip(depths.Skip(3))
            .Count(x => (x.Second + x.First.First.Second + x.First.Second) - (x.First.First.First + x.First.First.Second + x.First.Second) > 0);
    }
}