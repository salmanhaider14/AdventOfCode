using AdventOfCodeSupport;

namespace AdventOfCode._2024;

public class Day04 : AdventBase
{
    protected override object InternalPart1()
    {
        List<List<int>> directions = [[0, 1], [0, -1], [-1, 0], [1, 0], [1, 1], [1, -1], [-1, 1], [-1, -1]];
        var rows = Input.Lines.Length;
        var cols = Input.Lines[0].Length;
        var count = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (Input.Lines[i][j] is 'X')
                {
                    count += CountXMASPaths(Input.Lines, i, j, directions);
                }
            }
        }

        return count;
    }

    private int CountXMASPaths(string[] grid, int startRow, int startCol, List<List<int>> directions)
    {
        string wordToSearch = "XMAS";
        int pathCount = 0;

        foreach (var dir in directions)
        {
            if (FindWordPath(grid, startRow, startCol, 0, wordToSearch, dir))
            {
                pathCount++;
            }
        }

        return pathCount;
    }

    private bool FindWordPath(string[] grid, int row, int col, int index, string word, List<int> direction)
    {
        if (row < 0 || row >= grid.Length ||
            col < 0 || col >= grid[0].Length ||
            grid[row][col] != word[index])
        {
            return false;
        }

        if (index == word.Length - 1)
        {
            return true;
        }

        return FindWordPath(grid, row + direction[0], col + direction[1], index + 1, word, direction);
    }

    protected override object InternalPart2()
    {
        var rows = Input.Lines.Length;
        var cols = Input.Lines[0].Length;
        var count = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (Input.Lines[i][j] is 'A')
                {
                    if (IsXMAS(Input.Lines, i, j))
                        count++;
                }
            }
        }
        return count;
    }

    private bool IsXMAS(string[] grid, int row, int col)
    {
        var rows = grid.Length;
        var cols = grid[0].Length;
        var masFound = 0;

        if (row - 1 < 0 || row + 1 >= rows || col - 1 < 0 || col + 1 >= cols)
            return false;
        
        if (grid[row - 1][col - 1] == 'M' && grid[row + 1][col + 1] == 'S')
            masFound++;
        if (grid[row - 1][col + 1] == 'M' && grid[row + 1][col - 1] == 'S')
            masFound++;
        if (grid[row - 1][col - 1] == 'S' && grid[row + 1][col + 1] == 'M')
            masFound++;
        if (grid[row - 1][col + 1] == 'S' && grid[row + 1][col - 1] == 'M')
            masFound++;

        return masFound >= 2;
    }
    
}