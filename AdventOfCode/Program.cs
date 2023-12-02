using AdventOfCode.Days.DayTwo;
using BenchmarkDotNet.Running;

namespace AdventOfCode;

public sealed class Aoc {
    public static void Main() {
        //BenchmarkRunner.Run<Benchmark>();
        //return;
        
        var day = new DayTwoPartTwo();
        
        var result = day.Run();

        Console.WriteLine(result);
    }
}