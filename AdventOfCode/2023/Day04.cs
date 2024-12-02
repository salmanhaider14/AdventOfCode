using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AdventOfCode._2023;

public class Day04 : AdventBase
{
    protected override object InternalPart1()
    {
        var sum = 0;
        foreach (var input in Input.Lines)
        {
            var numbers = input.Split(":")[1].Trim();
            var parts = numbers.Split("|");
            var winningNumbers = Regex.Matches(parts[0], @"\d+").Select(x => int.Parse(x.Value)).ToHashSet();
            var myNumbers = Regex.Matches(parts[1], @"\d+").Select(x => int.Parse(x.Value)).ToList();

            var cardValue = 0;
            var firstMatch = true;
            var matchedNumbers = new HashSet<int>();

            foreach (var number in myNumbers)
            {
                if (winningNumbers.Contains(number) )
                {
                    if (firstMatch)
                    {
                        cardValue += 1;
                        firstMatch = false;
                    }
                    else
                    {
                        cardValue *= 2;
                    }
                }
            }

            sum += cardValue;
        }

        return sum;
    }


    protected override object InternalPart2()
    {
        throw new NotImplementedException();
    }
}