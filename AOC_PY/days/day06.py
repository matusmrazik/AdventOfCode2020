import os
from functools import reduce

INPUT_PATH = os.path.join(os.path.dirname(__file__), '../../Inputs', 'day06.txt')


def group_common_answers(group: str):
    return len(reduce(
        lambda acc, answer: acc.intersection(set(answer.strip())),
        group.split('\n'),
        set('abcdefghijklmnopqrstuvwxyz')
    ))


class Day06:

    def __init__(self):
        with open(INPUT_PATH, 'r') as infile:
            self.groups = [x.strip() for x in infile.read().split('\n\n')]

    def solve1(self):
        return reduce(lambda acc, group: acc + len(set(group.replace('\n', ''))), self.groups, 0)

    def solve2(self):
        return reduce(lambda acc, group: acc + group_common_answers(group), self.groups, 0)


def main():
    x = Day06()
    print(x.solve1())
    print(x.solve2())


if __name__ == '__main__':
    main()
