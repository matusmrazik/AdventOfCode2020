using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC_CS.Days
{
    class Day04
    {
        const string INPUT_FILE = "Inputs\\day04.txt";

        private string[] passports;

        public Day04()
        {
            passports = null;
        }

        private void ReadInput()
        {
            if (passports != null) return;

            var lines = File.ReadAllText(INPUT_FILE);
            passports = lines.Split("\n\n");
        }

        public int Solve1()
        {
            ReadInput();

            var solution = 0;
            foreach (var pass in passports)
            {
                var requiredFields = new SortedSet<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
                var tokens = pass.Split(new[] { '\n', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < tokens.Length; i += 2)
                {
                    requiredFields.Remove(tokens[i]);
                }
                if (requiredFields.Count == 0) ++solution;
            }
            return solution;
        }

        private bool ValidateField(string field, string value)
        {
            switch (field)
            {
                case "byr":
                    return Regex.IsMatch(value, @"^(19[2-9][0-9]|200[0-2])$");
                case "iyr":
                    return Regex.IsMatch(value, @"^(201[0-9]|2020)$");
                case "eyr":
                    return Regex.IsMatch(value, @"^(202[0-9]|2030)$");
                case "hgt":
                    return Regex.IsMatch(value, @"^(1[5-8][0-9]cm|19[0-3]cm|59in|6[0-9]in|7[0-6]in)$");
                case "hcl":
                    return Regex.IsMatch(value, @"^(\#[0-9a-f]{6})$");
                case "ecl":
                    return Regex.IsMatch(value, @"^(amb|blu|brn|gry|grn|hzl|oth)$");
                case "pid":
                    return Regex.IsMatch(value, @"^([0-9]{9})$");
                case "cid":
                    return true;
                default:
                    return false;
            }
        }

        public int Solve2()
        {
            ReadInput();

            var solution = 0;
            foreach (var pass in passports)
            {
                var valid = true;
                var requiredFields = new SortedSet<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
                var tokens = pass.Split(new[] { '\n', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                for (var i = 0; i < tokens.Length; i += 2)
                {
                    if (!ValidateField(tokens[i], tokens[i + 1]))
                    {
                        valid = false;
                        break;
                    }
                    requiredFields.Remove(tokens[i]);
                }
                if (valid && requiredFields.Count == 0) ++solution;
            }
            return solution;
        }
    }
}
