using System.Text.RegularExpressions;
using AdventOfCodeSupport;

namespace AdventOfCode._2020;

public class Day4 : AdventBase
{
    protected override object InternalPart1()
    {
        var passports = Input.Text.Split("\n\n");
        var valid = 0;

        foreach (var passport in passports)
        {
            if (passport.Contains("byr") && passport.Contains("iyr") && passport.Contains("eyr") &&
                passport.Contains("hgt") && passport.Contains("hcl") && passport.Contains("ecl") &&
                passport.Contains("pid"))
                valid++;
        }

        return valid;
    }

    protected override object InternalPart2()
    {
        var passports = Input.Text.Split("\n\n");
        var valid = 0;

        foreach (var passport in passports)
        {
            if (!passport.Contains("byr") || !passport.Contains("iyr") || !passport.Contains("eyr") ||
                !passport.Contains("hgt") || !passport.Contains("hcl") || !passport.Contains("ecl") ||
                !passport.Contains("pid")) continue;
            
            var byr = Regex.Match(passport, @"\bbyr:(\d{4})\b");
            var iyr = Regex.Match(passport, @"\biyr:(\d{4})\b");
            var eyr = Regex.Match(passport, @"\beyr:(\d{4})\b");
            var pid = Regex.IsMatch(passport, @"\bpid:\d{9}\b");
            var ecl = Regex.IsMatch(passport, @"\becl:(amb|blu|brn|gry|grn|hzl|oth)\b");
            var hcl = Regex.IsMatch(passport, @"\bhcl:#[0-9a-f]{6}\b");
            var hgt = Regex.Match(passport, @"\bhgt:(\d+)(cm|in)\b");

            if (byr.Success && iyr.Success && eyr.Success && pid && ecl && hcl && hgt.Success)
            {
                int byrValue = int.Parse(byr.Groups[1].Value);
                int iyrValue = int.Parse(iyr.Groups[1].Value);
                int eyrValue = int.Parse(eyr.Groups[1].Value);
                int hgtValue = int.Parse(hgt.Groups[1].Value);

                if (byrValue >= 1920 && byrValue <= 2002 &&
                    iyrValue >= 2010 && iyrValue <= 2020 &&
                    eyrValue >= 2020 && eyrValue <= 2030 &&
                    ((hgt.Groups[2].Value == "cm" && hgtValue >= 150 && hgtValue <= 193) ||
                     (hgt.Groups[2].Value == "in" && hgtValue >= 59 && hgtValue <= 76)))
                {
                    valid++;
                }
            }
        }

        return valid;
    }

}
