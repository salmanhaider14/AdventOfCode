using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AdventOfCode._2024;
public class Day03: AdventBase
{
    protected override object InternalPart1()
    {
        return Regex.Matches(Input.Text, @"mul\((\d{1,3}),(\d{1,3})\)")
            .Sum(x => long.Parse(x.Groups[1].Value) * long.Parse(x.Groups[2].Value));
    }
    protected override object InternalPart2()
    {
            var matches = Regex.Matches(Input.Text, @"mul\((\d{1,3}),(\d{1,3})\)|don't\(\)|do\(\)");
            var isEnabled = true;
            List<Match> validMuls = [];
            
            foreach (Match match in matches)
            {
                if (match.Value == "don't()")
                    isEnabled = false;
                else if (match.Value == "do()")
                    isEnabled = true;
                else if (isEnabled)
                    validMuls.Add(match);
            } 
            return validMuls.Sum(x => long.Parse(x.Groups[1].Value) * long.Parse(x.Groups[2].Value));
    }
}