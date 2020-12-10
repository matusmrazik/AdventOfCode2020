#include <algorithm>
#include <array>
#include <fstream>
#include <iostream>
#include <stdexcept>
#include <vector>

namespace AOC_CPP::Day10
{
    constexpr auto INPUT_FILE = "../Inputs/day10.txt";

    std::vector<int> inputs;
    std::vector<int64_t> cache;

    void read_input()
    {
        using namespace std::string_literals;
        inputs.clear();
        int n;
        std::ifstream infile(INPUT_FILE);
        if (!infile) throw std::runtime_error("File \""s + INPUT_FILE + "\" not found");
        while (infile >> n)
            inputs.push_back(n);
        infile.close();
        std::sort(inputs.begin(), inputs.end());
    }

    int solve1()
    {
        std::array counts{0, 0, 0, 1};
        counts.at(inputs.front())++;
        for (size_t i = 1; i < inputs.size(); ++i)
        {
            const auto diff = inputs[i] - inputs[i - 1];
            counts.at(diff)++;
        }
        return counts[1] * counts[3];
    }

    int64_t _solve_recur(size_t pos)
    {
        if (cache[pos] != -1) return cache[pos];
        int64_t result = 0;
        for (size_t i = pos + 1; i < inputs.size() && (inputs[i] - inputs[pos]) <= 3; ++i)
            result += _solve_recur(i);
        return cache[pos] = result;
    }

    int64_t solve2()
    {
        cache.assign(inputs.size(), -1);
        cache.back() = 1;
        int64_t solution = 0;
        for (size_t i = 0; i < inputs.size() && inputs[i] <= 3; ++i)
            solution += _solve_recur(i);
        return solution;
    }
} // namespace AOC_CPP::Day10

int main()
{
    using namespace AOC_CPP;
    Day10::read_input();
    std::cout << Day10::solve1() << '\n';
    std::cout << Day10::solve2() << '\n';

    return 0;
}
