using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace AdventOfCode.Days.DayFour; 

public class DayFourPartOne {
    public int Run() {
        var result = 0;

        Parallel.ForEach(File.ReadLines("Days/DayFour/data.txt"), line => {
            // Convert line into groups of winning numbers and current numbers
            var numGroups = Regex.Matches(line[(line.IndexOf(':') + 1)..], @"(\d+)(?:\s*(\d+))*", RegexOptions.Compiled)
                .Select(x => x.Value
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray())
                .ToArray();

            // Intersect to only get numbers that exist in both groups (aka winning numbers)
            var wonNums = numGroups[0].Intersect(numGroups[1]).ToArray();
            
            // Return points based off geometric progression
            Interlocked.Add(ref result, (int)(1 * Math.Pow(2, wonNums.Length - 1)));
        }); 
        
        return result;
    }
}

public class DayFourPartTwo {
    public int Run() {
        var copies = new Dictionary<int, int>();

        var lineIndex = 0;

        foreach (var line in File.ReadLines("Days/DayFour/data.txt")) {
            copies.TryAdd(lineIndex, 1);
            
            // Convert line into groups of winning numbers and current numbers
            var numGroups = Regex.Matches(line[line.IndexOf(':')..], @"(\d+)(?:\s*(\d+))*", RegexOptions.Compiled)
                .Select(x => x.Value
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray())
                .ToArray();

            // Intersect to only get numbers that exist in both groups (aka winning numbers)
            var wonNumsCount = numGroups[0].Intersect(numGroups[1]).Count();

            for (var i = 0; i < wonNumsCount; i++) {
                var nextIndex = lineIndex + i + 1;
                
                // Get next number to increment
                copies.TryGetValue(nextIndex, out var count);
                
                if (count == 0) count++; // Ensure initial copy is always counted
                
                copies[nextIndex] = count + copies[lineIndex];
            }
            
            lineIndex++;
        }
        
        return copies.Values.Sum();
    }
}
