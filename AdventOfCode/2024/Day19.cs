using AdventOfCodeSupport;
namespace AdventOfCode._2024;

public class Day19: AdventBase
{
    private const string TestInput =
        "r, wr, b, g, bwu, rb, gb, br\n\nbrwrr\nbggr\ngbbr\nrrbgbr\nubwu\nbwurrg\nbrgr\nbbrgwb";
    protected override object InternalPart1()
    {
        var (patterns, designs) = Parse();
        return designs.Count(design => BackTrack(design, "", patterns, new Dictionary<string, bool>())) - 1;
    }
    private static bool BackTrack(string design, string current, string[] patterns, Dictionary<string, bool> memo)
    {
        if (memo.TryGetValue(current, out var track)) return track;
        if (current.Length > design.Length || !design.StartsWith(current)) return memo[current] = false;
        if (design == current)
            return memo[current] = true;

        return memo[current] = patterns.Any(pattern => BackTrack(design, current + pattern, patterns, memo));
    }

    protected override object InternalPart2()
    {
        var (patterns, designs) = Parse();
        return designs.Sum(design => BackTrack2(design, "", patterns, new Dictionary<string, long>())) - 1;
    }
    private static long BackTrack2(string design, string current, string[] patterns, Dictionary<string, long> memo)
    {
        if (memo.TryGetValue(current, out var ways))
            return ways;

        if (current.Length > design.Length || !design.StartsWith(current))
            return 0;

        if (current == design)
            return 1;
        
        return memo[current] = patterns.Sum(pattern => BackTrack2(design, current + pattern, patterns, memo));
    }
    private (string[], string[]) Parse()
    {
        var parts = Input.Text.Split("\n\n");
        return (parts[0].Split(", "), parts[1].Split("\n"));
    }
}