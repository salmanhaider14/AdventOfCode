using AdventOfCodeSupport;
namespace AdventOfCode._2020;

public class Day3: AdventBase
{
    protected override object InternalPart1()
    {
        var grid = Input.Text.Split("\n").Select(x => x.ToCharArray()).ToArray();
        var rows = grid.Length;
        var cols = grid[0].Length;
        var trees = 0;

        for (int i = 0, j = 0; i < rows; i ++, j = (j + 3) % cols)
        {
            if (grid[i][j] is '#')
                trees++;
        }
        return trees;
    }
    
    protected override object InternalPart2()
        {
            var grid = Input.Text.Split("\n").Select(x => x.ToCharArray()).ToArray();
            var rows = grid.Length;
            var cols = grid[0].Length;
            var trees = 0;
            long product = 1;

            for (int i = 0, j = 0; i < rows; i++, j = (j + 3) % cols)
            {
                if (grid[i][j] is '#')
                    trees++;
            }

            product *= trees;
            trees = 0;
            for (int i = 0, j = 0; i < rows; i++, j = (j + 1) % cols)
            {
                if (grid[i][j] is '#')
                    trees++;
            }
            product *= trees;
            trees = 0;
            for (int i = 0, j = 0; i < rows; i++, j = (j + 5) % cols)
            {
                if (grid[i][j] is '#')
                    trees++;
            }
            product *= trees;
            trees = 0;
            
            for (int i = 0, j = 0; i < rows; i++, j = (j + 7) % cols)
            {
                if (grid[i][j] is '#')
                    trees++;
            }
            product *= trees;
            trees = 0;
            for (int i = 0, j = 0; i < rows; i+=2, j = (j + 1) % cols)
            {
                if (grid[i][j] is '#')
                    trees++;
            }
            product *= trees;
            return product;
        }
    }
