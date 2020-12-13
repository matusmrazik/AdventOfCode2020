#include <algorithm>
#include <array>
#include <fstream>
#include <iostream>
#include <limits>
#include <stdexcept>
#include <string>
#include <vector>

namespace AOC_CPP::Day11
{
    constexpr auto INPUT_FILE = "../Inputs/day11.txt";

    enum class Dir
    {
        UL,
        U,
        UR,
        L,
        R,
        DL,
        D,
        DR
    };

    struct Seat
    {
        Seat() : mValue('.'), mAdjFree{}, mAdjOccup{}, mDirsDist{}, mDirsOccup{false}
        {
            std::fill(mDirsDist.begin(), mDirsDist.end(), std::numeric_limits<int>::max());
        }

        Seat(const Seat &other) = default;

        Seat &operator=(char value)
        {
            mValue = value;
            return *this;
        }

        int count_adj_occupied() const
        {
            return mAdjOccup;
        }

        int count_adj_free() const
        {
            return mAdjFree;
        }

        int occupied_visible() const
        {
            return std::count(mDirsOccup.begin(), mDirsOccup.end(), true);
        }

        char value() const
        {
            return mValue;
        }

        bool is_seat() const
        {
            return mValue == '#' || mValue == 'L';
        }

        bool is_occupied() const
        {
            return mValue == '#';
        }

        bool is_free() const
        {
            return mValue == 'L';
        }

        void visible_changed(Dir dir, int dist, char oldVal, char newVal)
        {
            const auto ind = static_cast<size_t>(dir);
            if (oldVal == newVal) return;
            if (dist == 1)
            {
                oldVal == '#' ? --mAdjOccup : --mAdjFree;
                newVal == '#' ? ++mAdjOccup : ++mAdjFree;
            }
            if (newVal == '.') return;
            if (oldVal == '#' && dist <= mDirsDist[ind])
            {
                mDirsDist[ind] = dist;
                mDirsOccup[ind] = false;
            }
            if (newVal == '#' && dist <= mDirsDist[ind])
            {
                mDirsDist[ind] = dist;
                mDirsOccup[ind] = true;
            }
        }

    private:
        char mValue;
        int mAdjFree, mAdjOccup;
        std::array<int, 8> mDirsDist;
        std::array<bool, 8> mDirsOccup;
    };

    struct SeatMap
    {
        void init(const std::vector<std::string> &data)
        {
            mRows = data.size();
            mCols = data.front().size();
            mTotal = mRows * mCols;
            mSeats.assign(mRows * mCols, {});
            for (auto i = 0; i < mRows; ++i)
            {
                for (auto j = 0; j < mCols; ++j)
                {
                    set_seat_value(i, j, data.at(i).at(j));
                }
            }
        }

        bool set_seat_value(int row, int col, char value)
        {
            const auto pos = row * mCols + col;
            const auto oldVal = mSeats.at(pos).value();
            for (auto d = 1;; ++d)
            {
                int cnt = 0;
                cnt += set_visible_changed(row - d, col - d, d, Dir::DR, oldVal, value);
                cnt += set_visible_changed(row - d, col, d, Dir::D, oldVal, value);
                cnt += set_visible_changed(row - d, col + d, d, Dir::DL, oldVal, value);
                cnt += set_visible_changed(row, col - d, d, Dir::R, oldVal, value);
                cnt += set_visible_changed(row, col + d, d, Dir::L, oldVal, value);
                cnt += set_visible_changed(row + d, col - d, d, Dir::UR, oldVal, value);
                cnt += set_visible_changed(row + d, col, d, Dir::U, oldVal, value);
                cnt += set_visible_changed(row + d, col + d, d, Dir::UL, oldVal, value);
                if (cnt == 0) break;
            }
            mSeats[pos] = value;
            return oldVal != value;
        }

        SeatMap find_seating_v1() const
        {
            SeatMap curr = *this, next = *this;
            while (1)
            {
                int changes = 0;
                for (auto r = 0; r < curr.mRows; ++r)
                {
                    for (auto c = 0; c < curr.mCols; ++c)
                    {
                        const auto &seat = curr.get_seat(r, c);
                        if (!seat.is_seat()) continue;
                        if (seat.is_free() && seat.count_adj_occupied() == 0)
                        {
                            next.set_seat_value(r, c, '#');
                            ++changes;
                        }
                        else if (seat.is_occupied() && seat.count_adj_occupied() >= 4)
                        {
                            next.set_seat_value(r, c, 'L');
                            ++changes;
                        }
                    }
                }
                if (changes == 0) return curr;
                curr.mSeats = next.mSeats;
            }
        }

        SeatMap find_seating_v2() const
        {
            SeatMap curr = *this, next = *this;
            while (1)
            {
                int changes = 0;
                for (auto r = 0; r < curr.mRows; ++r)
                {
                    for (auto c = 0; c < curr.mCols; ++c)
                    {
                        const auto &seat = curr.get_seat(r, c);
                        if (!seat.is_seat()) continue;
                        if (seat.is_free() && seat.occupied_visible() == 0)
                        {
                            next.set_seat_value(r, c, '#');
                            ++changes;
                        }
                        else if (seat.is_occupied() && seat.occupied_visible() >= 5)
                        {
                            next.set_seat_value(r, c, 'L');
                            ++changes;
                        }
                    }
                }
                if (changes == 0) return curr;
                curr.mSeats = next.mSeats;
            }
        }

        int count_occupied() const
        {
            return std::count_if(mSeats.begin(), mSeats.end(), [](const Seat &x) { return x.is_occupied(); });
        }

    private:
        int set_visible_changed(int row, int col, int dist, Dir dir, char oldVal, char newVal)
        {
            if (!check_bounds(row, col)) return 0;
            get_seat(row, col).visible_changed(dir, dist, oldVal, newVal);
            return 1;
        }

        bool check_bounds(int row, int col) const
        {
            return check_row_bounds(row) && check_col_bounds(col);
        }

        bool check_row_bounds(int row) const
        {
            return row >= 0 && row < mRows;
        }

        bool check_col_bounds(int col) const
        {
            return col >= 0 && col < mCols;
        }

        Seat &get_seat(int row, int col)
        {
            return mSeats.at(row * mCols + col);
        }

        int mRows, mCols, mTotal;
        std::vector<Seat> mSeats;
    } seatMap;

    void read_input()
    {
        using namespace std::string_literals;
        std::vector<std::string> lines;
        std::string line;
        std::ifstream infile(INPUT_FILE);
        if (!infile) throw std::runtime_error("File \""s + INPUT_FILE + "\" not found");
        while (infile >> line)
            lines.emplace_back(line);
        infile.close();
        seatMap.init(lines);
    }

    int solve1()
    {
        const auto seating = seatMap.find_seating_v1();
        return seating.count_occupied();
    }

    int solve2()
    {
        const auto seating = seatMap.find_seating_v2();
        return seating.count_occupied();
    }
} // namespace AOC_CPP::Day11

int main()
{
    using namespace AOC_CPP;
    Day11::read_input();
    std::cout << Day11::solve1() << '\n';
    std::cout << Day11::solve2() << '\n';

    return 0;
}
