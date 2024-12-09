using AdventOfCodeSupport;
namespace AdventOfCode._2024;

public class Day09:AdventBase
{ 
    private struct Block
    { 
        public int? Id { get; set; } 
        public bool IsFile => Id.HasValue;
    }
    protected override object InternalPart1()
    {
        long checksum = 0;
        var blocks = new List<Block>();
        var isDigit = true;
        var id = 0;
        
        foreach (var digit in Input.Text)
        {
            var currentDigit = digit - '0';
            if (currentDigit is 0)
            {
                isDigit = !isDigit;
                continue;
            }
            if (isDigit)
            {
                for (int i = 0; i < currentDigit; i++)
                {
                    blocks.Add(new Block { Id = id });
                }
                id++;
                isDigit = false;
            }
            else
            {
                for (int i = 0; i < currentDigit; i++)
                {
                    blocks.Add(new Block { Id = null }); 
                }
                isDigit = true;
            }
        }
        
        int j = 0, k = blocks.Count - 1;
        while (j < k)
        {
            while (blocks[j].IsFile) j++;
            while (!blocks[k].IsFile) k--;
            
            if (j < k)
            {
                (blocks[j], blocks[k]) = (blocks[k], blocks[j]);
                j++;
                k--;
            }
        }
        
        for (var i = 0; i < blocks.Count; i++)
        {
            if (!blocks[i].IsFile) break;
            checksum += i * blocks[i].Id!.Value;
        }
        return checksum;
    }
    protected override object InternalPart2()
{
    long checksum = 0;
    var blocks = new List<Block>();
    var isDigit = true;
    var id = 0;
    
    foreach (var digit in Input.Text)
    {
        var currentDigit = digit - '0';
        if (currentDigit is 0)
        {
            isDigit = !isDigit;
            continue;
        }
        if (isDigit)
        {
            for (int i = 0; i < currentDigit; i++)
            {
                blocks.Add(new Block { Id = id });
            }
            id++;
            isDigit = false;
        }
        else
        {
            for (int i = 0; i < currentDigit; i++)
            {
                blocks.Add(new Block { Id = null });
            }
            isDigit = true;
        }
    }
    
    var largestId = blocks.Max(b => b.Id ?? -1);
    
    for (int currentFileId = largestId; currentFileId >= 0; currentFileId--)
    {
        var fileStart = blocks.FindIndex(b => b.Id == currentFileId);
        var fileEnd = blocks.FindLastIndex(b => b.Id == currentFileId);
        var fileSize = fileEnd - fileStart + 1;
        int bestStart = -1;
        
        for (var i = 0; i < fileStart; i++)
        {
            if (!blocks[i].IsFile)
            {
                var spanStart = i;
                while (i < fileStart && !blocks[i].IsFile)
                    i++;
                var spanEnd = i - 1;
                var spanSize = spanEnd - spanStart + 1;
                if (spanSize >= fileSize)
                {
                    bestStart = spanStart;
                    break; 
                }
            }
        }

        if (bestStart == -1) continue;
        for (int i = 0; i < fileSize; i++)
        { 
            (blocks[bestStart + i], blocks[fileStart + i]) = (blocks[fileStart + i], blocks[bestStart + i]);
        }
    }
    
    for (int i = 0; i < blocks.Count; i++)
    {
        if (blocks[i].IsFile)
            checksum += i * blocks[i].Id!.Value;
    }
    return checksum;
    }
}
