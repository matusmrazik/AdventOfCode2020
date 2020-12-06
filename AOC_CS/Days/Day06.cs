using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC_CS.Days
{
    class Day06
    {
        const string INPUT_FILE = "Inputs\\day06.txt";

        private string[] groups;

        public Day06()
        {
            groups = null;
        }

        private void ReadInput()
        {
            if (groups != null) return;
            groups = File.ReadAllText(INPUT_FILE).Split("\n\n");
            groups[groups.Length - 1] = groups.Last().Trim();
        }

        private int CountCommonAnswers(string group)
        {
            return group.Split('\n').Aggregate(
                new HashSet<char>("abcdefghijklmnopqrstuvwxyz"),
                (acc, val) => acc.Intersect(new HashSet<char>(val)).ToHashSet()
            ).Count;
        }

        public int Solve1()
        {
            ReadInput();
            return groups.Aggregate(0, (acc, val) => acc + new HashSet<char>(val.Replace("\n", "")).Count);
        }

        public int Solve2()
        {
            ReadInput();
            return groups.Aggregate(0, (acc, val) => acc + CountCommonAnswers(val));
        }
    }
}
