using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AdventOfCode._2024;

public class Day05: AdventBase
{
    protected override object InternalPart1()
    {
        long sum = 0;
        var rules = Regex.Matches(Input.Text, @"(\d+)\|(\d+)").Select(x => (long.Parse(x.Groups[1].Value),long.Parse(x.Groups[2].Value)));
        
        for (int i = rules.Count() + 1; i < Input.Lines.Length; i++)
        {
            var isCorrect = true;
            var updates = Input.Lines[i].Split(",").Select(x => long.Parse(x)).ToList();
            foreach (var rule in rules)
            {
                if (updates.Contains(rule.Item1) && updates.Contains(rule.Item2) && updates.IndexOf(rule.Item2) < updates.IndexOf(rule.Item1))
                {
                    isCorrect = false;
                    break;
                }
            }
            if (isCorrect)
                sum += updates[updates.Count / 2];
        }
        return sum;
    }
    
    protected override object InternalPart2()
    {
        long sum = 0;
        var rules = Regex.Matches(Input.Text, @"(\d+)\|(\d+)").Select(x => (long.Parse(x.Groups[1].Value),long.Parse(x.Groups[2].Value)));
        
        for (int i = rules.Count() + 1; i < Input.Lines.Length; i++)
        {
            var incorrect = false;
            var updates = Input.Lines[i].Split(",").Select(x => long.Parse(x)).ToList();
            bool changed;
            do
            {
                changed = false;
                foreach (var rule in rules)
                {
                    if ((updates.Contains(rule.Item1) && updates.Contains(rule.Item2)) &&
                        updates.IndexOf(rule.Item2) < updates.IndexOf(rule.Item1))
                    {
                        incorrect = true;
                        var firstIndex = updates.IndexOf(rule.Item1);
                        var secondIndex = updates.IndexOf(rule.Item2);
                        (updates[firstIndex], updates[secondIndex]) = (updates[secondIndex], updates[firstIndex]);
                        changed = true;
                    }
                }
            } while (changed);
            
            if(incorrect)
             sum += updates[updates.Count / 2];
        }
        return sum;
    }
}
