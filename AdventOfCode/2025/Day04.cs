using AdventOfCodeSupport;

namespace AdventOfCode._2025;

public class Day04: AdventBase
{
    private readonly string _exampleInput =
        "..@@.@@@@.\n@@@.@.@.@@\n@@@@@.@.@@\n@.@@@@..@.\n@@.@@@@.@@\n.@@@@@@@.@\n.@.@.@.@@@\n@.@@@.@@@@\n.@@@@@@@@.\n@.@.@@@.@.";
    private readonly (int row, int col)[] _directions = [(-1,0),(1,0),(0,-1),(0,1),(1,1),(1,-1),(-1,1),(-1,-1)];
    protected override object InternalPart1()
    {
        var grid = new char[Input.Lines.Length][];
        var count = 0;

        for (var index = 0; index < Input.Lines.Length; index++)
            grid[index] = Input.Lines[index].ToCharArray();

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] != '@') continue;
                var currCount = 0;
                foreach (var (directX, directY) in _directions)
                {
                    var newRow = i + directX;
                    var newCol = j + directY;
                    
                    if (newRow >= grid.Length || newRow < 0 || newCol >= grid[0].Length || newCol < 0) continue;
                    if (grid[newRow][newCol] == '@') currCount++;
                }
                if (currCount < 4) count++;

            }
        }
        return count;
    }
    protected override object InternalPart2()
    {
        var grid = new char[Input.Lines.Length][];
        var removedCount = 0;

        for (var index = 0; index < Input.Lines.Length; index++)
            grid[index] = Input.Lines[index].ToCharArray();

        while (true)
        {
            var count = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j] != '@') continue;
                    var currCount = 0;
                    foreach (var (directX, directY) in _directions)
                    {
                        var newRow = i + directX;
                        var newCol = j + directY;
                    
                        if (newRow >= grid.Length || newRow < 0 || newCol >= grid[0].Length || newCol < 0) continue;
                        if (grid[newRow][newCol] == '@')
                            currCount++;
                    }
                    if (currCount >= 4) continue;
                    count++;
                    grid[i][j] = '.';
                    removedCount++;
                }
            }

            if (count == 0) break;
        }
        return removedCount;
    }
}