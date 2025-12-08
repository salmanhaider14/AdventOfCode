using AdventOfCodeSupport;

namespace AdventOfCode._2025;

public class Day01: AdventBase
{
    private string ExampleInput = "L68\nL30\nR48\nL5\nR60\nL55\nL1\nL99\nR14\nL82";
    
    protected override object InternalPart1()
    {
        var num = 50;
        var zeroCount = 0;

        foreach (var line in Input.Lines)
        {
            var direct = line[0];
            var distance = int.Parse(line.Substring(1));
            
            if (direct == 'L')
            {
                num = (num - distance) % 100;
            }
            else
            {
                num = (num + distance) % 100;
            }

            if (num == 0) zeroCount++;
        }

        return zeroCount;
    }
    protected override object InternalPart2()
    {
        var num = 50;
        var zeroCount = 0;
        var prev = 0;

        foreach (var line in Input.Lines)
        {
            var direct = line[0];
            var distance = int.Parse(line.Substring(1));
            prev = num;

            if (direct == 'L')
            {
                // first step k (1..100) that will land on 0 when moving left
                var firstK = prev % 100;
                if (firstK == 0) firstK = 100;

                if (distance >= firstK)
                    zeroCount += 1 + (distance - firstK) / 100;

                // update position (make sure modulo is positive)
                num = (prev - (distance % 100) + 100) % 100;
            }
            else // 'R'
            {
                // first step k (1..100) that will land on 0 when moving right
                var firstK = (100 - prev) % 100;
                if (firstK == 0) firstK = 100;

                if (distance >= firstK)
                    zeroCount += 1 + (distance - firstK) / 100;

                num = (prev + (distance % 100)) % 100;
            }

            // NOTE: Do NOT add extra `if (num == 0) zeroCount++` here —
            // the logic above already counts the final landing if it was one of the zero hits.
        }

        return zeroCount;
    }

}