using System.IO;
using System.Linq;

namespace AOC_CS.Days
{
    class Day02
    {
        const string INPUT_FILE = "Inputs/day02.txt";

        private string[] inputLines;

        public Day02()
        {
            inputLines = null;
        }

        private void ReadInput()
        {
            if (inputLines != null) return;
            inputLines = File.ReadAllLines(INPUT_FILE);
        }

        public int Solve1()
        {
            ReadInput();

            var solution = 0;
            foreach (var line in inputLines)
            {
                var segments = line.Split(' ', '-');
                int minCount = int.Parse(segments[0]), maxCount = int.Parse(segments[1]);
                char reqChar = segments[2][0];
                var count = segments[3].Count(c => c == reqChar);
                if (count >= minCount && count <= maxCount) ++solution;
            }
            return solution;
        }

        public int Solve2()
        {
            ReadInput();

            var solution = 0;
            foreach (var line in inputLines)
            {
                var segments = line.Split(' ', '-');
                int pos1 = int.Parse(segments[0]), pos2 = int.Parse(segments[1]);
                char reqChar = segments[2][0];
                if (segments[3][pos1 - 1] == reqChar ^ segments[3][pos2 - 1] == reqChar) ++solution;
            }
            return solution;
        }
    }
}
