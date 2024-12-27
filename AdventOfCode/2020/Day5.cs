using AdventOfCodeSupport;

namespace AdventOfCode._2020;

public class Day5:AdventBase
{
    private const string Test = "BFFFBBFRRR";
    protected override object InternalPart1()
    {
        double maxSeatId = int.MinValue;
        foreach (var line in Input.Lines)
        {
            double mid = 127;
            double low = 0;
            double high = 127;
            double row = -1;
            double col = -1;
            for (int i = 0; i < 7; i++)
            {
                mid = Math.Round((double)(mid/2));
                if (mid is 1)
                {
                    row = line[i] is 'F' ? low : high;
                }
                if (line[i] is 'F')
                    high -= mid;
                else
                    low += mid;
            }
        
            mid = 7;
            low = 0;
            high = 7;

            for (var i = 7; i < 10; i++)
            {
                mid = Math.Round((double)(mid/2));
                if (mid is 1)
                {
                    col = line[i] is 'L' ? low : high;
                }
                if (line[i] is 'L')
                    high -= mid;
                else
                    low += mid;
            }

            maxSeatId = Math.Max((row * 8) + col,maxSeatId);
        }

        return maxSeatId;
    }

    protected override object InternalPart2()
    {
        var set = new HashSet<double>();
        foreach (var line in Input.Lines)
        {
            double mid = 127;
            double low = 0;
            double high = 127;
            double row = -1;
            double col = -1;
            for (int i = 0; i < 7; i++)
            {
                mid = Math.Round((double)(mid/2));
                if (mid is 1)
                {
                    row = line[i] is 'F' ? low : high;
                }
                if (line[i] is 'F')
                    high -= mid;
                else
                    low += mid;
            }
        
            mid = 7;
            low = 0;
            high = 7;

            for (var i = 7; i < 10; i++)
            {
                mid = Math.Round((double)(mid/2));
                if (mid is 1)
                {
                    col = line[i] is 'L' ? low : high;
                }
                if (line[i] is 'L')
                    high -= mid;
                else
                    low += mid;
            }

            set.Add(row * 8 + col);
        }
        double min = set.Min();
        double max = set.Max();

        foreach (var id in Enumerable.Range((int)min, (int)max - (int)min + 1))
        {
            if (!set.Contains(id) && set.Contains(id - 1) && set.Contains(id + 1))
                return id;
        }

        return -1;
    }
}