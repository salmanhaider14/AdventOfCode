using AdventOfCodeSupport;
namespace AdventOfCode._2024;

public class Day16 : AdventBase
{
    private static readonly int[][] Directions =
    [
        [0, 1],  // East (Right)
        [1, 0],  // South (Down)
        [0, -1], // West (Left)
        [-1, 0] // North (Up)
    ];

    private const string TestInput =
        "###############\n#.......#....E#\n#.#.###.#.###.#\n#.....#.#...#.#\n#.###.#####.#.#\n#.#.#.......#.#\n#.#.#####.###.#\n#...........#.#\n###.#.#####.#.#\n#...#.....#.#.#\n#.#.#.###.#.#.#\n#.....#...#.#.#\n#.###.#.#.#.#.#\n#S..#.....#...#\n###############";
    protected override object InternalPart1()
    {
        var map = Input.Text.Split("\n").Select(l => l.ToCharArray()).ToArray();
        var visited = new bool[map.Length, map[0].Length, 4];
        var priorityQueue = new SortedSet<(int score, int x, int y, int direction)>();
        var (startX, startY) = FindStart(map);
        
        priorityQueue.Add((0, startX, startY, 0));

        while (priorityQueue.Count > 0)
        {
            var current = priorityQueue.Min;
            priorityQueue.Remove(current);

            var (score, x, y, direction) = current;

            if (map[x][y] == 'E')
            {
                return score;
            }
            if (visited[x, y, direction]) continue;
            visited[x, y, direction] = true;
            
            var nx = x + Directions[direction][0];
            var ny = y + Directions[direction][1];
            
            if (nx >= 0 && ny >= 0 && nx < map.Length && ny < map[0].Length && map[nx][ny] is not '#' && !visited[nx, ny, direction])
            { 
                priorityQueue.Add((score + 1, nx, ny, direction));
            }
            var clockwiseDirection = (direction + 1) % 4;
            if (!visited[x, y, clockwiseDirection])
            {
                priorityQueue.Add((score + 1000, x, y, clockwiseDirection));
            }
            
            var counterClockwiseDirection = (direction - 1 + 4) % 4;
            if (!visited[x, y, counterClockwiseDirection])
            {
                priorityQueue.Add((score + 1000, x, y, counterClockwiseDirection));
            }
        }

        return -1; 
    }
    protected override object InternalPart2()
{
    var map = Input.Text.Split("\n").Select(l => l.ToCharArray()).ToArray();
    int rows = map.Length, cols = map[0].Length;
    int moveCost = 1;
    int turnCost = 1000;

    var (startX, startY) = FindStart(map);

    var distance = new Dictionary<(int x, int y, int dir), int>();
    var predecessors = new Dictionary<(int x, int y, int dir), List<(int x, int y, int dir)>>();
    var queue = new Queue<(int x, int y, int dir)>();

    queue.Enqueue((startX, startY, 0)); // Start facing East
    distance[(startX, startY, 0)] = 0;

    while (queue.Count > 0)
    {
        var (x, y, dir) = queue.Dequeue();

        // Check movements in current direction and turns
        for (int i = 0; i < 4; i++)
        {
            int nextDir = (dir + i) % 4; // 0 for forward, 1 for right turn, 3 for left turn
            int dx = Directions[nextDir][0];
            int dy = Directions[nextDir][1];
            int nx = x + dx, ny = y + dy;
            
            if (nx < 0 || ny < 0 || nx >= rows || ny >= cols || map[nx][ny] == '#') 
                continue;

            int cost = (i == 0 ? moveCost : turnCost);
            int newDist = distance[(x, y, dir)] + cost;

            if (!distance.TryGetValue((nx, ny, nextDir), out int oldDist) || newDist < oldDist)
            {
                distance[(nx, ny, nextDir)] = newDist;
                
                if (!predecessors.ContainsKey((nx, ny, nextDir)))
                    predecessors[(nx, ny, nextDir)] = new List<(int, int, int)>();
                else
                    predecessors[(nx, ny, nextDir)].Clear(); // Clear previous paths if we found a better one

                predecessors[(nx, ny, nextDir)].Add((x, y, dir));
                queue.Enqueue((nx, ny, nextDir));
            }
            else if (newDist == oldDist)
            {
                predecessors[(nx, ny, nextDir)].Add((x, y, dir)); // Multiple ways to reach with same cost
            }
        }
    }

    var bestPathTiles = new HashSet<(int x, int y)>();
    var stack = new Stack<(int x, int y, int dir)>();

    // Find the minimum score to reach 'E'
    int minScore = int.MaxValue;
    List<(int x, int y, int dir)> endPoints = new List<(int x, int y, int dir)>();

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            if (map[i][j] == 'E')
            {
                for (int d = 0; d < 4; d++) // Check from all directions
                {
                    if (distance.TryGetValue((i, j, d), out int dist))
                    {
                        if (dist < minScore)
                        {
                            minScore = dist;
                            endPoints.Clear();
                            endPoints.Add((i, j, d));
                        }
                        else if (dist == minScore)
                        {
                            endPoints.Add((i, j, d));
                        }
                    }
                }
            }
        }
    }

    // Backtrack from all endpoints with min score
    foreach (var end in endPoints)
    {
        stack.Push(end);
    }

    while (stack.Count > 0)
    {
        var (currentX, currentY, currentDir) = stack.Pop();
        bestPathTiles.Add((currentX, currentY));

        if (predecessors.TryGetValue((currentX, currentY, currentDir), out var preds))
        {
            foreach (var pred in preds)
            {
                stack.Push(pred);
            }
        }
    }

    return bestPathTiles.Count;
}
    private static (int, int) FindStart(char[][] map)
    {
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[0].Length; j++)
            {
                if (map[i][j] is 'S') return (i, j);
            }
        }

        return (-1, -1);
    }


}
