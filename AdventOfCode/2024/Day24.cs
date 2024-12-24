using AdventOfCodeSupport;

namespace AdventOfCode._2024;

public class Day24 : AdventBase
{
    private class Gate
    {
        public string Input1 { get; set; }
        public string Input2 { get; set; }
        public string Operation { get; set; }
        public string Output { get; set; }

        public Gate(string input1, string operation, string input2, string output)
        {
            Input1 = input1;
            Input2 = input2;
            Operation = operation;
            Output = output;
        }

        public bool TryCompute(Dictionary<string, int> wires, out int result)
        {
            result = 0;
            if (!wires.ContainsKey(Input1) || !wires.ContainsKey(Input2))
                return false;

            switch (Operation)
            {
                case "AND":
                    result = (wires[Input1] & wires[Input2]);
                    break;
                case "OR":
                    result = (wires[Input1] | wires[Input2]);
                    break;
                case "XOR":
                    result = wires[Input1] ^ wires[Input2];
                    break;
                default:
                    throw new Exception($"Unknown operation: {Operation}");
            }

            return true;
        }
    }

    protected override object InternalPart1()
    {
        var parts = Input.Text.Split("\n\n");
        var wires = parts[0].Split("\n")
            .Select(x => x.Split(": "))
            .ToDictionary(x => x[0], y => int.Parse(y[1]));

        var gates = parts[1].Split("\n")
            .Select(line =>
            {
                var parts = line.Split(" -> ");
                var inputParts = parts[0].Split(" ");
                return new Gate(inputParts[0], inputParts[1], inputParts[2], parts[1]);
            })
            .ToList();

        bool changed;
        do
        {
            changed = false;
            foreach (var gate in gates.ToList())
            {
                if (wires.ContainsKey(gate.Output))
                    continue;

                if (!gate.TryCompute(wires, out int result)) continue;
                wires[gate.Output] = result;
                changed = true;
            }
        } while (changed);

        var zWires = wires.Where(w => w.Key.StartsWith('z'))
            .OrderByDescending(w => w.Key)
            .Select(w => w.Value.ToString());

        var binaryString = string.Join("", zWires);
        return Convert.ToInt64(binaryString, 2);
    }

    protected override object InternalPart2()
    {
        var input = Input.Text.Split("\n\n");
        Dictionary<string, string> registers = new();

        foreach (var line in input[0].Split('\n'))
        {
            var parts = line.Split(": ");
            registers[parts[0]] = parts[1];
        }

        foreach (var line in input[1].Split('\n'))
        {
            var parts = line.Split(" -> ");
            registers[parts[1]] = parts[0];
        }

        var swaps = SolvePart2(registers);

        return string.Join(",", swaps.OrderBy(s => s));
    }

    private List<string> SolvePart2(Dictionary<string, string> registers)
    {
        List<string> swaps = [];
        var index = 0;
        var carryReg = "";
        while (registers.ContainsKey($"x{index:00}") && swaps.Count < 8)
        {
            var xReg = $"x{index:00}";
            var yReg = $"y{index:00}";
            var zReg = $"z{index:00}";
            if (index == 0)
            {
                carryReg = FindExpression(registers, xReg, "AND", yReg);
            }
            else
            {
                var XORReg = FindExpression(registers, xReg, "XOR", yReg);
                var ANDReg = FindExpression(registers, xReg, "AND", yReg);
                var carryInReg = FindExpression(registers, XORReg, "XOR", carryReg);
                if (carryInReg == null)
                {
                    swaps.Add(XORReg);
                    swaps.Add(ANDReg);
                    (registers[XORReg], registers[ANDReg]) = (registers[ANDReg], registers[XORReg]);
                    index = 0;
                    continue;
                }

                if (carryInReg != zReg)
                {
                    swaps.Add(carryInReg);
                    swaps.Add(zReg);
                    (registers[carryInReg], registers[zReg]) = (registers[zReg], registers[carryInReg]);
                    index = 0;
                    continue;
                }

                carryInReg = FindExpression(registers, XORReg, "AND", carryReg);
                carryReg = FindExpression(registers, ANDReg, "OR", carryInReg);
            }

            index++;
        }

        return swaps;
    }

    private static string FindExpression(Dictionary<string, string> registers, string op1, string op, string op2)
    {
        var try1 = registers.FirstOrDefault(r => r.Value == $"{op1} {op} {op2}").Key;
        var try2 = registers.FirstOrDefault(r => r.Value == $"{op2} {op} {op1}").Key;
        return try1 ?? try2;
    }
}