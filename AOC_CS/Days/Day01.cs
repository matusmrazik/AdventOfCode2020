using System.Collections.Generic;
using System.IO;

namespace AOC_CS.Days
{
    class Day01
    {
        const string INPUT_FILE = "Inputs\\day01.txt";

        private List<int> inputNumbers;

        public Day01()
        {
            inputNumbers = null;
        }

        private void ReadInput()
        {
            if (inputNumbers != null) return;

            var lines = File.ReadAllLines(INPUT_FILE);
            inputNumbers = new List<int>();
            foreach (var line in lines)
            {
                inputNumbers.Add(int.Parse(line));
            }
        }

        public int Solve1()
        {
            const int targetSum = 2020;

            ReadInput();

            for (var i = 0; i < inputNumbers.Count; ++i)
            {
                for (var j = i + 1; j < inputNumbers.Count; ++j)
                {
                    if (inputNumbers[i] + inputNumbers[j] != targetSum) continue;
                    return inputNumbers[i] * inputNumbers[j];
                }
            }

            return 0;
        }

        public int Solve2()
        {
            const int targetSum = 2020;

            ReadInput();

            for (var i = 0; i < inputNumbers.Count; ++i)
            {
                for (var j = i + 1; j < inputNumbers.Count; ++j)
                {
                    var sumOfTwo = inputNumbers[i] + inputNumbers[j];
                    if (sumOfTwo > targetSum) continue;
                    for (var k = j + 1; k < inputNumbers.Count; ++k)
                    {
                        if (sumOfTwo + inputNumbers[k] != targetSum) continue;
                        return inputNumbers[i] * inputNumbers[j] * inputNumbers[k];
                    }
                }
            }

            return 0;
        }
    }
}
