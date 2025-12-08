using AdventOfCodeSupport;
namespace AdventOfCode._2020;

public class Day13: AdventBase
{
    protected override object InternalPart1()
    {
        var parts = Input.Text.Trim().Split("\n");
        var minimumDepartedTime = int.Parse(parts[0]);
        var busses = parts[1].Split(",").Where(x => x != "x").Select(int.Parse);
        var map = new Dictionary<int, long>();

        foreach (var bus in busses)
        {
            var time = 0;
            while (time <= minimumDepartedTime) time += bus;
            map[bus] = time - minimumDepartedTime;
        }
        var bestBus = map.MinBy(x => x.Value);
        return bestBus.Key * bestBus.Value;
    }

    protected override object InternalPart2()
    {
        var parts = Input.Text.Trim().Split("\n");
        var buses = parts[1]
            .Split(",")
            .Select((id, index) => (id, index))
            .Where(x => x.id != "x")
            .Select(x => (busId: int.Parse(x.id), offset: x.index))
            .ToArray();

        long timestamp = 0;
        long step = buses[0].busId;

        foreach (var (busId, offset) in buses.Skip(1))
        {
            while ((timestamp + offset) % busId != 0)
            {
                timestamp += step;
            }
            step *= busId; 
        }
        return timestamp;
    }

}