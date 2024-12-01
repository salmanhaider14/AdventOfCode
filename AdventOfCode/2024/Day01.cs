using System.Text;
using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AdventOfCode._2024;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var qLeft = new List<int>();
        var qRight = new List<int>();
        var sum = 0;
        
        foreach (var input in Input.Lines)
        {
            var parts = input.Split(" ");
            var leftNumber = int.Parse(parts[0].Trim());
            var rightNumber = int.Parse(parts[parts.Length - 1].Trim());
            qLeft.Add(leftNumber);
            qRight.Add(rightNumber);
        }
        qLeft.Sort();
        qRight.Sort();
        for (int i = 0; i < qLeft.Count; i++)
        {
            var difference= Math.Abs(qRight[i] - qLeft[i]);
            sum += difference;
        }
        return sum;
    }

    protected override object InternalPart2()
    {
        var qLeft = new List<int>();
        var builder = new StringBuilder();
        var sum = 0;
        
        foreach (var input in Input.Lines)
        {
            var parts = input.Split(" ");
            var leftNumber = int.Parse(parts[0].Trim());
            var rightNumber = int.Parse(parts[parts.Length - 1].Trim());
            
            qLeft.Add(leftNumber);
            builder.Append($"{rightNumber},");
        }

        var rightList = builder.ToString();
        for (int i = 0; i < qLeft.Count; i++)
        {
            var regexPattern = @$"{qLeft[i]}";
            var occurences = Regex.Matches(rightList, regexPattern);
            var multiply = qLeft[i] * occurences.Count;
            sum += multiply;
        }
        return sum;
    }
}