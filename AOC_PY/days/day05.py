import os
import numpy as np

INPUT_PATH = os.path.join(os.path.dirname(__file__), '../../Inputs', 'day05.txt')


def get_seat_id(pass_str: str):
    row = int(pass_str[:7].replace('F', '0').replace('B', '1'), 2)
    col = int(pass_str[7:].replace('L', '0').replace('R', '1'), 2)
    return row * 8 + col


class Day05:

    def __init__(self):
        self.passes = []
        self.seat_ids = []
        with open(INPUT_PATH, 'r') as infile:
            for line in infile:
                p = line.strip()
                self.passes.append(p)
                self.seat_ids.append(get_seat_id(p))

    def solve1(self):
        return np.max(self.seat_ids)

    def solve2(self):
        seats_taken = np.zeros(128 * 8, dtype=np.bool)
        first, last = np.min(self.seat_ids), np.max(self.seat_ids)
        seats_taken[first:last + 1] = True
        seats_taken[self.seat_ids] = False
        return np.where(seats_taken == True)[0][0]


def main():
    x = Day05()
    print(x.solve1())
    print(x.solve2())


if __name__ == '__main__':
    main()
