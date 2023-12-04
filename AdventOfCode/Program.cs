using AdventOfCode.Days.DayThree;
using BenchmarkDotNet.Running;

namespace AdventOfCode;

public sealed class Aoc {
    public static void Main() {
        //BenchmarkRunner.Run<Benchmark>();
        //return;
        
        var day = new DayThreePartTwo();
        
        var result = day.Run();

        Console.WriteLine(result);
    }
}