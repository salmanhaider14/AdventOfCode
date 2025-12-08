using AdventOfCodeSupport;

namespace AdventOfCode._2020;

public class Day10:AdventBase
{
    protected override object InternalPart1()
    {
        var adapters = Input.Lines.Select(int.Parse).OrderBy(x => x).ToList();
        var rating = 0; 
        var oneDifferences = 0;
        var threeDifferences = 0;
        
        adapters.Add(adapters.Max() + 3);

        foreach (var adapter in adapters)
        {
            var difference = adapter - rating;
            if (difference == 1) oneDifferences++;
            else if (difference == 3) threeDifferences++;
            rating = adapter; 
        }

        return oneDifferences * threeDifferences;
    }


    protected override object InternalPart2()
    {
        var adapters = Input.Lines.Select(int.Parse).OrderBy(x => x).ToList();
        var deviceAdapterRating = adapters.Max() + 3;
        adapters.Add(deviceAdapterRating);
        var dp = new Dictionary<int, long> { { 0, 1 } };

        foreach (var adapter in adapters)
        {
            dp.TryGetValue(adapter - 1, out long waysFromOne);
            dp.TryGetValue(adapter - 2, out long waysFromTwo);
            dp.TryGetValue(adapter - 3, out long waysFromThree);

            dp.TryAdd(adapter, 0);

            dp[adapter] += waysFromOne + waysFromTwo + waysFromThree;
        }

        return dp[adapters[^1]];
    }
}