using System.IO;

namespace AOC_CS.Days
{
    class Day03
    {
        const string INPUT_FILE = "Inputs\\day03.txt";

        private string[] map;

        public Day03()
        {
            map = null;
        }

        private void ReadInput()
        {
            if (map != null) return;
            map = File.ReadAllLines(INPUT_FILE);
        }

        private int CountTrees(Slope slope)
        {
            ReadInput();

            int r = 0, c = 0;
            var solution = 0;
            while (true)
            {
                r += slope.Down;
                if (r >= map.Length) break;
                c = (c + slope.Right) % map[r].Length;
                if (map[r][c] == '#') ++solution;
            }
            return solution;
        }

        public int Solve1()
        {
            var slope = new Slope(3, 1);
            return CountTrees(slope);
        }

        public long Solve2()
        {
            var slopes = new[] { new Slope(1, 1), new Slope(3, 1), new Slope(5, 1), new Slope(7, 1), new Slope(1, 2) };

            var solution = 1L;
            foreach (var slope in slopes)
            {
                solution *= CountTrees(slope);
            }
            return solution;
        }

        private readonly struct Slope
        {
            public Slope(int right, int down)
            { this.Right = right; Down = down; }

            public readonly int Right { get; }
            public readonly int Down { get; }
        }
    }
}
