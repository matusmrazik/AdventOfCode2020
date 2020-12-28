import os
import numpy as np

INPUT_PATH = os.path.join(os.path.dirname(__file__), '../../Inputs', 'day13.txt')


class Day13:

    def __init__(self):
        with open(INPUT_PATH, 'r') as infile:
            lines = infile.read().splitlines()
        self.depart_time = int(lines[0])
        self.all_buses = lines[1].split(',')
        self.buses = [int(_) for _ in self.all_buses if _ != 'x']
        self.indices = [_i for _i, _a in enumerate(self.all_buses) if _a != 'x']

    def solve1(self):
        mods = [_ - self.depart_time % _ for _ in self.buses]
        pos = np.argmin(mods)
        return mods[pos] * self.buses[pos]

    def solve2(self):
        mods = [(x - i) % x for x, i in zip(self.buses, self.indices)]
        cur, cm = 1, 1
        for val, m in zip(self.buses, mods):
            while cur % val != m:
                cur += cm
            cm = np.lcm(cm, val)
        return cur


def main():
    x = Day13()
    print(x.solve1())
    print(x.solve2())


if __name__ == '__main__':
    main()
