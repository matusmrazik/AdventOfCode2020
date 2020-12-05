import os
import re

INPUT_PATH = os.path.join(os.path.dirname(__file__), '../../Inputs', 'day02.txt')


class Day02:

    def __init__(self):
        self.inputs = []
        r = re.compile(r'(?P<n1>\d+)-(?P<n2>\d+) (?P<reqC>[a-z]): (?P<passwd>[a-z]+)')
        with open(INPUT_PATH, 'r') as infile:
            for line in infile:
                m = r.match(line)
                self.inputs.append((int(m.group('n1')), int(m.group('n2')), m.group('reqC'), m.group('passwd')))

    def solve1(self):
        solution = 0
        for n1, n2, reqChar, passwd in self.inputs:
            if n1 <= passwd.count(reqChar) <= n2:
                solution += 1
        return solution

    def solve2(self):
        solution = 0
        for n1, n2, reqChar, passwd in self.inputs:
            if (passwd[n1 - 1] == reqChar) ^ (passwd[n2 - 1] == reqChar):
                solution += 1
        return solution


def main():
    x = Day02()
    print(x.solve1())
    print(x.solve2())


if __name__ == '__main__':
    main()
