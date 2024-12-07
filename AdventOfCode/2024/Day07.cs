using AdventOfCodeSupport;
namespace AdventOfCode._2024;

public class Day07 : AdventBase
{
    protected override object InternalPart1()
    {
        long sum = 0;
        foreach (var line in Input.Lines)
        {
            var parts = line.Split(": ");
            var total = long.Parse(parts[0]);
            var numbers = parts[1].Split(" ").Select(long.Parse).ToArray();

            if (IsTrueTotal(numbers, numbers[0], total,1))
                sum += total;
        }
        return sum;
    }
    private static bool IsTrueTotal(long[] numbers, long currentTotal, long actualTotal, int index)
    {
        if (currentTotal > actualTotal)
            return false;
        if (index == numbers.Length)
            return actualTotal == currentTotal;
        
        return IsTrueTotal(numbers, currentTotal + numbers[index], actualTotal, index + 1) ||
               IsTrueTotal(numbers, currentTotal * numbers[index], actualTotal, index + 1);
    }
    
    protected override object InternalPart2()
    {
        long sum = 0;
        foreach (var line in Input.Lines)
        {
            var parts = line.Split(": ");
            var total = long.Parse(parts[0]);
            var numbers = parts[1].Split(" ").Select(long.Parse).ToArray();

            if (IsTrueTotal2(numbers, numbers[0], total,1))
                sum += total;
        }
        return sum;
    }
    private static bool IsTrueTotal2(long[] numbers, long currentTotal, long actualTotal, int index)
    {
        if (currentTotal > actualTotal)
            return false;
        if (index == numbers.Length)
            return actualTotal == currentTotal;
        
        var thirdPossibility = long.Parse(currentTotal.ToString() + numbers[index]);
        return IsTrueTotal2(numbers, currentTotal + numbers[index], actualTotal, index + 1) ||
               IsTrueTotal2(numbers, currentTotal * numbers[index], actualTotal, index + 1) ||
               IsTrueTotal2(numbers, thirdPossibility, actualTotal, index + 1);
    }
}