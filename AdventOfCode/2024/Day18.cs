using AdventOfCodeSupport;
namespace AdventOfCode._2024;

public class Day18: AdventBase
{
    private static readonly int[][] Directions = [[0, 1], [0, -1], [1, 0], [-1, 0]];
    protected override object InternalPart1()
    {
        var grid = new char[71][];
        var bytes = Input.Lines.Take(1024).Select(x => x.Split(",")).Select(y => (byte.Parse(y[0]),byte.Parse(y[1])));
        for (var i = 0; i < grid.Length; i++)
        {
            grid[i] = new char[71];
            for (var j = 0; j < grid[i].Length; j++)
            {
                grid[i][j] = '.';
            }
        }
        foreach (var (x,y) in bytes)
        {
            grid[y][x] = '#';
        }

        return FindPath(grid);
    }
    protected override object InternalPart2()
    {
        var grid = new char[71][];
        var bytes = Input.Lines.Select(x => x.Split(",")).Select(y => (int.Parse(y[0]),int.Parse(y[1]))).ToArray();
        for (var i = 0; i < grid.Length; i++)
        {
            grid[i] = new char[71];
            for (var j = 0; j < grid[i].Length; j++)
            {
                grid[i][j] = '.';
            }
        }
        
        for (var i = 0; i < bytes.Length; i++)
        {
            var (x, y) = bytes[i];
            grid[y][x] = '#';
            if (i <= 1024) continue;
            if (FindPath(grid) is -1) return $"{x},{y}";
        }
        return -1;
    }
    private static int FindPath(char[][] grid)
    {
        var q = new Queue<(int,int)>();
        var dis = new int[grid.Length][];
        for (var i = 0; i < grid.Length; i++)
        {
            dis[i] = new int[71];
            for (var j = 0; j < grid[i].Length; j++)
            {
                dis[i][j] = -1;
            }
        }
        q.Enqueue((0,0));
        dis[0][0] = 0;
        while (q.Count > 0)
        {
            var (x,y) = q.Dequeue();
            foreach (var direction in Directions)
            {
                var nx =  x + direction[0];
                var ny = y + direction[1];

                if (nx < 0 || nx >= grid.Length || ny < 0 || ny >= grid[0].Length || dis[nx][ny] is not -1 ||
                    grid[nx][ny] is '#') continue;
                
                dis[nx][ny] = dis[x][y] + 1;
                q.Enqueue((nx,ny));
            }
        }
        return dis[70][70];
    }
}