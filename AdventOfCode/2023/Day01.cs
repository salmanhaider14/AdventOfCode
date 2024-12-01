using System.Text;
using AdventOfCodeSupport;
using Microsoft.Extensions.Primitives;

namespace AdventOfCode._2023;

public class Day01 : AdventBase
{
    protected override object InternalPart1()
    {
        var sum = 0;
        foreach (var input in Input.Lines)
        {
            ExtractValue(input,ref sum);
        }
        return sum;
    }
    private void ExtractValue(string value, ref int sum)
    {
        var i = 0;
        var j = value.Length - 1;
        var ans = new StringBuilder();
        while (i < value.Length)
        {
            if (char.IsDigit(value[i]))
            {
                ans.Append(value[i]);
                break;
            }
            i++;
        }
        while ( j>=0)
        {
            if (char.IsDigit(value[j]))
            {
                ans.Append(value[j]);
                break;
            }
            j--;
        }

        if (ans.Length == 1)
            ans.Append(ans[0]);

        sum += int.Parse(ans.ToString());
    }
    protected override object InternalPart2()
    {
        Dictionary<string,int> map =new()
        {
            {"one",1},{"two",2},{"three",3},{"four",4},{"five",5},
            {"six",6},{"seven",7},{"eight",8},{"nine",9}
        };
        var sum = 0;
        foreach (var input in Input.Lines)
        {
            ExtractValue2(input,map,ref sum);
        }

        return sum;
    }
    private void ExtractValue2(string value, Dictionary<string,int> map,ref int sum)
    {
        var ans = new StringBuilder();
        var exit = false;

        for (int i = 0; i < value.Length && !exit; i++)
        {
            if (char.IsDigit(value[i]))
            {
                ans.Append(value[i]);
                break;
            }
            for (int j = 0; j <=i; j++)
            {
                var substring = value.Substring(j, i - j + 1);
                if (map.ContainsKey(substring))
                {
                    ans.Append(map[substring]);
                    exit = true;
                    break;
                }
            }
        }

        exit = false;
        for (int i = value.Length - 1; i >=0  && !exit; i--)
        {
            if (char.IsDigit(value[i]))
            {
                ans.Append(value[i]);
                break;
            }
            for (int j = i; j >=0; j--)
            {
                var substring = value.Substring(j, i - j + 1);
                if (map.ContainsKey(substring))
                {
                    ans.Append(map[substring]);
                    exit = true;
                    break;
                }
            }
        }

        if (ans.Length == 1)
            ans.Append(ans[0]);

        sum += int.Parse(ans.ToString());
    }
}