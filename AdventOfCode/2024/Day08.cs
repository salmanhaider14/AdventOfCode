using AdventOfCodeSupport;
namespace AdventOfCode._2024;

public class Day08 : AdventBase
{
    protected override object InternalPart1()
    {
        var grid = Input.Lines.Select(x => x.ToCharArray()).ToArray();
        var rows = grid.Length;
        var cols = grid[0].Length;
        var antinodeSet = new HashSet<(int, int)>();
        
        for (int row1 = 0; row1 < rows; row1++)
        {
            for (int col1 = 0; col1 < cols; col1++)
            {
                if (!char.IsLetterOrDigit(grid[row1][col1])) continue;

                for (int row2 = 0; row2 < rows; row2++)
                {
                    for (int col2 = 0; col2 < cols; col2++)
                    {
                        if (grid[row1][col1] != grid[row2][col2] || (row1 == row2 && col1 == col2)) continue;
                        
                        int antiRow = row1 + (row2 - row1) * 2;
                        int antiCol = col1 + (col2 - col1) * 2;
                        
                        if (antiRow >= 0 && antiRow < rows && 
                            antiCol >= 0 && antiCol < cols)
                        {
                            antinodeSet.Add((antiRow, antiCol));
                        }
                    }
                }
            }
        }

        return antinodeSet.Count;
    }
    
    protected override object InternalPart2()
{
    var grid = Input.Lines.Select(x => x.ToCharArray()).ToArray();
    var rows = grid.Length;
    var cols = grid[0].Length;
    var antinodeSet = new HashSet<(int, int)>();
    
    var antennasByFrequency = new Dictionary<char, List<(int row, int col)>>();
    for (int row = 0; row < rows; row++)
    {
        for (int col = 0; col < cols; col++)
        {
            if (char.IsLetterOrDigit(grid[row][col]))
            {
                if (!antennasByFrequency.ContainsKey(grid[row][col]))
                {
                    antennasByFrequency[grid[row][col]] = new List<(int, int)>();
                }
                antennasByFrequency[grid[row][col]].Add((row, col));
            }
        }
    }
    
    foreach (var frequencyGroup in antennasByFrequency)
    {
        var antennas = frequencyGroup.Value;
        
        for (int i = 0; i < antennas.Count; i++)
        {
            for (int j = i + 1; j < antennas.Count; j++)
            {
                var (row1, col1) = antennas[i];
                var (row2, col2) = antennas[j];
                
                int rowDiff = row2 - row1;
                int colDiff = col2 - col1;
                
                int currentRow = row1;
                int currentCol = col1;
                
                for (int sign = -1; sign <= 1; sign += 2)
                {
                    while (true)
                    {
                        currentRow += sign * rowDiff;
                        currentCol += sign * colDiff;
                        
                        if (currentRow < 0 || currentRow >= rows || 
                            currentCol < 0 || currentCol >= cols)
                        {
                            break;
                        }
                        
                        antinodeSet.Add((currentRow, currentCol));
                    }
                }
            }
        }
    }
    
    return antinodeSet.Count;
}
}