#include <iostream>
#include <fstream>
#include <string>
#include <vector>

namespace AOC_CPP::Day08
{
    constexpr auto INPUT_FILE = "../Inputs/day08.txt";

    using ProgramType = std::vector<std::pair<std::string, int>>;

    ProgramType program;

    auto run_program(int changeLine)
    {
        const int nCommands = program.size();
        int acc = 0, line = 0;
        std::vector<bool> processed(program.size());
        while (true)
        {
            if (line == nCommands)
                return std::make_pair(true, acc);
            if (processed[line])
                return std::make_pair(false, acc);
            processed[line] = true;
            auto [op, param] = program[line];
            if (line == changeLine)
            {
                if (op == "nop")
                    op = "jmp";
                else if (op == "jmp")
                    op = "nop";
            }
            if (op == "nop")
            {
                ++line;
            }
            else if (op == "acc")
            {
                acc += param;
                ++line;
            }
            else if (op == "jmp")
            {
                line += param;
            }
        }
    }

    auto run_program()
    {
        return run_program(-1);
    }

    void read_input()
    {
        program.clear();
        std::string op;
        int param;
        std::ifstream infile(INPUT_FILE);
        while (infile >> op >> param)
        {
            program.emplace_back(op, param);
        }
        infile.close();
    }

    int solve1()
    {
        const auto [_, acc] = run_program();
        return acc;
    }

    int solve2()
    {
        const int nCommands = program.size();
        for (auto line = 0; line < nCommands; ++line)
        {
            const auto [ok, acc] = run_program(line);
            if (ok)
                return acc;
        }
        return 0;
    }

} // namespace AOC_CPP::Day08

int main()
{
    using namespace AOC_CPP;
    Day08::read_input();
    std::cout << Day08::solve1() << '\n';
    std::cout << Day08::solve2() << '\n';

    return 0;
}
