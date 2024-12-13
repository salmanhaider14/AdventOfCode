using AdventOfCodeSupport;
namespace AdventOfCode._2024
{
    public class Day11 : AdventBase
    {
        protected override object InternalPart1()
        {
            var stones = Input.Text.Split(" ").ToList();
            List<string> current = [];
            List<string> prev = stones;
            for (int i = 0; i < 25; i++)
            {
                foreach (var stone in prev)
                {
                    if (stone == "0")
                        current.Add("1");
                    else if (stone.Length % 2 == 0)
                    {
                        var midIndex = stone.Length / 2;
                        var firstPart = stone.Substring(0, midIndex);
                        current.Add(long.Parse(firstPart).ToString());
                        var secondPart = stone.Substring(midIndex);
                        current.Add(long.Parse(secondPart).ToString());
                    }
                    else
                    {
                        long x = long.Parse(stone) * 2024;
                        current.Add(x.ToString());
                    }
                }

                prev = [..current];
                current.Clear();
            }

            return prev.Count;
        }
        protected override object InternalPart2()
        {
            var initialStones = Input.Text.Split(" ").ToList();
            return OptimizedStoneEvolution(initialStones, 75);
        }
        private static long OptimizedStoneEvolution(List<string> initialStones, int blinkCount)
        {
            var stoneTypeCounts = initialStones
                .GroupBy(x => x)
                .ToDictionary(g => g.Key, g => (long)g.Count());

            var transformationCache = new Dictionary<string, List<string>>();

            for (var blink = 0; blink < blinkCount; blink++)
            {
                var nextStoneTypeCounts = new Dictionary<string, long>();

                foreach (var (stoneType, count) in stoneTypeCounts)
                {
                    var transformedStones = transformationCache.TryGetValue(stoneType, out var cached)
                        ? cached
                        : TransformStone(stoneType);

                    transformationCache[stoneType] = transformedStones;

                    foreach (var newStoneType in transformedStones)
                    {
                        nextStoneTypeCounts[newStoneType] = nextStoneTypeCounts.GetValueOrDefault(newStoneType) + count;
                    }
                }
                stoneTypeCounts = nextStoneTypeCounts;
                Console.WriteLine($"Blink {blink + 1}: {stoneTypeCounts.Sum(x => x.Value)} stones");
            }

            return stoneTypeCounts.Sum(x => x.Value);
        }

        private static List<string> TransformStone(string stone)
        {
            return stone switch
            {
                "0" => ["1"],
                var s when s.Length % 2 == 0 =>
                [
                    long.Parse(s[..(s.Length / 2)]).ToString(),
                    long.Parse(s[(s.Length / 2)..]).ToString()
                ],
                _ => [(long.Parse(stone) * 2024).ToString()]
            };
        }
    }
}
