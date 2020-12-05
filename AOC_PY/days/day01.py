import os

import numpy as np

INPUT_PATH = os.path.join(os.path.dirname(__file__), '../../Inputs', 'day01.txt')


class Day01:

    def __init__(self):
        self.inputs = np.loadtxt(INPUT_PATH, dtype=np.int32)

    def solve1(self):
        for i in range(len(self.inputs)):
            for j in range(i + 1, len(self.inputs)):
                if self.inputs[i] + self.inputs[j] != 2020:
                    continue
                return self.inputs[i] * self.inputs[j]

    def solve2(self):
        for i in range(len(self.inputs)):
            for j in range(i + 1, len(self.inputs)):
                for k in range(j + 1, len(self.inputs)):
                    if self.inputs[i] + self.inputs[j] + self.inputs[k] != 2020:
                        continue
                    return self.inputs[i] * self.inputs[j] * self.inputs[k]


def main():
    x = Day01()
    print(x.solve1())
    print(x.solve2())


if __name__ == '__main__':
    main()
