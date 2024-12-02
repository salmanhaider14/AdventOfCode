using System.Text;
using System.Text.RegularExpressions;
using AdventOfCodeSupport;
using Microsoft.Extensions.Primitives;

namespace AdventOfCode._2023;

public class Day03 : AdventBase
{
    protected override object InternalPart1()
    {
            var schematic = Input.Lines;

            var symbols = new HashSet<char> { '*', '#', '$', '+', '-', '=', '/', '%', '&', '@' };
            int rows = schematic.Length;
            int cols = schematic[0].Length;
            int sum = 0;
            
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int r = 0; r < rows; r++)
            {
                var matches = Regex.Matches(schematic[r], @"\d+");
                foreach (Match match in matches)
                {
                    string numberStr = match.Value;
                    int startCol = match.Index;
                    int endCol = startCol + numberStr.Length - 1;

                    // Check if the number is adjacent to a symbol
                    bool isPartNumber = false;

                    for (int c = startCol; c <= endCol; c++)
                    {
                        for (int d = 0; d < 8; d++)
                        {
                            int nr = r + dx[d];
                            int nc = c + dy[d];

                            if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && symbols.Contains(schematic[nr][nc]))
                            {
                                isPartNumber = true;
                                break;
                            }
                        }

                        if (isPartNumber) break;
                    }

                    if (isPartNumber)
                    {
                        sum += int.Parse(numberStr);
                    }
                }
            }

            return sum;
    }

    
    protected override object InternalPart2()
    {
        string[] schematic = Input.Lines;

        int rows = schematic.Length;
        int cols = schematic[0].Length;
        long totalGearRatioSum = 0;

        // Directions for adjacent cells (8 directions)
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (schematic[r][c] == '*') // Check for '*'
                {
                    List<int> adjacentNumbers = new List<int>();

                    // Check all 8 directions for part numbers
                    for (int d = 0; d < 8; d++)
                    {
                        int nr = r + dx[d];
                        int nc = c + dy[d];

                        if (nr >= 0 && nr < rows && nc >= 0 && nc < cols)
                        {
                            // Try to extract a part number starting from this cell
                            Match match = Regex.Match(schematic[nr].Substring(nc), @"^\d+");
                            if (match.Success)
                            {
                                adjacentNumbers.Add(int.Parse(match.Value));
                            }
                        }
                    }

                    // If the '*' is adjacent to exactly two part numbers
                    if (adjacentNumbers.Count == 2)
                    {
                        int gearRatio = adjacentNumbers[0] * adjacentNumbers[1];
                        totalGearRatioSum += gearRatio;
                    }
                }
            }
        }

        return totalGearRatioSum;
    }
   
}