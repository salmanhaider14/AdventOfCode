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
        var rightMap = new Dictionary<int, int>();
        var sum = 0;
        
        foreach (var input in Input.Lines)
        {
            var parts = input.Split(" ");
            var leftNumber = int.Parse(parts[0].Trim());
            var rightNumber = int.Parse(parts[parts.Length - 1].Trim());
            
            qLeft.Add(leftNumber);
            if (rightMap.ContainsKey(rightNumber))
                rightMap[rightNumber]++;
            else
                rightMap[rightNumber] = 1;
        }
        for (int i = 0; i < qLeft.Count; i++)
        {
            if (rightMap.TryGetValue(qLeft[i], out int count))
             sum += qLeft[i] * count;
        }
        return sum;
    }
}