using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AdventOfCode._2024;

public class Day13: AdventBase
{
    //Here I'm just brute-forcing 
    protected override object InternalPart1()
    {
        var minmumTokens = 0;
        var games = Input.Text.Split("\n\n");
        foreach (var game in games)
        {
            var parts = game.Split("\n");
            var button1 = Regex.Matches(parts[0], @"\d+");
            var xA = int.Parse(button1[0].Value);
            var yA = int.Parse(button1[1].Value);
            
            var button2 = Regex.Matches(parts[1], @"\d+");
            var xB = int.Parse(button2[0].Value);
            var yB = int.Parse(button2[1].Value);
            
            var prize = Regex.Matches(parts[2], @"\d+");
            var prizeX = int.Parse(prize[0].Value);
            var prizeY = int.Parse(prize[1].Value);
            
            var currentXA = 0;
            var currentYA = 0;
            var found = false;

            for (int i = 1; i <= 100 && !found; i++)
            {
                currentXA += xA;
                currentYA += yA;
                
                var currentXB = 0;
                var currentYB = 0;
                
                for (int j = 1; j <= 100; j++)
                {
                    currentXB += xB;
                    currentYB += yB;

                    if (currentXA + currentXB == prizeX && currentYA + currentYB == prizeY)
                    {
                        found = true;
                        minmumTokens += (i * 3) + j;
                        break;
                    }
                       
                }
            }
        }
        return minmumTokens;
    }
    //Cramer Rule Approach , We back to school guys 
    protected override object InternalPart2()
    {
        long minmumTokens = 0;
        var games = Input.Text.Split("\n\n");
        foreach (var game in games)
        {
            var parts = game.Split("\n");
            var button1 = Regex.Matches(parts[0], @"\d+");
            var xA = int.Parse(button1[0].Value);
            var yA = int.Parse(button1[1].Value);

            var button2 = Regex.Matches(parts[1], @"\d+");
            var xB = int.Parse(button2[0].Value);
            var yB = int.Parse(button2[1].Value);


            var prize = Regex.Matches(parts[2], @"\d+");
            long prizeX = int.Parse(prize[0].Value) + 10000000000000;
            long prizeY = int.Parse(prize[1].Value) + 10000000000000;

            Console.WriteLine("PrizeX:{0}, PrizeY: {1}", prizeX, prizeY);

            int[][] A = [[xA, xB], [yA,yB]];
            long[] B = [prizeX, prizeY];
            var res = ApplyCramerRule(A, B);
            Console.WriteLine("Cramer's Rule Results -> X: {0}, Y: {1}", res.Item1, res.Item2);
            
            if (res != (-1, -1))
            {
                var (x, y) = res;
                if(x * xA + y * xB == prizeX && x * yA + y * yB == prizeY )
                  minmumTokens += (3 * res.Item1) + res.Item2;
            }
        }
        return minmumTokens;
    }
    private static (long, long) ApplyCramerRule(int[][] A,long[] B)
    {
        long D = (A[0][0] * A[1][1]) - (A[0][1] * A[1][0]);
        if (D == 0) return (-1, -1);
        long Dx = (B[0] * A[1][1]) - (B[1] * A[0][1]);
        long Dy = (A[0][0] * B[1]) - (B[0] * A[1][0]);

        long x = Dx / D;
        long y = Dy / D;
        
        return (x, y);
    }
}