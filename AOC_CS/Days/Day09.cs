using System;
using System.Collections.Generic;
using System.IO;

namespace AOC_CS.Days
{
    class Day09
    {
        const string INPUT_FILE = "Inputs/day09.txt";

        const int PREAMBLE = 25;

        private long[] inputs;
        private HashSet<long>[] sums;

        public Day09()
        {
            inputs = null;
            sums = null;
        }

        private void ReadInput()
        {
            if (inputs != null && sums != null) return;

            var lines = File.ReadAllLines(INPUT_FILE);
            inputs = Array.ConvertAll(lines, long.Parse);

            sums = new HashSet<long>[inputs.Length];
            for (var i = 0; i < inputs.Length; ++i)
                sums[i] = new HashSet<long>();
            for (var i = 0; i < inputs.Length; ++i)
            {
                for (var j = 1; j < PREAMBLE && i + j < inputs.Length; ++j)
                {
                    var sum = inputs[i] + inputs[i + j];
                    for (var k = j + 1; k <= PREAMBLE && i + k < inputs.Length; ++k)
                        sums[i + k].Add(sum);
                }
            }
        }

        public long Solve1()
        {
            ReadInput();
            for (var i = PREAMBLE; i < inputs.Length; ++i)
            {
                if (!sums[i].Contains(inputs[i]))
                    return inputs[i];
            }
            return inputs.Length;
        }

        public long Solve2()
        {
            ReadInput();
            var num = Solve1();
            for (var i = 0; i < inputs.Length; ++i)
            {
                long sum = inputs[i], min = sum, max = sum;
                for (var j = i + 1; j < inputs.Length; ++j)
                {
                    sum += inputs[j];
                    if (sum > num) break;
                    if (inputs[j] < min) min = inputs[j];
                    if (inputs[j] > max) max = inputs[j];
                    if (sum == num) return min + max;
                }
            }
            return 0;
        }
    }
}
