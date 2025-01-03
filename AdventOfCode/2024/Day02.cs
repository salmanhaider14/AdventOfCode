using AdventOfCodeSupport;
namespace AdventOfCode._2024;
public class Day02: AdventBase
{ 
    protected override object InternalPart1()
    {
        return Input.Lines
            .Select(line => line.Trim().Split(" ").Select(long.Parse).ToList())
            .Count(SafetyCheck);
    }
    protected override object InternalPart2()
    {
        var safeReports = 0;
        foreach (var line in Input.Lines)
        {
            var numbers = line.Trim().Split(" ").Select(x => long.Parse(x)).ToList();
            var safe = SafetyCheck(numbers);
            if (safe)
                safeReports++;
            else 
            {
                for (int i = 0; i < numbers.Count; i++)
                {
                    var modified = numbers.Where((_, index) => index != i).ToList();
                    var isSafe = SafetyCheck(modified);

                    if (isSafe)
                    {
                        safeReports++;
                        break;
                    }
                }
            }
        }
        return safeReports;
    }

    private bool SafetyCheck(List<long> numbers)
    {
        var increasing = numbers[0] < numbers[1];
        var safe = true;

        for (int i = 0; i < numbers.Count - 1; i++)
        {
            var current = numbers[i];
            var next = numbers[i + 1];
            
            if ((increasing && next <= current) || (!increasing && next >= current) || (Math.Abs(current - next) is > 3))
            {
                safe = false;
                break;
            } 
        }
        return safe;
    }
}