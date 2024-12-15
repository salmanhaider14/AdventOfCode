using AdventOfCodeSupport;
using BenchmarkDotNet.Disassemblers;

namespace AdventOfCode._2024;

public class Day15 : AdventBase
{
    static readonly Dictionary<char, sbyte[]> directions = new() { { '^', [-1, 0] }, { 'v', [1, 0] },{ '>', [0, 1] },{ '<', [0, -1] } };
    protected override object InternalPart1()
    {
        var parts = Input.Text.Split("\n\n");
        var map = parts[0].Split("\n").Select(x => x.ToCharArray()).ToArray();
        var moves = parts[1].Replace("\n","");
        var sum = 0;
        for (var i = 0; i < map.Length; i++)
                for (var j = 0; j < map[0].Length; j++) 
                    if(map[i][j] is '@') Move(map,i,j,moves);
        
        for (var i = 0; i < map.Length; i++)
                 for (var j = 0; j < map[0].Length; j++)
                     if (map[i][j] is 'O') sum += (100 * i) + j;
        return sum;
    }
    private static void Move(char[][] map, int startRow, int startCol, string moves)
    {
        map[startRow][startCol] = '.';
        var row = startRow;
        var col = startCol;
        foreach (var move in moves)
        {
            var dir = directions[move];
            var newRow = row + dir[0];
            var newCol = col + dir[1];
            switch (map[newRow][newCol])
            {
                case '#':
                    continue;
                case '.':
                    (row, col) = (newRow, newCol);
                    break;
                case 'O':
                    var (resRow,resCol) = PushTheBoxes(map, row, col, dir);
                    (row, col) = (resRow, resCol);
                    break;
            }
        }
    }
    private static (int,int) PushTheBoxes(char[][] map, int startRow, int startCol,sbyte[] direction)
    {
        var row = startRow;
        var col = startCol;
        var stack = new Stack<(int, int)>();
        var foundSpace = false;
        
        while (true)
        {
            row += direction[0];
            col += direction[1];

            if (map[row][col] == '#') 
                return (startRow, startCol); 

            if (map[row][col] == '.') 
            {
                foundSpace = true;
                break; 
            }
            if (map[row][col] == 'O')
                stack.Push((row, col)); 
        }
        
        if (!foundSpace) return (startRow, startCol);
        
        while (stack.Count > 0)
        {
            var (boxRow, boxCol) = stack.Pop();
            (map[row][col], map[boxRow][boxCol]) = (map[boxRow][boxCol], map[row][col]);
            (row, col) = (boxRow, boxCol);
        }
        return (row, col);
    }
    
    protected override object InternalPart2()
    {
        var parts = Input.Text.Split("\n\n");
        var originalMap = parts[0].Split("\n").Select(line => line.ToCharArray()).ToArray();
        var moves = parts[1].Replace("\n", "");
        var newMap = new char[originalMap.Length][];
        var sum = 0;
        for (int i = 0; i < originalMap.Length; i++)
        {
            var currentIndex = -1;
            newMap[i] = new char[originalMap[0].Length * 2];
            for (int j = 0; j < originalMap[0].Length; j++)
            {
                if (originalMap[i][j] is '#')
                {
                    newMap[i][++currentIndex] = '#';
                    newMap[i][++currentIndex] = '#';
                }
                if (originalMap[i][j] is '.')
                {
                    newMap[i][++currentIndex] = '.';
                    newMap[i][++currentIndex] = '.';
                }
                if (originalMap[i][j] is 'O')
                {
                    newMap[i][++currentIndex] = '[';
                    newMap[i][++currentIndex] = ']';
                }
                if (originalMap[i][j] is '@')
                {
                    newMap[i][++currentIndex] = '@';
                    newMap[i][++currentIndex] = '.';
                }
            }
        }
        int rx = -1, ry = -1;
        
        for (int i = 0; i < newMap.Length; i++)
        {
            for (int j = 0; j < newMap[0].Length; j++)
            {
                if (newMap[i][j] is '@')
                {
                    rx = j;
                    ry = i;
                }
            }
        }
        var nextPoints = new List<(int x, int y)>();
        var scratch = new List<(int x, int y)>();
        var boxes = new List<(int x, int y)>();

        foreach (var c in moves)
        {
            var (dx, dy) = (directions[c][1], directions[c][0]);

            if (dy is 0)
            {
                if (newMap[ry][rx + dx] is '.')
                {
                    newMap[ry][rx] = '.';
                    rx += dx;
                    newMap[ry][rx] = '@';
                    continue;
                }

                var (nx, ny) = (rx + dx, ry);
                while (newMap[ny][nx] is '[' or ']')
                    nx += dx;

                if (newMap[ny][nx] == '#')
                    continue;

                newMap[ry][rx] = '.';
                rx += dx;
                (var ch, newMap[ry][rx]) = (newMap[ry][rx], '@');

                (nx, ny) = (rx + dx, ry);
                while (newMap[ny][nx] is not '.')
                {
                    (ch, newMap[ny][nx]) = (newMap[ny][nx], ch);
                    nx += dx;
                }

                newMap[ny][nx] = ch;
            }
            else
            {
                if (newMap[ry + dy][rx] is '#')
                    continue;

                if (newMap[ry + dy][rx] is '.')
                {
                    newMap[ry][rx] = '.';
                    ry += dy;
                    newMap[ry][rx] = '@';
                    continue;
                }

                boxes.Clear();
                nextPoints.Clear();
                nextPoints.Add((rx, ry + dy));

                var flag = true;
                while (nextPoints.Count != 0)
                {
                    scratch.Clear();

                    foreach (var (px, py) in nextPoints)
                    {
                        if (newMap[py][px] is '#')
                        {
                            flag = false;
                            scratch.Clear();
                            break;
                        }

                        if (newMap[py][px] is '[')
                        {
                            boxes.Add((px, py));
                            scratch.Add((px, py + dy));
                            scratch.Add((px + 1, py + dy));
                        }
                        else if (newMap[py][px] is ']')
                        {
                            boxes.Add((px - 1, py));
                            scratch.Add((px, py + dy));
                            scratch.Add((px - 1, py + dy));
                        }
                    }

                    (scratch, nextPoints) = (nextPoints, scratch);
                }

                if (!flag)
                    continue;

                for (var i = boxes.Count - 1; i >= 0; i--)
                {
                    var (bx, by) = boxes[i];
                    (newMap[by + dy][bx], newMap[by][bx]) = ('[', '.');
                    (newMap[by + dy][bx + 1], newMap[by][bx + 1]) = (']', '.');
                }

                newMap[ry][rx] = '.';
                ry += dy;
                newMap[ry][rx] = '@';
            }
        }


        for (int i = 0; i < newMap.Length; i++)
        {
            for (int j = 0; j < newMap[0].Length; j++)
            {
                if (newMap[i][j] is '[') sum += (100 * i) + j;
            }
        }
        return sum;
    }

}