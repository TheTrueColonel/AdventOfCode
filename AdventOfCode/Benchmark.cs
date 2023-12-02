using AdventOfCode.Days;
using AdventOfCode.Days.DayTwo;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode; 

[MemoryDiagnoser]
public class Benchmark {

    [Benchmark]
    public int Day2Part1Bench() {
        var day = new DayTwoPartOne();
        return day.Run();
    }
    
    [Benchmark]
    public int Day2Part2Bench() {
        var day = new DayTwoPartTwo();
        return day.Run();
    }
}
