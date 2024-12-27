using AdventOfCodeSupport;

namespace AdventOfCode._2020;

public class Day7:AdventBase
{
    private const string test =
        "light red bags contain 1 bright white bag, 2 muted yellow bags.\ndark orange bags contain 3 bright white bags, 4 muted yellow bags.\nbright white bags contain 1 shiny gold bag.\nmuted yellow bags contain 2 shiny gold bags, 9 faded blue bags.\nshiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.\ndark olive bags contain 3 faded blue bags, 4 dotted black bags.\nvibrant plum bags contain 5 faded blue bags, 6 dotted black bags.\nfaded blue bags contain no other bags.\ndotted black bags contain no other bags.";
    protected override object InternalPart1()
    {
        var rules = Input.Text.Split("\n");
        var bagMap = new Dictionary<string, List<string>>();
        
        foreach (var rule in rules)
        {
            var parts = rule.Split(" contain ");
            var outerBag = parts[0].Replace(" bags", "");
            var innerBags = parts[1]
                .Split(", ")
                .Select(x => x.Substring(x.IndexOf(' ') + 1)
                    .Replace(" bags", "").Replace(" bag", "").Replace(".", ""))
                .Where(x => x != "no other")
                .ToList();

            if (!bagMap.ContainsKey(outerBag))
                bagMap[outerBag] = [];

            foreach (var innerBag in innerBags)
                bagMap[outerBag].Add(innerBag);
        }
        
        var canContainGold = new HashSet<string>();
        var queue = new Queue<string>();

        foreach (var bag in bagMap.Keys)
        {
            if (!bagMap[bag].Contains("shiny gold")) continue;
            canContainGold.Add(bag);
            queue.Enqueue(bag);
        }
        while (queue.Count > 0)
        {
            var currentBag = queue.Dequeue();
            foreach (var outerBag in bagMap.Keys)
            {
                if (bagMap[outerBag].Contains(currentBag) && canContainGold.Add(outerBag))
                {
                    queue.Enqueue(outerBag);
                }
            }
        }

        return canContainGold.Count;
    }
    
    protected override object InternalPart2()
    {
        var rules = Input.Text.Split("\n");
        var bagMap = new Dictionary<string, List<(int count, string color)>>();
        
        foreach (var rule in rules)
        {
            var parts = rule.Split(" contain ");
            var outerBag = parts[0].Replace(" bags", "").Replace(" bag", "");
            var innerBags = parts[1]
                .Split(", ")
                .Select(x => x.Trim('.'))
                .Where(x => x != "no other bags")
                .Select(x =>
                {
                    var spaceIndex = x.IndexOf(' ');
                    var count = int.Parse(x[..spaceIndex]);
                    var color = x[(spaceIndex + 1)..].Replace(" bags", "").Replace(" bag", "");
                    return (count, color);
                })
                .ToList();

            bagMap[outerBag] = innerBags;
        }

        return CountBagsInside("shiny gold");
        
        int CountBagsInside(string bag)
        {
            if (!bagMap.ContainsKey(bag) || bagMap[bag].Count == 0)
                return 0;

            var count = 0;
            foreach (var (innerCount, innerBag) in bagMap[bag])
            {
                count += innerCount + innerCount * CountBagsInside(innerBag);
            }

            return count;
        }
    }
}