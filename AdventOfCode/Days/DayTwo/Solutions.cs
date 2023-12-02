using System.Text.RegularExpressions;

namespace AdventOfCode.Days.DayTwo; 

public class DayTwoPartOne {
    private const string NumQuery = @"\d+";
    private const string TextQuery = "[a-z]+";

    private readonly Dictionary<string, int> _limits = new() {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 },
    };

    public int Run() {
        var result = 0;

        Parallel.ForEach(File.ReadLines("Days/DayTwo/data.txt").AsParallel(), line => {
            var valid = true;
            var gameNum = int.Parse(Regex.Match(line, NumQuery, RegexOptions.Compiled).Value);

            // Takes all text after ":", splits it by ";" and ",", then flattens list to single list of color results
            var colorResults = line[(line.IndexOf(':') + 1)..]
                .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(x => x.Split(','))
                .SelectMany(x => x);

            foreach (var res in colorResults) {
                var dieCount = int.Parse(Regex.Match(res, NumQuery, RegexOptions.Compiled).Value);
                var dieColor = Regex.Match(res, TextQuery, RegexOptions.Compiled).Value;

                if (dieCount <= _limits[dieColor]) continue;
                
                valid = false;
                break;
            }

            if (valid) {
                Interlocked.Add(ref result, gameNum);
            }
        });

        return result;
    }
}
public class DayTwoPartTwo {
    private const string NumQuery = @"\d+";
    private const string TextQuery = "[a-z]+";

    public int Run() {
        var result = 0;

        Parallel.ForEach(File.ReadLines("Days/DayTwo/data.txt").AsParallel(), line => {
            var minNeeded = new Dictionary<string, int> {
                { "red", 0 },
                { "green", 0 },
                { "blue", 0 },
            };

            // Takes all text after ":", splits it by ";" and ",", then flattens list to single list of color results
            var colorResults = line[(line.IndexOf(':') + 1)..]
                .Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(x => x.Split(','))
                .SelectMany(x => x);

            foreach (var res in colorResults) {
                var dieCount = int.Parse(Regex.Match(res, NumQuery, RegexOptions.Compiled).Value);
                var dieColor = Regex.Match(res, TextQuery, RegexOptions.Compiled).Value;

                minNeeded[dieColor] = Math.Max(minNeeded[dieColor], dieCount);
            }

            Interlocked.Add(ref result, minNeeded["red"] * minNeeded["green"] * minNeeded["blue"]);
        });

        return result;
    }
}
