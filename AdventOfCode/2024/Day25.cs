using AdventOfCodeSupport;

namespace AdventOfCode._2024;

public class Day25:AdventBase
{
    private const string TestInput =
        "#####\n.####\n.####\n.####\n.#.#.\n.#...\n.....\n\n#####\n##.##\n.#.##\n...##\n...#.\n...#.\n.....\n\n.....\n#....\n#....\n#...#\n#.#.#\n#.###\n#####\n\n.....\n.....\n#.#..\n###..\n###.#\n###.#\n#####\n\n.....\n.....\n.....\n#....\n#.#..\n#.#.#\n#####";
    protected override object InternalPart1()
    {
        var schematics = Input.Text.Split("\n\n");
        var locks = new List<List<int>> ();
        var keys = new List<List<int>>();
        var uniqueCombinations = 0;

        foreach (var schematic in schematics)
        {
            var grid = schematic.Split("\n").Select(x => x.ToCharArray()).ToArray();
            var curr = new List<int>();
            for (var i = 0; i < grid[0].Length; i++)
            {
                var count = 0;
                for (var j = 0; j < grid.Length; j++)
                {
                    if (grid[j][i] == '#')
                        count++;
                }
                curr.Add(count-1);
            }
            if(grid[0][0] == '.') keys.Add(curr);
            else locks.Add(curr);
        }

        foreach (var @lock in locks)
        {
            foreach (var key in keys)
            {
                var i = 0;
                var isCorrect = true;
                while (i <= 4)
                {
                    if (@lock[i] + key[i] >= 6)
                    {
                        isCorrect = false;
                        break;
                    }
                    i++;
                }

                if (isCorrect) uniqueCombinations++;
            }
        }
        return uniqueCombinations;
    }

    protected override object InternalPart2()
    {
        throw new NotImplementedException();
    }
}