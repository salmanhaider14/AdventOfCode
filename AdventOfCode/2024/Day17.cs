using System.Text.RegularExpressions;
using AdventOfCodeSupport;
namespace AdventOfCode._2024;

public class Day17:AdventBase
{
    private const string TestInput = "Register A: 2024\nRegister B: 0\nRegister C: 0\n\nProgram: 0,3,5,4,3,0";
    protected override object InternalPart1()
    {
        var parts = Input.Text.Split("\n\n");
        var programs = parts[1].Split(": ")[1].Split(",").Select(x => byte.Parse(x)).ToArray();
        var regex = Regex.Match(parts[0], @"\d+");
        var A = int.Parse(regex.Value);
        return RunProgramAndCollectOutput(A, programs);
    }
    protected override object InternalPart2()
    {
        var parts = Input.Text.Split("\n\n");
        var programs = parts[1].Split(": ")[1].Split(",").Select(x => byte.Parse(x)).ToArray();
        long A = 1;
        var goal = string.Join(",", programs);
        while (true)
        {
            Console.WriteLine("CurrentA in main: {0}",A);
            var output = RunProgramAndCollectOutput(A, programs);
            if (output.Equals(goal)) break;
            if (goal.EndsWith(output)) A *= 8;
            else A++;
        }
        return A;
    }
    private string RunProgramAndCollectOutput(long initialA, byte[] programs)
    {
    List<long> output = [];
    var A = initialA;
    long B = 0;
    long C = 0;
    var IP = 0;

    while (IP < programs.Length)
    {
        var current = programs[IP];
        var operand = programs[IP + 1];
        switch (current)
        {
            case 0:
                A = A / (int)Math.Pow(2, GetComboOperand(operand));
                IP += 2;
                break;
            case 1:
                B = B ^ operand;
                IP += 2;
                break;
            case 2:
                B = GetComboOperand(operand) % 8;
                IP += 2;
                break;
            case 3:
                if (A is 0) IP += 2;
                else IP = operand;
                break;
            case 4:
                B = B ^ C;
                IP += 2;
                break;
            case 5:
                output.Add(GetComboOperand(operand) % 8);
                IP += 2;
                break;
            case 6:
                B = A / (int)Math.Pow(2, GetComboOperand(operand));
                IP += 2;
                break;
            case 7:
                C = A / (int)Math.Pow(2, GetComboOperand(operand));
                IP += 2;
                break;
        }
    }

    return string.Join(",", output);
    long GetComboOperand(int operand)
    {
        return operand switch
        {
            0 or 1 or 2 or 3 => operand,
            4 => A,
            5 => B,
            6 => C,
            _ => -1
        };
    }
    }
}