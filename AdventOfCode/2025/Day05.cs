using AdventOfCodeSupport;

namespace AdventOfCode._2025;

public class Day05: AdventBase
{
    private string _exampleInput = "3-5\n10-14\n16-20\n12-18\n\n1\n5\n8\n11\n17\n32";
    protected override object InternalPart1()
    {
        var inputParts = Input.Text.Split("\n\n");
        var freshRanges = inputParts[0].Split("\n").Select(x => x.Split("-")).Select(x => (long.Parse(x[0]),(long.Parse(x[1]))));
        var availableIngredients = inputParts[1].Split("\n").Select(long.Parse);
        var freshCount = 0;

        foreach (var availableIngredient in availableIngredients)
        {
            foreach (var freshRange in freshRanges)
            {
                if (availableIngredient == freshRange.Item1 || availableIngredient == freshRange.Item2)
                {
                    freshCount++;
                    break;
                }
                if (availableIngredient > freshRange.Item1 && availableIngredient < freshRange.Item2)
                {
                    freshCount++;
                    break;
                }
            }
        }
        

        return freshCount;
    }

    protected override object InternalPart2()
    {
        var inputParts = Input.Text.Split("\n\n");
        var freshRanges = inputParts[0].Split("\n").Select(x => x.Split("-")).Select(x => (long.Parse(x[0]),(long.Parse(x[1])))).OrderBy(x => x.Item1).ToList();
        var sortedRanges = new List<(long,long)>();

        var prev = freshRanges[0];
        for (int i = 1; i < freshRanges.Count; i++)
        {
            if (prev.Item2 < freshRanges[i].Item1)
            {
                sortedRanges.Add(prev);
                prev = freshRanges[i];
                continue;
            }
            prev.Item2 = Math.Max(prev.Item2, freshRanges[i].Item2);
        }
        sortedRanges.Add(prev);
        return sortedRanges.Select(x => x.Item2 - x.Item1 + 1).Sum();
    }
}

