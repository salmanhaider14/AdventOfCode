using System.Text.RegularExpressions;
using AdventOfCodeSupport;
namespace AdventOfCode._2024;
public partial class Day13: AdventBase
{
    [GeneratedRegex(@"\d+")]
    private static partial Regex regex();
    protected override object InternalPart1()
    {
        long minimumTokens = 0;
        var games = Input.Text.Split("\n\n");
   
        foreach (var game in games)
        {
            var parts = regex().Matches(game);
            
            var xA = byte.Parse(parts[0].Value);
            var yA = byte.Parse(parts[1].Value);
            
            var xB = byte.Parse(parts[2].Value);
            var yB = byte.Parse(parts[3].Value);
            
            var prizeX = int.Parse(parts[4].Value);
            var prizeY = int.Parse(parts[5].Value);
            
            byte[][] A = [[xA, xB], [yA,yB]];
            long[] B = [prizeX, prizeY];
            var res = ApplyCramerRule(A, B);

            if (res == (-1, -1)) continue;
            
            var (x, y) = res;
            if(x * xA + y * xB == prizeX && x * yA + y * yB == prizeY )
                minimumTokens += (3 * x) + y;
        }
        return minimumTokens;
    }
    [GeneratedRegex(@"\d+")]
    private static partial Regex regex2();
    protected override object InternalPart2()
    {
        long minimumTokens = 0;
        var games = Input.Text.Split("\n\n");
        foreach (var game in games)
        {
            var parts = regex2().Matches(game);
            
            var xA = byte.Parse(parts[0].Value);
            var yA = byte.Parse(parts[1].Value);
            
            var xB = byte.Parse(parts[2].Value);
            var yB = byte.Parse(parts[3].Value);
            
            long prizeX = int.Parse(parts[4].Value) + 10000000000000;
            long prizeY = int.Parse(parts[5].Value) + 10000000000000;

            byte[][] A = [[xA, xB], [yA,yB]];
            long[] B = [prizeX, prizeY];
            var res = ApplyCramerRule(A, B);

            if (res == (-1, -1)) continue;
            
            var (x, y) = res;
            if(x * xA + y * xB == prizeX && x * yA + y * yB == prizeY )
                minimumTokens += (3 * x) + y;
        }
        return minimumTokens;
    }
    private static (long, long) ApplyCramerRule(byte[][] A,long[] B)
    {
        long D = (A[0][0] * A[1][1]) - (A[0][1] * A[1][0]);
        if (D == 0) return (-1, -1);
        var Dx = (B[0] * A[1][1]) - (B[1] * A[0][1]);
        var Dy = (A[0][0] * B[1]) - (B[0] * A[1][0]);

        var x = Dx / D;
        var y = Dy / D;
        
        return (x, y);
    }
}