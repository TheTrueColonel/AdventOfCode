using AdventOfCode.Days.DayFour;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode; 

[MemoryDiagnoser]
public class Benchmark {

    [Benchmark]
    public int Day4Part1Bench() {
        var day = new DayFourPartOne();
        return day.Run();
    }
    
    [Benchmark]
    public int Day4Part2Bench() {
        var day = new DayFourPartTwo();
        return day.Run();
    }
    
    [Benchmark]
    public int Day4Part1BenchForFun() {
        var day = new DayFourPartOneForFun();
        return day.Run();
    }
    
    [Benchmark]
    public int Day4Part2BenchForFun() {
        var day = new DayFourPartTwoForFun();
        return day.Run();
    }
}
