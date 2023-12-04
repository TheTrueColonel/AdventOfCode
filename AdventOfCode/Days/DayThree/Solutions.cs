using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Days.DayThree; 

public class DayThreePartOne {
    private static readonly string Data = File.ReadAllText("Days/DayThree/data.txt");

    public int Run() {
        var rows = Data.Split('\n');
        var symbols = Parse(rows, new Regex(@"[^.0-9]"));
        var nums = Parse(rows, new Regex(@"\d+"));

        var selectedNumbers = from n in nums
            where symbols.Any(x => Adjacent(x, n))
            select n.Int;

        return selectedNumbers.Sum();
    }

    private static Part[] Parse(IReadOnlyList<string> rows, Regex rgx) {
        var value = from row in Enumerable.Range(0, rows.Count)
            from match in rgx.Matches(rows[row])
            select new Part(match.Value, row, match.Index);

        return value.ToArray();
    }

    private static bool Adjacent(Part part1, Part part2) {
        return Math.Abs(part2.Row - part1.Row) <= 1 &&
               part1.Col <= part2.Col + part2.Text.Length &&
               part2.Col <= part1.Col + part1.Text.Length;
    }
}

public class DayThreePartTwo {
    private static readonly string Data = File.ReadAllText("Days/DayThree/data.txt");

    public int Run() {
        var rows = Data.Split('\n');
        var gears = Parse(rows, new Regex(@"\*"));
        var nums = Parse(rows, new Regex(@"\d+"));

        var selectedNumbers = from g in gears
            let neighbors = from n in nums where Adjacent(n, g) select n.Int
            where neighbors.Count() == 2
            select neighbors.First() * neighbors.Last();

        return selectedNumbers.Sum();
    }

    private static Part[] Parse(IReadOnlyList<string> rows, Regex rgx) {
        var value = from row in Enumerable.Range(0, rows.Count)
            from match in rgx.Matches(rows[row])
            select new Part(match.Value, row, match.Index);

        return value.ToArray();
    }

    private static bool Adjacent(Part part1, Part part2) {
        return Math.Abs(part2.Row - part1.Row) <= 1 &&
               part1.Col <= part2.Col + part2.Text.Length &&
               part2.Col <= part1.Col + part1.Text.Length;
    }
}

internal record Part(string Text, int Row, int Col) {
    public int Int => int.Parse(Text);
}