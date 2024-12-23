using System.Collections.Immutable;
using AdventOfCodeSupport;

namespace AdventOfCode._2024;

public class Day23: AdventBase
{
    private const string TestInput =
        "kh-tc\nqp-kh\nde-cg\nka-co\nyn-aq\nqp-ub\ncg-tb\nvc-aq\ntb-ka\nwh-tc\nyn-cg\nkh-ub\nta-co\nde-co\ntc-td\ntb-wq\nwh-td\nta-ka\ntd-qp\naq-cg\nwq-ub\nub-vc\nde-ta\nwq-aq\nwq-vc\nwh-yn\nka-de\nkh-ta\nco-tc\nwh-qp\ntb-vc\ntd-yn";
    protected override object InternalPart1()
    {
        var tSets = 0;
        var connections = Input.Text.Split("\n").Select(x => x.Split("-")).ToArray();
        var dict = new Dictionary<string, List<string>>();
        foreach (var connection in connections)
        {
            if (dict.ContainsKey(connection[0]))
            {
                dict[connection[0]].Add(connection[1]);
            }
            else
                dict[connection[0]] = [connection[1]];
            
            if (dict.ContainsKey(connection[1]))
            {
                dict[connection[1]].Add(connection[0]);
            }
            else
                dict[connection[1]] = [connection[0]];
        }

        var sets = new List<List<string>>();
        foreach (var computer in dict.Keys)
        {
            var neighbors = dict[computer];
            foreach (var neighbor1 in neighbors)
            {
                foreach (var neighbor2 in neighbors)
                {
                    if (neighbor1 != neighbor2 && dict[neighbor1].Contains(neighbor2))
                    {
                        sets.Add(new List<string> { computer, neighbor1, neighbor2 });
                    }
                }
            }
        }

        sets = sets
            .Select(set => string.Join(",", set.OrderBy(x => x)))
            .Distinct()
            .Select(set => set.Split(",").ToList())
            .ToList();

        foreach (var set in sets)
        {
            Console.WriteLine(string.Join(",",set));
            foreach (var computer in set)
            {
                if (computer.StartsWith("t"))
                {
                    tSets++;
                    break;
                }
            }
        }
        
        return tSets;
    }

    protected override object InternalPart2()
    {
        var connections = Input.Text.Split("\n").Select(x => x.Split("-")).ToArray();
        var dict = new Dictionary<string, List<string>>();
        
        foreach (var connection in connections)
        {
            if (!dict.ContainsKey(connection[0]))
                dict[connection[0]] = new List<string>();
            dict[connection[0]].Add(connection[1]);

            if (!dict.ContainsKey(connection[1]))
                dict[connection[1]] = new List<string>();
            dict[connection[1]].Add(connection[0]);
        }

        List<string> largestClique = new();
        
        foreach (var start in dict.Keys)
        {
            var candidates = new HashSet<string> { start };
            foreach (var neighbor in dict[start])
            {
                if (candidates.All(member => dict[neighbor].Contains(member)))
                    candidates.Add(neighbor);
            }
            
            if (candidates.Count > largestClique.Count)
                largestClique = candidates.ToList();
        }
        
        largestClique.Sort();
        return string.Join(",", largestClique);
    }

}