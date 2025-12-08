using AdventOfCodeSupport;

namespace AdventOfCode._2020;

public class Day9:AdventBase
{
    protected override object InternalPart1()
    {
        var preamble = Input.Lines.Take(25).Select(long.Parse).ToList();
        var searchNumbers = Input.Lines.Skip(25).Select(long.Parse).ToArray();
        foreach (var target in searchNumbers)
        {
            var found = false;
            for (int i = 0; i < preamble.Count && !found; i++)
            {
                for (int j = 1; j < preamble.Count; j++)
                {
                    if (preamble[i] + preamble[j] == target) found = true;
                }
            }

            if (!found) return target;
            preamble.RemoveAt(0);
            preamble.Add(target);
        }

        return -1;
    }

    protected override object InternalPart2()
    {
        var preamble = Input.Lines.Take(25).Select(long.Parse).ToList();
        var searchNumbers = Input.Lines.Skip(25).Select(long.Parse).ToArray();
        var allNumbers = Input.Lines.Select(long.Parse).ToArray();
        long targetSum = 0;
        foreach (var target in searchNumbers)
        {
            var found = false;
            for (int i = 0; i < preamble.Count && !found; i++)
            {
                for (int j = 1; j < preamble.Count; j++)
                {
                    if (preamble[i] + preamble[j] == target) found = true;
                }
            }

            if (!found)
            {
                targetSum = target;
                break;
            }
            preamble.RemoveAt(0);
            preamble.Add(target);
        }
        
        for (int i = 0; i < allNumbers.Length; i++)
        {
            List<long> foundSequence = [allNumbers[i]];
            var sum = allNumbers[i];
            for (int j = i + 1; j < allNumbers.Length; j++)
            {
                foundSequence.Add(allNumbers[j]);
                sum += allNumbers[j];
                if (sum == targetSum)
                {
                    Console.WriteLine(string.Join(",",foundSequence));
                    return foundSequence.Min() + foundSequence.Max();
                }
            }
        }

        return -1;

    }
}