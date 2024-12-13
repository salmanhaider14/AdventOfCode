using AdventOfCodeSupport;
namespace AdventOfCode._2024;
public class Day12: AdventBase
{
    private static readonly int[][] Directions = [[0, 1], [0, -1], [1, 0], [-1, 0]];
    protected override object InternalPart1()
    {
        var sum = 0;
        var grid = Input.Lines.Select(l => l.ToCharArray()).ToArray();
        var visited = new bool[grid.Length, grid[0].Length];
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if(visited[i,j])
                    continue;
                List<(int, int)> siblingsPositions = [];
                SearchSiblings(grid, i, j, grid[i][j], siblingsPositions,visited);
                var parameters = CountParameters(grid, siblingsPositions, grid[i][j]);
                sum += siblingsPositions.Count * parameters;
            }
        }
        return sum;
    }
    protected override object InternalPart2()
    {
        var sum = 0;
        var grid = Input.Lines.Select(l => l.ToCharArray()).ToArray();
        var visited = new bool[grid.Length, grid[0].Length];
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if(visited[i,j])
                    continue;
                List<(int, int)> siblingsPositions = [];
                SearchSiblings(grid, i, j, grid[i][j], siblingsPositions,visited);
                Console.WriteLine("Sibling {0}, Count {1}", grid[i][j],siblingsPositions.Count);
                var sides = CountSides(grid, siblingsPositions, grid[i][j]);
                Console.WriteLine("Sides {0}", sides);
                sum += siblingsPositions.Count * sides;
            }
        }
        return sum;
    }
    private static void SearchSiblings(char[][] grid, int row, int col, char target, List<(int,int)> siblingsPositions,bool[,] visited)
    {
        if (row < 0 || row >= grid.Length || col < 0 || col >= grid[0].Length || grid[row][col] != target || visited[row,col])
            return;

        visited[row, col] = true;
        siblingsPositions.Add((row,col));
        foreach (var direction in Directions)
        {
            SearchSiblings(grid, row + direction[0], col + direction[1],target,siblingsPositions,visited);
        }
    }
    private static int CountParameters(char[][]grid, List<(int, int)> positions, char target)
    {
        var count = 0;
        foreach (var (row,col) in positions)
        {
            foreach (var direction in Directions)
            {
                var newRow = row + direction[0];
                var newCol = col + direction[1];
                if (newRow < 0 || newRow >= grid.Length || newCol < 0 || newCol >= grid[0].Length || grid[newRow][newCol] != target)
                    count++;
            }
        }
        return count;
    }
    private static int CountSides(char[][] grid, List<(int, int)> positions, char target)
    {
        var fences = new HashSet<(int, int, int, int)>();
        var cornerCount = 0;
        
        foreach (var (row, col) in positions)
        {
            foreach (var direction in Directions)
            {
                var newRow = row + direction[0];
                var newCol = col + direction[1];

                if (newRow < 0 || newRow >= grid.Length || 
                    newCol < 0 || newCol >= grid[0].Length ||
                    grid[newRow][newCol] != target)
                {
                    fences.Add((row, col, direction[0], direction[1]));
                }
            }
        }
        
        foreach (var (x, y, dirx, diry) in fences)
        {
            bool countCorner = true;

            // Check if adjacent fence has same direction
            if (dirx == 0 && diry == -1) // Up
            {
                if (fences.Contains((x + 1, y, 0, -1))) // Right
                {
                    countCorner = false;
                }
            }
            else if (dirx == 0 && diry == 1) // Down
            {
                if (fences.Contains((x + 1, y, 0, 1))) // Right
                {
                    countCorner = false;
                }
            }
            else if (dirx == 1 && diry == 0) // Right
            {
                if (fences.Contains((x, y + 1, 1, 0))) // Down
                {
                    countCorner = false;
                }
            }
            else if (dirx == -1 && diry == 0) // Left
            {
                if (fences.Contains((x, y + 1, -1, 0))) // Down
                {
                    countCorner = false;
                }
            }

            if (countCorner)
            {
                cornerCount++;
                Console.WriteLine($"Corner detected at ({x},{y}) with direction ({dirx},{diry})");
            }
            Console.WriteLine($"dx:{x}, dy:{y}, dirx: {dirx}, diry: {diry}, Corner Count: {cornerCount}");
        }

        return cornerCount;
    }
}