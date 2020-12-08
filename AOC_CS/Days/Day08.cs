using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC_CS.Days
{
    class Day08
    {
        const string INPUT_FILE = "Inputs/day08.txt";

        private Tuple<string, int>[] program;

        public Day08()
        {
            program = null;
        }

        private void ReadInput()
        {
            if (program != null) return;
            var cmds = File.ReadAllLines(INPUT_FILE);
            var prg = cmds.Aggregate(
                new List<Tuple<string, int>>(),
                (acc, cmd) =>
                {
                    var tmp = cmd.Split(' ');
                    acc.Add(new Tuple<string, int>(tmp[0], int.Parse(tmp[1])));
                    return acc;
                }
            );
            program = prg.ToArray();
        }

        private bool RunProgram(int changeLine, out int accumulator)
        {
            accumulator = 0;
            var line = 0;
            var processed = new bool[program.Length];
            while (true)
            {
                if (line == program.Length) return true;
                if (processed[line]) return false;
                processed[line] = true;
                var (op, param) = program[line];
                if (line == changeLine)
                {
                    if (op == "jmp") op = "nop";
                    else if (op == "nop") op = "jmp";
                }
                switch (op)
                {
                    case "nop":
                        ++line;
                        break;
                    case "acc":
                        accumulator += param;
                        ++line;
                        break;
                    case "jmp":
                        line += param;
                        break;
                }
            }
        }

        public int Solve1()
        {
            ReadInput();
            RunProgram(-1, out var solution);
            return solution;
        }

        public int Solve2()
        {
            ReadInput();
            int solution = 0;
            for (var line = 0; line < program.Length; ++line)
            {
                if (RunProgram(line, out solution)) return solution;
            }
            return 0;
        }
    }
}
