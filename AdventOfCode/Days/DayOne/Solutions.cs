using System.Text.Json;

namespace AdventOfCode.Days.DayOne; 

public class DayOnePartOne {
    private static List<string> _data = new();
    
    public DayOnePartOne() {
        var json = File.ReadAllText("Days/DayOne/data.json");

        _data = JsonSerializer.Deserialize<List<string>>(json)!;
    }

    public int Run() => Trebuchet(_data);
    
    private int Trebuchet(List<string> input) {
        var result = new List<int>();

        foreach (var item in input) {
            var first = FindFirstDigit(item);
            var last = FindLastDigit(item);
            
            result.Add(int.Parse($"{first}{last}"));
        }
        
        return result.Sum();
    }

    private char FindFirstDigit(string input) {
        foreach (var c in input.Where(char.IsDigit)) {
            return c;
        }

        throw new InvalidOperationException("No digit found");
    }

    private char FindLastDigit(string input) {
        for (var i = input.Length - 1; i >= 0; i--) {
            var c = input[i];

            if (char.IsDigit(c)) {
                return c;
            }
        }

        throw new InvalidOperationException("No digit found");
    }
}

public class DayOnePartTwo {
    private static readonly Dictionary<string, int> NumMap = new(StringComparer.OrdinalIgnoreCase) {
        { "zero", 0 },
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };
    private static List<string> _data = new();
    
    public DayOnePartTwo() {
        var json = File.ReadAllText("Days/DayOne/data.json");

        _data = JsonSerializer.Deserialize<List<string>>(json)!;
    }

    public int Run() => Trebuchet(_data);
    
    private int Trebuchet(List<string> input) {
        var result = new List<int>();

        foreach (var item in input) {
            var first = FindFirstNumber(item);
            var last = FindLastNumber(item);
            
            result.Add(int.Parse($"{first}{last}"));
        }

        return result.Sum();
    }

    private int FindFirstNumber(string input) {
        for (var i = 0; i < input.Length; i++) {
            if (char.IsDigit(input[i])) {
                return (int)char.GetNumericValue(input[i]);
            }

            var result =  GetConcatenatedNumberFromString(input[i..]);

            if (result != -1) {
                return result;
            }
        }

        throw new InvalidOperationException("No digit found");
    }
    
    private int FindLastNumber(string input) {
        for (var i = input.Length - 1; i >= 0; i--) {
            if (char.IsDigit(input[i])) {
                return (int)char.GetNumericValue(input[i]);
            }

            var result =  GetConcatenatedNumberFromString(input[i..]);

            if (result != -1) {
                return result;
            }
        }

        throw new InvalidOperationException("No digit found");
    }
    
    private int GetConcatenatedNumberFromString(string input) {
        var currentNumber = GetNumberFromInt(input);

        if (currentNumber != -1) {
            return currentNumber;
        }

        return -1;
    }

    private int GetNumberFromInt(string input) {
        foreach (var kvp in NumMap) {
            if (input.StartsWith(kvp.Key)) {
                return kvp.Value;
            }
        }

        return -1;
    }
}