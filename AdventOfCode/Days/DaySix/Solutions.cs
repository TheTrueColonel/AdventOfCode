namespace AdventOfCode.Days.DaySix; 

public class DaySix {
    private static readonly List<Tuple<long, long>> Times = new() {
        //new(45, 305),
        //new(97, 1062),
        //new(72, 1110),
        //new(95, 1695)
        new(45977295, 305106211101695)
    };
    
    public int Run() {
        var result = 1;
        
        foreach (var item in Times) {
            var winningCount = 0;
            
            for (var i = 1; i < item.Item1; i++) {
                if (i * (item.Item1 - i) > item.Item2) 
                    winningCount++;
            }

            if (winningCount > 0) {
                result *= winningCount;
            }
        }

        return result;
    }
}
