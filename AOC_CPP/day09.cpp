#include <fstream>
#include <iostream>
#include <set>
#include <stdexcept>
#include <vector>

namespace AOC_CPP::Day09
{
    constexpr auto INPUT_FILE = "../Inputs/day09.txt";

    using IntType = int64_t;

    constexpr size_t PREAMBLE = 25;
    std::vector<IntType> inputs;

    std::vector<std::set<IntType>> sums;

    void read_input()
    {
        using namespace std::string_literals;
        inputs.clear();
        IntType n;
        std::ifstream infile(INPUT_FILE);
        if (!infile) throw std::runtime_error("File \""s + INPUT_FILE + "\" not found");
        while (infile >> n)
            inputs.push_back(n);
        infile.close();
        sums.assign(inputs.size(), std::set<IntType>{});
        for (size_t i = 0; i < inputs.size(); ++i)
        {
            for (size_t j = 1; j < PREAMBLE && i + j < inputs.size(); ++j)
            {
                const auto tmp = inputs[i] + inputs[i + j];
                for (size_t k = j + 1; k <= PREAMBLE && i + k < inputs.size(); ++k)
                    sums[i + k].insert(tmp);
            }
        }
    }

    IntType solve1()
    {
        for (size_t i = PREAMBLE; i < inputs.size(); ++i)
        {
            if (sums[i].find(inputs[i]) == sums[i].end())
                return inputs[i];
        }
        return 0;
    }

    IntType solve2()
    {
        const auto num = solve1();
        for (size_t i = 0; i < inputs.size(); ++i)
        {
            auto sum = inputs[i], min = sum, max = sum;
            for (size_t j = i + 1; j < inputs.size(); ++j)
            {
                const auto tmp = inputs[j];
                sum += tmp;
                if (sum > num) break;
                if (tmp > max) max = tmp;
                if (tmp < min) min = tmp;
                if (sum == num) return min + max;
            }
        }
        return 0;
    }
} // namespace AOC_CPP::Day09

int main()
{
    using namespace AOC_CPP;
    Day09::read_input();
    std::cout << Day09::solve1() << '\n';
    std::cout << Day09::solve2() << '\n';

    return 0;
}
