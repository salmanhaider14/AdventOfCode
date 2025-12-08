using AdventOfCodeSupport;
namespace AdventOfCode._2020;

public class Day12: AdventBase
{
    private readonly char[] _compass = ['N', 'E', 'S', 'W']; // Compass directions in clockwise order
    private readonly Dictionary<char, int[]> _directions = new()
    {
        ['N'] = [-1, 0], // North
        ['S'] = [1, 0],  // South
        ['E'] = [0, 1],  // East
        ['W'] = [0, -1] // West
    };

    private const string test = "F10\nN3\nF7\nR90\nF11";
    protected override object InternalPart1()
    {
        var instructions = Input.Lines.Select(x => (x[0], int.Parse(x.Substring(1)))).ToArray();
        var row = 0;
        var col = 0;
        var currentDirectionIndex = 1;

        foreach (var (action, value) in instructions)
        {
            switch (action)
            {
                case 'N':
                case 'S':
                case 'E':
                case 'W':
                    row += _directions[action][0] * value;
                    col += _directions[action][1] * value;
                    break;
                case 'L':
                    currentDirectionIndex = (currentDirectionIndex - value / 90 + 4) % 4; // Rotate left
                    break;
                case 'R':
                    currentDirectionIndex = (currentDirectionIndex + value / 90) % 4; // Rotate right
                    break;
                case 'F':
                    var forwardDirection = _compass[currentDirectionIndex];
                    row += _directions[forwardDirection][0] * value;
                    col += _directions[forwardDirection][1] * value;
                    break;
            }
        }
        
        return Math.Abs(row) + Math.Abs(col);
    }

    protected override object InternalPart2()
{
    var instructions = Input.Lines.Select(x => (x[0], int.Parse(x.Substring(1)))).ToArray();

    // Initial positions: Ship at (0, 0), waypoint 10 east and 1 north relative to the ship
    var shipRow = 0;
    var shipCol = 0;
    var waypointRow = 1;  // Positive north (y-axis)
    var waypointCol = 10; // Positive east (x-axis)

    foreach (var (action, value) in instructions)
    {
        switch (action)
        {
            case 'N':
                waypointRow += value; // Move north
                break;

            case 'S':
                waypointRow -= value; // Move south
                break;

            case 'E':
                waypointCol += value; // Move east
                break;

            case 'W':
                waypointCol -= value; // Move west
                break;

            case 'L':
                RotateWaypoint(ref waypointRow, ref waypointCol, -value); // Rotate counter-clockwise
                break;

            case 'R':
                RotateWaypoint(ref waypointRow, ref waypointCol, value); // Rotate clockwise
                break;

            case 'F':
                // Move the ship to the waypoint a number of times equal to the given value
                shipRow += waypointRow * value;
                shipCol += waypointCol * value;
                break;

            default:
                throw new InvalidOperationException($"Unknown action: {action}");
        }

        // Debugging: Log current positions
        Console.WriteLine($"Action: {action}, Value: {value}");
        Console.WriteLine($"Ship Position: ({shipRow}, {shipCol}), Waypoint: ({waypointRow}, {waypointCol})");
    }

    // Calculate Manhattan distance from the starting position
    var manhattanDistance = Math.Abs(shipRow) + Math.Abs(shipCol);

    Console.WriteLine($"Final Ship Position: ({shipRow}, {shipCol})");
    Console.WriteLine($"Manhattan Distance: {manhattanDistance}");

    return manhattanDistance;
}

// Helper method for waypoint rotation
private void RotateWaypoint(ref int row, ref int col, int degrees)
{
    // Normalize degrees to the range [0, 360)
    degrees = (degrees + 360) % 360;

    // Apply rotations
    switch (degrees)
    {
        case 90:  // 90° clockwise
            (row, col) = (-col, row);
            break;

        case 180: // 180°
            (row, col) = (-row, -col);
            break;

        case 270: // 90° counter-clockwise
            (row, col) = (col, -row);
            break;

        case 0: // No rotation
            break;

        default:
            throw new InvalidOperationException($"Unsupported rotation: {degrees} degrees");
    }

    // Debugging: Log after rotation
    Console.WriteLine($"Waypoint after {degrees}° rotation: ({row}, {col})");
}
}