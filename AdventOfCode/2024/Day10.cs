using AdventOfCodeSupport;

namespace AdventOfCode._2024;

public class Day10 : AdventBase
{
    protected override object InternalPart1()
    {
        var grid = Input.Lines.Select(x => x.ToCharArray()).ToArray();
        return CalculateTrailSum(grid, useVisited: true); 
    }
    protected override object InternalPart2()
    {
        var grid = Input.Lines.Select(x => x.ToCharArray()).ToArray();
        return CalculateTrailSum(grid, useVisited: false);
    }
    private static int CalculateTrailSum(char[][] grid, bool useVisited)
    {
        var sum = 0;
        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == '0')
                {
                    sum += CountPaths(grid, i, j, useVisited);
                }
            }
        }
        return sum;
    }
    private static int CountPaths(char[][] grid, int startRow, int startCol, bool useVisited)
    {
        var directions = new[] { new[] { 0, 1 }, [0, -1], [1, 0], [-1, 0] };
        var count = 0;
        var visited = useVisited ? new bool[grid.Length, grid[0].Length] : null;
        ExplorePaths(grid, startRow, startCol, 0, ref count, directions, visited);
        return count;
    }
    private static bool ExplorePaths(char[][] grid, int row, int col, int currentNum, ref int count, int[][] directions, bool[,]? visited)
    {
        if (row < 0 || row >= grid.Length || col < 0 || col >= grid[0].Length ||
            (visited != null && visited[row, col]) || grid[row][col] - '0' != currentNum)
            return false;

        if (visited != null) visited[row, col] = true;

        if (grid[row][col] == '9' && currentNum == 9)
        {
            count++;
            return true;
        }
        foreach (var direction in directions)
            ExplorePaths(grid, row + direction[0], col + direction[1], currentNum + 1, ref count, directions, visited);
       
        if (visited != null) visited[row, col] = false;
        return false;
    }
}
