using AdventOfCode.Days.DaySix;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode; 

[MemoryDiagnoser]
public class Benchmark {

    [Benchmark]
    public int Day6Bench() {
        var day = new DaySix();
        return day.Run();
    }
}
