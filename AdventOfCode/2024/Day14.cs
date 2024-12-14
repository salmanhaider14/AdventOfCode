using System.Text.RegularExpressions;
using AdventOfCodeSupport;
namespace AdventOfCode._2024;

public class Day14 : AdventBase
{
    const int width = 101;
    const int length = 103;
    protected override object InternalPart1()
    {
        var finalPositions = new List<(int, int)>();
        foreach (var input in Input.Lines)
        {
            var parts = Regex.Matches(input, @"-?\d+");
            var pX = int.Parse(parts[0].Value);
            var pY = int.Parse(parts[1].Value);
            var vX = int.Parse(parts[2].Value);
            var vY = int.Parse(parts[3].Value);

            for (var i = 0; i < 100; i++)
            {
                pX = (pX + vX + width) % width;
                pY = (pY + vY + length) % length;
            }

            finalPositions.Add((pX, pY));
        }

        var firstQuadrantCount = 0;
        var secondQuadrantCount = 0;
        var thirdQuadrantCount = 0;
        var fourthQuadrantCount = 0;

        foreach (var (row, col) in finalPositions)
        {
            if (row == width / 2 || col == length / 2) continue;

            if (row < width / 2 && col < length / 2) firstQuadrantCount++;
            if (row >= width / 2 && col < length / 2) secondQuadrantCount++;
            if (row < width / 2 && col >= length / 2) thirdQuadrantCount++;
            if (row >= width / 2 && col >= length / 2) fourthQuadrantCount++;
        }

        var safetyFactor = firstQuadrantCount * secondQuadrantCount * thirdQuadrantCount * fourthQuadrantCount;
        return safetyFactor;
    }
    protected override object InternalPart2()
    {
        var robots = new List<(int pX, int pY, int vX, int vY)>();
        foreach (var line in Input.Lines)
        {
            var parts = Regex.Matches(line, @"-?\d+");
            var pX = int.Parse(parts[0].Value);
            var pY = int.Parse(parts[1].Value);
            var vX = int.Parse(parts[2].Value);
            var vY = int.Parse(parts[3].Value);

            robots.Add((pX, pY, vX, vY));
        }
        
        for (var second = 1; second < int.MaxValue; second++) 
        {
            var updatedPositions = new List<(int, int)>();
            for (var i = 0; i < robots.Count; i++)
            {
                var (pX, pY, vX, vY) = robots[i];
                
                pX = (pX + vX + width) % width;
                pY = (pY + vY + length) % length;
                
                robots[i] = (pX, pY, vX, vY);
                updatedPositions.Add((pX, pY));
            }
            if (updatedPositions.GroupBy(pos => pos).All(group => group.Count() == 1))
            {
                return second; 
            }
        }
        return -1; 
    }
}