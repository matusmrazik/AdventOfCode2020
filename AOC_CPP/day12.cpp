#include <fstream>
#include <iostream>
#include <stdexcept>
#include <string>
#include <vector>

namespace AOC_CPP::Day12
{
    constexpr auto INPUT_FILE = "../Inputs/day12.txt";

    std::vector<std::pair<char, int>> instructions;

    void read_input()
    {
        using namespace std::string_literals;
        instructions.clear();
        char c;
        int num;
        std::ifstream infile(INPUT_FILE);
        if (!infile) throw std::runtime_error("File \""s + INPUT_FILE + "\" not found");
        while (infile >> c >> num)
            instructions.emplace_back(c, num);
        infile.close();
    }

    int solve1()
    {
        struct
        {
            int posX{0};
            int posY{0};
            int rot{90};
            std::pair<int, int> posD{1, 0};

            void move(int dist)
            {
                posX += dist * posD.first;
                posY += dist * posD.second;
            }

            void add_rotation(int deg)
            {
                rot = (rot + deg) % 360;
                switch (rot)
                {
                    case 0:
                        posD = {0, 1};
                        break;
                    case 90:
                    case -270:
                        posD = {1, 0};
                        break;
                    case 180:
                    case -180:
                        posD = {0, -1};
                        break;
                    case 270:
                    case -90:
                        posD = {-1, 0};
                        break;
                    default:
                        throw deg;
                }
            }

            int dist() { return std::abs(posX) + std::abs(posY); }
        } data;

        for (const auto [action, param] : instructions)
        {
            if (action == 'N')
                data.posY += param;
            else if (action == 'S')
                data.posY -= param;
            else if (action == 'E')
                data.posX += param;
            else if (action == 'W')
                data.posX -= param;
            else if (action == 'L')
                data.add_rotation(-param);
            else if (action == 'R')
                data.add_rotation(param);
            else if (action == 'F')
                data.move(param);
        }

        return data.dist();
    }

    int solve2()
    {
        struct
        {
            int wPosX{10}, wPosY{1}, posX{0}, posY{0};

            void move(int dist)
            {
                const auto dX = dist * (wPosX - posX);
                const auto dY = dist * (wPosY - posY);
                posX += dX;
                posY += dY;
                wPosX += dX;
                wPosY += dY;
            }

            void rotate_waypoint(int deg)
            {
                const auto dX = wPosX - posX;
                const auto dY = wPosY - posY;
                switch (deg)
                {
                    case 90:
                    case -270:
                        // X = y, Y = -x
                        wPosX = wPosX - dX + dY;
                        wPosY = wPosY - dY - dX;
                        break;
                    case 180:
                    case -180:
                        // X = -x, Y = -y
                        wPosX -= 2 * dX;
                        wPosY -= 2 * dY;
                        break;
                    case 270:
                    case -90:
                        // X = -y, Y = x
                        wPosX = wPosX - dX - dY;
                        wPosY = wPosY - dY + dX;
                        break;
                    default:
                        throw deg;
                }
            }

            int dist() { return std::abs(posX) + std::abs(posY); }
        } data;

        for (const auto [action, param] : instructions)
        {
            if (action == 'N')
                data.wPosY += param;
            else if (action == 'S')
                data.wPosY -= param;
            else if (action == 'E')
                data.wPosX += param;
            else if (action == 'W')
                data.wPosX -= param;
            else if (action == 'L')
                data.rotate_waypoint(-param);
            else if (action == 'R')
                data.rotate_waypoint(param);
            else if (action == 'F')
                data.move(param);
        }

        return data.dist();
    }
} // namespace AOC_CPP::Day12

int main()
{
    using namespace AOC_CPP;
    Day12::read_input();
    std::cout << Day12::solve1() << '\n';
    std::cout << Day12::solve2() << '\n';

    return 0;
}
