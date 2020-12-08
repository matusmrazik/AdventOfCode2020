using System;
using System.IO;

namespace AOC_CS.Days
{
    class Day05
    {
        const string INPUT_FILE = "Inputs/day05.txt";

        private string[] passes;

        public Day05()
        {
            passes = null;
        }

        private void ReadInput()
        {
            if (passes != null) return;
            passes = File.ReadAllLines(INPUT_FILE);
        }

        private int GetSeatId(string pass)
        {
            var rowString = pass.Substring(0, 7).Replace('B', '1').Replace('F', '0');
            var colString = pass.Substring(7, 3).Replace('R', '1').Replace('L', '0');
            var row = Convert.ToInt32(rowString, 2);
            var col = Convert.ToInt32(colString, 2);
            return row * 8 + col;
        }

        public int Solve1()
        {
            ReadInput();

            int solution = 0;
            foreach (var pass in passes)
            {
                var seatId = GetSeatId(pass);
                if (seatId > solution) solution = seatId;
            }
            return solution;
        }

        public int Solve2()
        {
            ReadInput();

            var seatsTaken = new bool[128 * 8];
            foreach (var pass in passes)
            {
                var seatId = GetSeatId(pass);
                seatsTaken[seatId] = true;
            }

            return Array.IndexOf(seatsTaken, false, Array.IndexOf(seatsTaken, true));
        }
    }
}
