import os
from functools import reduce

INPUT_PATH = os.path.join(os.path.dirname(__file__), '../../Inputs', 'day03.txt')


class Day03:

    def __init__(self):
        self.map = []
        with open(INPUT_PATH, 'r') as infile:
            for line in infile:
                self.map.append(line.strip())

    def count_trees(self, right: int, down: int):
        row, col, ret = 0, 0, 0
        while 1:
            row += down
            if row >= len(self.map):
                break
            col = (col + right) % len(self.map[row])
            if self.map[row][col] == '#':
                ret += 1
        return ret

    def solve1(self):
        return self.count_trees(right=3, down=1)

    def solve2(self):
        return reduce(
            lambda a, b: a * self.count_trees(**b),
            [{'right': 1, 'down': 1}, {'right': 3, 'down': 1}, {'right': 5, 'down': 1}, {'right': 7, 'down': 1},
             {'right': 1, 'down': 2}], 1
        )


def main():
    x = Day03()
    print(x.solve1())
    print(x.solve2())


if __name__ == '__main__':
    main()
