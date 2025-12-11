using System.Text;
using AdventOfCodeSupport;
using System.Text.RegularExpressions;
namespace AdventOfCode._2025;

public partial class Day06: AdventBase
{
    [GeneratedRegex(@"\d+")]
    private static partial Regex numbersRegex();
    
    [GeneratedRegex(@"\S+")]
    private static partial Regex operatorsRegex();

    private string _exampleInput = "123 328  51 64 \n 45 64  387 23 \n  6 98  215 314\n*   +   *   +  ";
    protected override object InternalPart1()
    {
        var lines = _exampleInput.Split("\n");
        var operators = operatorsRegex().Matches(Input.Lines[^1]).Select(m => m.Value).ToArray();
        var grid = new int[Input.Lines.Length-1][];
        var solutions = new List<long>();
        for (var index = 0; index < Input.Lines.Length-1; index++)
        {
            var line = Input.Lines[index];
            var matches = numbersRegex().Matches(line);
            grid[index] = matches.Select(m => m.Value).Select(int.Parse).ToArray();
        }

        for (int i = 0; i < grid[0].Length; i++)
        {
            long sum = 0;
            long multiply = 1;
            for (int j = 0; j < grid.Length; j++)
            {
                if (operators[i] == "+")
                    sum += grid[j][i];
                else
                    multiply = grid[j][i] * multiply;
            }

            solutions.Add(operators[i] == "+" ? sum : multiply);
        }
        return solutions.Sum();
    }

    protected override object InternalPart2()
    {
        var operators = operatorsRegex().Matches(Input.Lines[^1]).Select(m => m.Value).ToArray();
        var grid = new char[Input.Lines.Length - 1][];
        var solutions = new List<long>();
        var ranges = new List<(int start, int end)>();

        // Build grid of chars
        for (var index = 0; index < Input.Lines.Length - 1; index++)
        {
            grid[index] = Input.Lines[index].ToCharArray();
        }

        // Detect blocks (non-empty column ranges)
        var i = 0;
        while (i < grid[0].Length)
        {
            if (IsEmptyCol(i, grid))
            {
                i++;
                continue;
            }

            int start = i;
            while (i < grid[0].Length && !IsEmptyCol(i, grid))
                i++;

            int end = i - 1;
            ranges.Add((start, end));
        }

        // Process each block
        foreach (var range in ranges)
        {
            var numbers = new List<long>();

            // Traverse columns from right to left (cephalopod order)
            for (int j = range.end; j >= range.start; j--)
            {
                var sb = new StringBuilder();
                for (int k = 0; k < grid.Length; k++)
                {
                    if (char.IsDigit(grid[k][j]))
                        sb.Append(grid[k][j]);
                }
                if (sb.Length > 0)
                    numbers.Add(long.Parse(sb.ToString()));
            }

            // Find operator: look for the operator in the last line at any position within this range
            var op = "+";
            for (int j = range.start; j <= range.end; j++)
            {
                var c = Input.Lines[^1][j];
                if (c == '+' || c == '*')
                {
                    op = c.ToString();
                    break;
                }
            }

            long result = op == "+" ? 0 : 1;

            foreach (var num in numbers)
            {
                if (op == "+") result += num;
                else result *= num;
            }

            solutions.Add(result);
        }

        return solutions.Sum();
    }

    private bool IsEmptyCol(int col, char[][] grid)
    {
        for (int i = 0; i < grid.Length; i++)
            if (grid[i][col] != ' ') return false;
        return true;
    }

}