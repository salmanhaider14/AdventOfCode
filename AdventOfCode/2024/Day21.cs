using AdventOfCodeSupport;

namespace AdventOfCode._2024;

public class Day21 : AdventBase
{
    private static readonly HashSet<(int dx, int dy, char)> Directions =
        [(0, 1, '>'), (1, 0, 'v'), (-1, 0, '^'), (0, -1, '<')];

    private const string TestInput = "029A\n980A\n179A\n456A\n379A";

    protected override object InternalPart1()
    {
        var codes = TestInput.Split("\n");
        var keypad = new char[][] { ['7', '8', '9'], ['4', '5', '6'], ['1', '2', '3'], ['X', '0', 'A'] };
        var dpad = new char[][] { ['X', '^', 'A'], ['<', 'v', '>'] };
        var sum = 0;

        foreach (var code in codes)
        {
            var allKeypadPaths = GetAllPathsForSequence(keypad, code);
            var shortestFinalLength = int.MaxValue;

            foreach (var keypadPath in allKeypadPaths)
            {
                var allFirstRoboPaths = GetAllControlPaths(dpad, keypadPath);

                foreach (var firstRoboPath in allFirstRoboPaths)
                {
                    var allSecondRoboPaths = GetAllControlPaths(dpad, firstRoboPath);

                    var minLength = allSecondRoboPaths.Min(path => path.Count);
                    shortestFinalLength = Math.Min(shortestFinalLength, minLength);
                }
            }

            var complexity = shortestFinalLength * int.Parse(code.TrimEnd('A'));
            Console.WriteLine("Complexity For Code {0}: {1}", code, complexity);
            sum += complexity;
        }

        return sum;
    }

    private List<List<char>> GetAllPathsForSequence(char[][] grid, string sequence)
    {
        var currentPaths = new List<List<char>> { new() };
        var currentX = grid.Length - 1;
        var currentY = grid[0].Length - 1;

        foreach (var target in sequence)
        {
            var newPaths = new List<List<char>>();
            var shortestLength = int.MaxValue;

            foreach (var currentPath in currentPaths)
            {
                // Reset position for this path
                var x = currentX;
                var y = currentY;
                foreach (var move in currentPath)
                {
                    if (move == 'A') continue;
                    var dir = Directions.First(d => d.Item3 == move);
                    x += dir.dx;
                    y += dir.dy;
                }

                // Find all paths to the next target from this position
                var targetPaths = FindTarget(grid, x, y, target).ToList();
                if (!targetPaths.Any()) continue;

                var minLength = targetPaths.Min(p => p.Item3.Count);
                shortestLength = Math.Min(shortestLength, currentPath.Count + minLength);

                // Add all paths of minimum length
                foreach (var (_, _, path) in targetPaths.Where(p => p.Item3.Count == minLength))
                {
                    newPaths.Add([..currentPath, ..path]);
                }
            }

            // Keep only paths of the shortest length
            currentPaths = newPaths.Where(p => p.Count == shortestLength).ToList();
        }

        return currentPaths;
    }

    private List<List<char>> GetAllControlPaths(char[][] grid, List<char> sequence)
    {
        var currentPaths = new List<List<char>> { new() };
        var currentX = 0;
        var currentY = grid[0].Length - 1;

        foreach (var target in sequence)
        {
            var newPaths = new List<List<char>>();
            var shortestLength = int.MaxValue;

            foreach (var currentPath in currentPaths)
            {
                // Reset position for this path
                var x = currentX;
                var y = currentY;
                foreach (var move in currentPath)
                {
                    if (move == 'A') continue;
                    var dir = Directions.First(d => d.Item3 == move);
                    x += dir.dx;
                    y += dir.dy;
                }

                // Find all paths to the next target
                var targetPaths = FindTarget(grid, x, y, target).ToList();
                if (!targetPaths.Any()) continue;

                var minLength = targetPaths.Min(p => p.Item3.Count);
                shortestLength = Math.Min(shortestLength, currentPath.Count + minLength);

                // Add all paths of minimum length
                foreach (var (_, _, path) in targetPaths.Where(p => p.Item3.Count == minLength))
                {
                    newPaths.Add([..currentPath, ..path]);
                }
            }

            // Keep only paths of the shortest length
            currentPaths = newPaths.Where(p => p.Count == shortestLength).ToList();
        }

        return currentPaths;
    }

