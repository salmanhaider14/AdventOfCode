using System.Text;
using AdventOfCodeSupport;
using Microsoft.Extensions.Primitives;

namespace AdventOfCode._2025;

public class Day02: AdventBase
{
    private string ExampleInput =
        "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
    protected override object InternalPart1()
    {
        var ids = Input.Text.Split(",");
        List<long> invalidIds = [];
        foreach (var id in ids)
        {
            var parts = id.Split("-");
            var first = long.Parse(parts[0]);
            var last = long.Parse(parts[1]);

            for (long i = first; i <= last; i++)
            {
                var numString = i.ToString();
                var midPoint = numString.Length / 2;
                var firstHalf = numString.Substring(0, midPoint);
                var secondHalf = numString.Substring(midPoint);
                
                if(firstHalf == secondHalf) invalidIds.Add(i);

            }
            
            Console.WriteLine(id);
        }
        return invalidIds.Sum();
    }

    protected override object InternalPart2()
    {
        var ids = Input.Text.Split(",");
        List<long> invalidIds = [];
        foreach (var id in ids)
        {
            var parts = id.Split("-");
            var first = long.Parse(parts[0]);
            var last = long.Parse(parts[1]);

            for (long i = first; i <= last; i++)
            {
                if(Helper(i)) invalidIds.Add(i);
            }
            Console.WriteLine(id);
        }
        return invalidIds.Sum();
    }

    private bool Helper(long id)
    {
        var numString = id.ToString();
        for (int i = 0; i < numString.Length/2; i++)
        {
            var stringBuilder = new StringBuilder();
            for (int j = 0; j <= i; j++)
            {
                stringBuilder.Append(numString[j]);
            }

            var finalString = stringBuilder.ToString();
            var blocks = numString.Length / finalString.Length;
            var newNum = new StringBuilder();
                for (int j = 0; j < blocks; j++)
                {
                    newNum.Append(finalString);
                }

                if (numString == newNum.ToString()) return true;
        }

        return false;
    }
}