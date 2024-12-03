using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AdventOfCode._2024;

public class Day03: AdventBase
{
    protected override object InternalPart1()
    { 
        return ProcessMuls(Regex.Matches(Input.Text, @"mul\(\d{1,3},\d{1,3}\)").Select(x => x.Value));
    }

    protected override object InternalPart2()
    {
            var matches = Regex.Matches(Input.Text, @"mul\(\d{1,3},\d{1,3}\)|don't\(\)|do\(\)");

            var isEnabled = true;
            List<string> validMuls = [];
            foreach (Match match in matches)
            {
                if (match.Value == "don't()")
                    isEnabled = false;
                else if (match.Value == "do()")
                    isEnabled = true;
                else if (isEnabled)
                    validMuls.Add(match.Value);
            }

            return ProcessMuls(validMuls);
    }
    private int ProcessMuls(IEnumerable<string> muls)
    {
        var sum = 0;
        foreach (var mul in muls)
        {
            var numbers = Regex.Matches(mul, @"\d+");
            sum += int.Parse(numbers[0].Value) * int.Parse(numbers[1].Value);
        }
        return sum;
    }
}