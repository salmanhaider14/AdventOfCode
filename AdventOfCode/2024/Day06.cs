using AdventOfCodeSupport;
namespace AdventOfCode._2024;
public class Day06: AdventBase
{
    private static readonly sbyte[] dx =  { -1, 0, 1, 0 };
    private static readonly sbyte[] dy = { 0, 1, 0, -1 };
    protected override object InternalPart1()
    {
        var grid = Input.Lines.Select(x => x.ToCharArray()).ToArray();
        var (i, j) = FindGuard(grid);
        return GetRoute(grid, i, j).Count;
    }
    protected override object InternalPart2()
    {
        var grid = Input.Lines.Select(x => x.ToCharArray()).ToArray();
        var (i, j) = FindGuard(grid);
        return GetLoopCount(grid, i, j);
    }
    private static int GetLoopCount(char[][] grid, int startRow, int startCol)
    {
        var loopCount = 0;
        var patrolPath = GetRoute(grid, startRow, startCol);

        foreach (var (row, col) in patrolPath)
        {
            if (row == startRow && col == startCol)
                continue;
            
            var temp = grid[row][col];
            grid[row][col] = '#';
            
            if (IsStuckInLoop(grid, startRow, startCol))
                loopCount++;
            grid[row][col] = temp;
        }
        return loopCount;
    }
    private static HashSet<(int,int)> GetRoute(char[][] grid, int startRow, int startCol)
    {
        int dirIndex = 0; 
        int row = startRow, col = startCol;
        var visited = new HashSet<(int, int)> { (row, col) };

        while (true)
        {
            var newRow = row + dx[dirIndex];
            var newCol = col + dy[dirIndex];
            
            if (newRow >= 0 && newRow < grid.Length && 
                newCol >= 0 && newCol < grid[0].Length && 
                grid[newRow][newCol] != '#')
            {
                row = newRow;
                col = newCol;
                visited.Add((row, col));
            }
            else
                dirIndex = (dirIndex + 1) % 4;
            if (IsOutOfTheBounds(grid,row,col,dirIndex))
                break;
        }
        return visited;
    }
    private static bool IsStuckInLoop(char[][] grid, int startRow, int startCol)
    {
        var visited = new HashSet<(int, int, int)>();
        int dirIndex = 0; 
        int row = startRow, col = startCol;

        while (true)
        {
            int newRow = row + dx[dirIndex];
            int newCol = col + dy[dirIndex];

            if (visited.Contains((newRow, newCol, dirIndex)))
                return true;
            if (newRow >= 0 && newRow < grid.Length && 
                newCol >= 0 && newCol < grid[0].Length && 
                grid[newRow][newCol] != '#')
            {
                row = newRow;
                col = newCol;
                visited.Add((row, col, dirIndex));
            }
            else
                dirIndex = (dirIndex + 1) % 4;
            if (IsOutOfTheBounds(grid,row,col,dirIndex))
                break;
        }
        return false;
    }
    private static bool IsOutOfTheBounds(char[][] grid, int row, int col, int dirIndex) => row + dx[dirIndex] < 0 || row + dx[dirIndex] >= grid.Length || col + dy[dirIndex] < 0 || col + dy[dirIndex] >= grid[0].Length;
    private static  (int,int) FindGuard(char[][] grid)
    {
        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] == '^')
                    return(i,j);
            }
        }
        return (0, 0);
    }
}