using System.IO;
using System.Linq;
using AOC_CS.Utils;
using AOC_CS.Utils.Extensions;

namespace AOC_CS.Days
{
    class Day13
    {
        const string INPUT_FILE = "Inputs/day13.txt";

        private int departTime;
        private string[] allBuses;
        private int[] buses;
        private int[] indices;

        public Day13()
        {
            departTime = 0;
            allBuses = null;
            buses = null;
        }

        private void ReadInput()
        {
            if (indices != null) return;
            var lines = File.ReadAllLines(INPUT_FILE);
            departTime = int.Parse(lines[0]);
            allBuses = lines[1].Split(',');
            buses = allBuses.Where(x => x != "x").Select(int.Parse).ToArray();
            indices = allBuses.Select((x, i) => x == "x" ? -1 : i).Where(x => x >= 0).ToArray();
        }

        public int Solve1()
        {
            ReadInput();
            var mods = buses.Select(x => x - departTime % x).ToArray();

            var pos = mods.ArgMin();
            return mods[pos] * buses[pos];
        }

        public long Solve2()
        {
            ReadInput();
            var mods = buses.Select((x, i) =>
            {
                var tmp = (x - indices[i]) % x;
                return tmp < 0 ? tmp + x : tmp;
            }).ToArray();

            long l = 1, cur = 1;
            for (int i = 0; i < buses.Length; ++i)
            {
                while (cur % buses[i] != mods[i])
                    cur += l;
                l = Numbers.Lcm(l, buses[i]);
            }
            return cur;
        }
    }
}
