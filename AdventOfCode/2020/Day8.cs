using AdventOfCodeSupport;

namespace AdventOfCode._2020;

public class Day8: AdventBase
{
    protected override object InternalPart1()
    {
        var accumulator = 0;
        var visited = new bool[Input.Lines.Length];
        var i = 0;
        while (!visited[i])
        {
            visited[i] = true;
            var parts = Input.Lines[i].Split(" ");
            switch (parts[0])
            {
                case "nop":
                    i++;
                    break;
                case "acc":
                    accumulator += int.Parse(parts[1]);
                    i++;
                    break;
                case "jmp":
                    i += int.Parse(parts[1]);
                    break;
            }
        }
        return accumulator;
    }
    protected override object InternalPart2()
    {
        List<int> potentialIndexes = [];
        for (var j = 0; j < Input.Lines.Length; j++)
        {
            var parts = Input.Lines[j].Split(" ");
            if (parts[0] is "jmp" or "nop")
                potentialIndexes.Add(j);
        }

        foreach (var index in potentialIndexes)
        {
            var accumulator = 0;
            var visited = new bool[Input.Lines.Length];
            var i = 0;
            while ( i < Input.Lines.Length && i >= 0 && !visited[i])
            {
                visited[i] = true;
                var parts = Input.Lines[i].Split(" ");
                if (i == index)
                {
                    parts[0] = parts[0] == "jmp" ? "nop" : "jmp";
                }
                switch (parts[0])
                {
                    case "nop":
                        i++;
                        break;
                    case "acc":
                        accumulator += int.Parse(parts[1]);
                        i++;
                        break;
                    case "jmp":
                        i += int.Parse(parts[1]);
                        break;
                }
            }
            if (i >= Input.Lines.Length)
                return accumulator;
        }
        return -1;
    }
}