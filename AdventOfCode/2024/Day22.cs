using System.Runtime.InteropServices;
using AdventOfCodeSupport;
namespace AdventOfCode._2024;

public class Day22 : AdventBase
{
    private const string TestInput = "1\n2\n3\n2024";
    
    protected override object InternalPart1()
    {
        var initialNumbers = Input.Text.Split("\n").Select(int.Parse);
        long sum = 0;
        foreach (var secretNumber in initialNumbers)
        {
            long currentNumber = secretNumber;
            for (var i = 0; i < 2000; i++)
            {
                long step1 = ((currentNumber * 64) ^ currentNumber) % 16777216;
                long step2 = ((int)(step1 / 32) ^ step1) % 16777216;
                long step3 = ((step2 * 2048) ^ step2) % 16777216;

                currentNumber = step3;
            }
            sum += currentNumber;
        }
        return sum;
    }

    protected override object InternalPart2()
    {
        var initialNumbers = Input.Text.Split("\n").Select(long.Parse);
        var prices = new Dictionary<(int, int, int, int), int>();
        var seen = new HashSet<(int, int, int, int)>();

        foreach (var num in initialNumbers)
        {
            seen.Clear();
            var current = num;
            var (d1, d2, d3, d4) = (0, 0, 0, 0);

            for (var i = 0; i < 2000; i++)
            {
                long step1 = ((current * 64) ^ current) % 16777216;
                long step2 = ((int)(step1 / 32) ^ step1) % 16777216;
                long step3 = ((step2 * 2048) ^ step2) % 16777216;
                
                (d1, d2, d3, d4) = ((int)(step3 % 10) - (int)(current % 10), d1, d2, d3);
                
                if (i >= 3 && seen.Add((d1, d2, d3, d4)))
                {
                    ref var price = ref CollectionsMarshal.GetValueRefOrAddDefault(prices, (d1, d2, d3, d4), out _);
                    price += (int)(step3 % 10);
                }
                current = step3;
            }
        }
        return prices.Values.Max();
    }
}
