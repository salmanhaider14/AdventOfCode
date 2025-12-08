using AdventOfCodeSupport;
namespace AdventOfCode._2020;

public class Day11: AdventBase
{
    private readonly List<List<int>> _directions = [[0, 1], [0, -1], [-1, 0], [1, 0], [1, 1], [1, -1], [-1, 1], [-1, -1]];
    
    protected override object InternalPart1()
    {
        var grid = Input.Lines.Select(x => x.ToCharArray()).ToArray();
        var occupiedSeats = 0;
        for (int k = 0; k < int.MaxValue; k++)
        {
            var newGrid = new char[grid.Length][];
            for (int i = 0; i < grid.Length; i++)
                newGrid[i] = (char[])grid[i].Clone();
            
            var changed = false;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    var cell = grid[i][j];
                    switch (cell)
                    {
                        case 'L':
                        {
                            var occupied = false;
                            foreach (var direction in _directions)
                            {
                                var nr = i + direction[0];
                                var nc = j + direction[1];
                                if(nr < 0 || nr >= grid.Length || nc < 0 || nc >= grid[0].Length) continue;
                                if (grid[nr][nc] is not '#') continue;
                                occupied = true;
                                break;
                            }

                            if (!occupied)
                            {
                                newGrid[i][j] = '#';
                                changed = true;
                            }
                            break;
                        }
                        case '#':
                        {
                            var occupied = 0;
                            foreach (var direction in _directions)
                            {
                                var nr = i + direction[0];
                                var nc = j + direction[1];
                                if(nr < 0 || nr >= grid.Length || nc < 0 || nc >= grid[0].Length) continue;
                                if (grid[nr][nc] is not '#') continue;
                                occupied++;
                                if(occupied >=4 )break;
                            }

                            if (occupied >= 4)
                            {
                                newGrid[i][j] = 'L';
                                changed = true;
                            }
                            break;
                        }
                    }
                }
            }     
            if(!changed) break;
            grid = newGrid;
        }
        
        foreach (var t in grid)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (t[j] is '#') occupiedSeats++;
            }
        }
        return occupiedSeats;
    }

    protected override object InternalPart2()
    {
        var grid = Input.Lines.Select(x => x.ToCharArray()).ToArray();
        var occupiedSeats = 0;
        for (int k = 0; k < int.MaxValue; k++)
        {
            var newGrid = new char[grid.Length][];
            for (int i = 0; i < grid.Length; i++)
                newGrid[i] = (char[])grid[i].Clone();
            
            var changed = false;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    var cell = grid[i][j];
                    switch (cell)
                    {
                        case 'L':
                        {
                            var occupied = false;
                            foreach (var direction in _directions)
                            {
                                if (CheckAdjacent(grid, i, j, direction)) continue;
                                occupied = true;
                                break;
                            }

                            if (!occupied)
                            {
                                newGrid[i][j] = '#';
                                changed = true;
                            }

                            break;
                        }
                        case '#':
                        {
                            var occupied = 0;
                            foreach (var direction in _directions)
                            {
                                if (!CheckAdjacent(grid, i, j, direction))occupied++;
                                if(occupied >= 5) break;
                            }

                            if (occupied >= 5)
                            {
                                newGrid[i][j] = 'L';
                                changed = true;
                            }

                            break;
                        }
                    }
                }
            }     
            if(!changed) break;
            grid = newGrid;
        }
        
        foreach (var t in grid)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                if (t[j] is '#') occupiedSeats++;
            }
        }
        return occupiedSeats;
    }
    private static bool CheckAdjacent(char[][] grid, int row, int col, List<int> direction)
    {
        var nr = row+ direction[0];
        var nc = col + direction[1];

        if (nr < 0 || nr >= grid.Length || nc < 0 || nc >= grid[0].Length) return true;
        return grid[nr][nc] switch
        {
            '#' => false,
            'L' => true,
            _ => CheckAdjacent(grid, nr, nc, direction)
        };
    }
}