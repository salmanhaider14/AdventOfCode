using AdventOfCodeSupport;
namespace AdventOfCode._2024;

public class Day20:AdventBase
{
    private static readonly int[][] Directions = [[0, 1], [0, -1], [1, 0], [-1, 0]];
    protected override object InternalPart1()
    {
        var grid = Input.Text.Split("\n").Select(x => x.ToCharArray()).ToArray();
        var groups = new SortedDictionary<int, int>();
        var (sx, sy) = StartPos(grid);
        var (ex, ey) = EndPos(grid);
        var originalCost = CalculateCost(grid, sx, sy, ex, ey);
        var cheatsPos = new HashSet<(int,int,int,int)>();

        for (int i = 1; i < grid.Length - 1; i++)
        {
            for (int j = 1; j < grid[0].Length - 1; j++)
            {
                if (grid[i][j] is '#') continue;

                foreach (var direction in Directions)
                {
                    var nx = i + direction[0];
                    var ny = j + direction[1];
                    var nx1 = nx + direction[0];
                    var ny1 = ny + direction[1];

                    if (nx1 < 1 || nx1 >= grid.Length - 1 || ny1 < 1 || ny1 >= grid[0].Length - 1 || 
                        grid[nx1][ny1] is '#') continue;
                    
                    if (i <= nx && j <= ny)
                    {
                        cheatsPos.Add((nx, ny, nx1, ny1));
                    }
                }
            }
        }

        foreach (var (x,y,j,k) in cheatsPos)
        {
            var temp1 = grid[x][y];
            var temp2 = grid[j][k];

            grid[x][y] = '1';
            grid[j][k] = '2';
            var currentCost = originalCost - CalculateCost(grid, sx, sy, ex, ey);

            if (!groups.TryAdd(currentCost, 1))
                groups[currentCost]++;

            grid[x][y] = temp1;
            grid[j][k] = temp2;
        }

        foreach (var kvp in groups)
        {
            Console.WriteLine("{0},{1}", kvp.Key, kvp.Value);
        }

        return groups.Where(x => x.Key >= 100).Sum(x => x.Value);
    }
    
    protected override object InternalPart2()
    {
        var (map, start, end) = ParseGrid(Input.Text);
        var seen = new Dictionary<(int x, int y), int>();
        var queue = new Queue<((int x, int y) pos, int dist)>();
        queue.Enqueue((start, 0));

        while (queue.TryDequeue(out var current))
        {
            var (pos, dist) = current;

            if (!seen.TryAdd(pos, dist))
                continue;

            foreach (var dir in Directions)
            {
                var neighbor = (pos.x + dir[0], pos.y + dir[1]);

                if (map.Contains(neighbor))
                    queue.Enqueue((neighbor, dist + 1));
            }
        }

        var reachablePoints = seen.ToArray();
        var cheatDistance = 20;
        var minSave = 100;
        var cheatCount = 0;

        for (int i = 0; i < reachablePoints.Length; i++)
        {
            var (p1, t1) = reachablePoints[i];

            for (int j = i + 1; j < reachablePoints.Length; j++)
            {
                var (p2, t2) = reachablePoints[j];
                var dist = Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y);

                if (dist <= cheatDistance && Math.Abs(t1 - t2) - minSave >= dist)
                {
                    cheatCount++;
                }
            }
        }

        return cheatCount;
    }
    private (HashSet<(int, int)> map, (int x, int y) start, (int x, int y) end) ParseGrid(string input)
    {
        var map = new HashSet<(int, int)>();
        (int x, int y) start = (-1, -1);
        (int x, int y) end = (-1, -1);

        var rows = input.Split("\n");
        for (int y = 0; y < rows.Length; y++)
        {
            for (int x = 0; x < rows[y].Length; x++)
            {
                if (rows[y][x] != '#')
                {
                    map.Add((x, y));
                    if (rows[y][x] == 'S')
                        start = (x, y);
                    else if (rows[y][x] == 'E')
                        end = (x, y);
                }
            }
        }

        return (map, start, end);
    }
    private static int CalculateCost(char[][] grid,int startRow,int startCol,int endRow,int endCol)
    {
        var q = new Queue<(int,int)>();
        var dis = new int[grid.Length][];
        for (var i = 0; i < grid.Length; i++)
        {
            dis[i] = new int[grid[0].Length];
            for (var j = 0; j < grid[0].Length; j++)
            {
                dis[i][j] = -1;
            }
        }
        q.Enqueue((startRow,startCol));
        dis[startRow][startCol] = 0;
        while (q.Count > 0)
        {
            var (x,y) = q.Dequeue();
            if (x == endRow && y == endCol) return dis[x][y];
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
        return -1;
    }
    private static (int,int) StartPos(char[][] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] is 'S')
                {
                    return (i,j);
                }
            }
        }
        return (-1,-1);
    }
    private static (int,int) EndPos(char[][] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (grid[i][j] is 'E')
                {
                    return (i,j);
                }
            }
        }
        return (-1,-1);
    }
}