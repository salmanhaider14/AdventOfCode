using AdventOfCodeSupport;
namespace AdventOfCode._2020;

public class Day6:AdventBase
{
    private const string test = "abc\n\na\nb\nc\n\nab\nac\n\na\na\na\na\n\nb";
    protected override object InternalPart1()
    {
        var groups = Input.Text.Split("\n\n");
        var sum = 0;

        foreach (var group in groups)
        {
            var set = new HashSet<char>();
            foreach (var c in group)
            {
                if(char.IsLetter(c))
                    set.Add(c);
            }

            sum += set.Count;
        }

        return sum;
    }

    protected override object InternalPart2()
    {
        var groups = Input.Text.Split("\n\n");
        var sum = 0;

        foreach (var group in groups)
        {
            var persons = group.Split("\n");
                var votes = new List<HashSet<char>>();
                foreach (var person in persons)
                {
                    var set = new HashSet<char>();
                    foreach (var c in person)
                    {
                        if (char.IsLetter(c))
                            set.Add(c);
                    }
                    votes.Add(set);
                }
                sum += votes.Aggregate((a, b) => a.Intersect(b).ToHashSet()).Count;
        }
        return sum;
    }
}