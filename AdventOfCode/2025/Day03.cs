using System.Text;
using AdventOfCodeSupport;

namespace AdventOfCode._2025;

public class Day03: AdventBase
{
    private string _exampleInput = "987654321111111\n811111111111119\n234234234234278\n818181911112111";
    protected override object InternalPart1()
    {
        List<int> largestJoltages = [];
        foreach (var line in Input.Lines)
        {
            var largestJoltage = 0;
            for (int i = 0; i < line.Length; i++)
            {
                for (int j = i+1; j < line.Length; j++)
                    largestJoltage = Math.Max(largestJoltage, int.Parse(line[i] + line[j].ToString()));
            }
            largestJoltages.Add(largestJoltage);
        }
        return largestJoltages.Sum();
    }

    protected override object InternalPart2()
    {
        List<long> largestJoltages = [];
        foreach (var line in Input.Lines)
        {
            var stringBuilder = new StringBuilder();
            var i = 0;
            while (stringBuilder.Length < 12)
            {
                var max = 0;
                var maxIndex = 0;
                for (int j = i; j < line.Length; j++)
                {
                    if (int.Parse(line[j].ToString()) > max && (line.Length - j)- 1 >= 12 - stringBuilder.Length - 1)
                    {
                        max = int.Parse(line[j].ToString());
                        maxIndex = j;
                    } 
                }

                i = maxIndex + 1;
                stringBuilder.Append(max);
            }
            largestJoltages.Add(long.Parse(stringBuilder.ToString()));
        }
        return largestJoltages.Sum();
    }
}