    private static IEnumerable<(int, int, List<char>)> FindTarget(char[][] grid, int startRow, int startCol,
        char target)
    {
        var queue = new Queue<(int x, int y, List<char> path, HashSet<(int, int)> visited)>();
        queue.Enqueue((startRow, startCol, new List<char>(), new HashSet<(int, int)> { (startRow, startCol) }));
        var shortestLength = int.MaxValue;
        var foundPaths = new List<(int, int, List<char>)>();

        while (queue.Count > 0)
        {
            var (x, y, path, visited) = queue.Dequeue();

            if (path.Count > shortestLength) continue;

            if (grid[x][y] == target)
            {
                var completePath = new List<char>(path) { 'A' };
                if (completePath.Count <= shortestLength)
                {
                    if (completePath.Count < shortestLength)
                    {
                        shortestLength = completePath.Count;
                        foundPaths.Clear();
                    }

                    foundPaths.Add((x, y, completePath));
                }

                continue;
            }

            foreach (var (dx, dy, icon) in Directions)
            {
                var nx = x + dx;
                var ny = y + dy;

                if (nx < 0 || nx >= grid.Length || ny < 0 || ny >= grid[0].Length ||
                    grid[nx][ny] == 'X' || visited.Contains((nx, ny))) continue;

                var newVisited = new HashSet<(int, int)>(visited) { (nx, ny) };
                var newPath = new List<char>(path) { icon };
                queue.Enqueue((nx, ny, newPath, newVisited));
            }
        }

        return foundPaths;
    }

protected override object InternalPart2()
    {
        var codes = Input.Text.Split("\n");
        var total = 0L;
        var cache = new Dictionary<((int, int), (int, int), int), long>();

        foreach (var code in codes)
        {
            var count = NumpadPresses(code, 25, cache);
            total += count * int.Parse(code.Substring(0, 3));
        }

        return total;
    }

    private static long NumpadPresses(string code, int depth, Dictionary<((int, int), (int, int), int), long> cache)
    {
        var count = 0L;
        var curr = (3, 2); // numpad A

        foreach (var c in code)
        {
            var next = Math.DivRem("789456123.0A".IndexOf(c), 3);
            count += NumpadPresses(curr, next, depth, cache);
            curr = next;
        }

        return count;
    }

    private static long NumpadPresses((int, int) beg, (int, int) end, int depth, Dictionary<((int, int), (int, int), int), long> cache)
    {
        var best = long.MaxValue;
        var work = new Queue<((int, int), string)>();
        work.Enqueue((beg, ""));

        while (work.TryDequeue(out var state))
        {
            var (pos, path) = state;

            if (pos == (3, 0)) // numpad gap
            {
                continue;
            }

            if (pos == end)
            {
                best = Math.Min(best, DirpadPresses(path + 'A', depth, cache));
                continue;
            }

            QueueNextMoves(work, pos, end, path);
        }

        return best;
    }

    private static long DirpadPresses(string code, int depth, Dictionary<((int, int), (int, int), int), long> cache)
    {
        if (depth is 0)
        {
            return code.Length;
        }

        var count = 0L;
        var curr = (0, 2); // dirpad A

        foreach (var c in code)
        {
            var next = Math.DivRem(".^A<v>".IndexOf(c), 3);
            count += DirpadPresses(curr, next, depth, cache);
            curr = next;
        }

        return count;
    }

    private static long DirpadPresses((int, int) beg, (int, int) end, int depth, Dictionary<((int, int), (int, int), int), long> cache)
    {
        if (cache.TryGetValue((beg, end, depth), out var result))
        {
            return result;
        }

        var best = long.MaxValue;
        var work = new Queue<((int, int), string)>();
        work.Enqueue((beg, ""));

        while (work.TryDequeue(out var state))
        {
            var (pos, path) = state;

            if (pos == (0, 0)) // dirpad gap
            {
                continue;
            }

            if (pos == end)
            {
                best = Math.Min(best, DirpadPresses(path + 'A', depth - 1, cache));
                continue;
            }

            QueueNextMoves(work, pos, end, path);
        }

        return cache[(beg, end, depth)] = best;
    }

    private static void QueueNextMoves(Queue<((int, int), string)> queue, (int, int) pos, (int, int) end, string path)
    {
        if (end.Item1 < pos.Item1)
        {
            queue.Enqueue((pos with { Item1 = pos.Item1 - 1 }, path + '^'));
        }
        else if (pos.Item1 < end.Item1)
        {
            queue.Enqueue((pos with { Item1 = pos.Item1 + 1 }, path + 'v'));
        }

        if (end.Item2 < pos.Item2)
        {
            queue.Enqueue((pos with { Item2 = pos.Item2 - 1 }, path + '<'));
        }
        else if (pos.Item2 < end.Item2)
        {
            queue.Enqueue((pos with { Item2 = pos.Item2 + 1 }, path + '>'));
        }
    }
}