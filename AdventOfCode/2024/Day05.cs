using AdventOfCodeSupport;
namespace AdventOfCode._2024;
public class Day05: AdventBase
{ 
    protected override object InternalPart1()
    {
        var sum = 0;
        var rules = GetRules();
       
        for (var i = rules.Count + 1; i < Input.Lines.Length; i++)
        {
            var updates = Input.Lines[i].Split(",").Select(byte.Parse).ToArray();
            var applicableRules = rules
                .Where(rule => updates.Contains(rule.Item1) && updates.Contains(rule.Item2));
            var isCorrect = applicableRules.All(rule => Array.IndexOf(updates, rule.Item2) >= Array.IndexOf(updates, rule.Item1));
            if (isCorrect)
                sum += updates[updates.Length / 2];
        }
        return sum;
    }
    protected override object InternalPart2()
    {
        var sum = 0;
        var rules = GetRules();
        for (var i = rules.Count + 1; i < Input.Lines.Length; i++)
        {
            var incorrect = false;
            var updates = Input.Lines[i].Split(",").Select(byte.Parse).ToArray();
            var applicableRules = rules
                .Where(rule => updates.Contains(rule.Item1) && updates.Contains(rule.Item2)).ToArray();
            bool changed;
            do
            {
                changed = false;
                foreach (var rule in applicableRules)
                {
                    var firstIndex = Array.IndexOf(updates, rule.Item1);
                    var secondIndex = Array.IndexOf(updates, rule.Item2);
                    if (firstIndex < secondIndex) continue;
                    incorrect = true;
                    (updates[firstIndex], updates[secondIndex]) = (updates[secondIndex], updates[firstIndex]);
                    changed = true;
                }
            } while (changed);
            
            if(incorrect)
             sum += updates[updates.Length / 2];
        }
        return sum;
    }
    private List<(byte, byte)> GetRules()
    {
        var spaceIndex = Array.IndexOf(Input.Lines, string.Empty);
        List<(byte, byte)> rules = [];
        for (var i = 0; i < spaceIndex; i++)
        {
            var parts = Input.Lines[i].Split("|");
            rules.Add((byte.Parse(parts[0]),byte.Parse(parts[1])));
        }
        return rules;
    }
}
