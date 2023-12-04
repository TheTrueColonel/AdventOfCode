using System.Text.RegularExpressions;

namespace AdventOfCode.Days.DayFour;

/// <summary>
/// Was curious about performance measurements with static Regex, and using spans for parsing the numGroups
/// </summary>

public class DayFourPartOneForFun {
    private static readonly Regex Regex = new(@"(\d+)(?:\s*(\d+))*", RegexOptions.Compiled);
    
    public int Run() {
        var result = 0;
        
        Parallel.ForEach(File.ReadLines("Days/DayFour/data.txt"), line => {
            var lineSpan = line.AsSpan(line.IndexOf(':') + 1);
            var numGroups = GetNumGroups(lineSpan);

            // Intersect to only get numbers that exist in both groups (aka winning numbers)
            var wonNums = numGroups[1].Intersect(numGroups[0]).ToArray();
            
            // Return points based off geometric progression
            Interlocked.Add(ref result, (int)(1 * Math.Pow(2, wonNums.Length - 1)));
        }); 
        
        return result;
    }

    private static List<List<int>> GetNumGroups(ReadOnlySpan<char> lineSpan) {
        var numGroups = new List<List<int>> {
            new(),
            new()
        };
        var groupIndex = 0;
        
        foreach (var match in Regex.EnumerateMatches(lineSpan)) {
            var numGroup = lineSpan.Slice(match.Index, match.Length);

            while (!numGroup.IsEmpty) {
                var nextSeparatorIndex = numGroup.IndexOf(' ');

                var selection = nextSeparatorIndex == -1 ? 
                    numGroup : 
                    numGroup[..nextSeparatorIndex];

                if (selection.IsWhiteSpace()) {
                    numGroup = numGroup[(nextSeparatorIndex + 1)..];
                    continue;
                }
                
                int.TryParse(selection, out var number);
                
                if (nextSeparatorIndex == -1) {
                    numGroups[groupIndex].Add(number);
                    break;
                }
                
                numGroups[groupIndex].Add(number);

                numGroup = numGroup[(nextSeparatorIndex + 1)..];
            }

            groupIndex++;
        }

        return numGroups;
    }
}

public class DayFourPartTwoForFun {
    private static readonly Regex Regex = new(@"(\d+)(?:\s*(\d+))*", RegexOptions.Compiled);
    
    public int Run() {
        var copies = new Dictionary<int, int>();

        var lineIndex = 0;

        foreach (var line in File.ReadLines("Days/DayFour/data.txt")) {
            copies.TryAdd(lineIndex, 1);
            
            var lineSpan = line.AsSpan(line.IndexOf(':') + 1);
            var numGroups = GetNumGroups(lineSpan);

            // Intersect to only get numbers that exist in both groups (aka winning numbers)
            var wonNumsCount = numGroups[1].Intersect(numGroups[0]).Count();

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

    private static List<List<int>> GetNumGroups(ReadOnlySpan<char> lineSpan) {
        var numGroups = new List<List<int>> {
            new(),
            new()
        };
        var groupIndex = 0;
        
        foreach (var match in Regex.EnumerateMatches(lineSpan)) {
            var numGroup = lineSpan.Slice(match.Index, match.Length);

            while (!numGroup.IsEmpty) {
                var nextSeparatorIndex = numGroup.IndexOf(' ');

                var selection = nextSeparatorIndex == -1 ? 
                    numGroup : 
                    numGroup[..nextSeparatorIndex];

                if (selection.IsWhiteSpace()) {
                    numGroup = numGroup[(nextSeparatorIndex + 1)..];
                    continue;
                }
                
                int.TryParse(selection, out var number);
                
                if (nextSeparatorIndex == -1) {
                    numGroups[groupIndex].Add(number);
                    break;
                }
                
                numGroups[groupIndex].Add(number);

                numGroup = numGroup[(nextSeparatorIndex + 1)..];
            }

            groupIndex++;
        }

        return numGroups;
    }
}
