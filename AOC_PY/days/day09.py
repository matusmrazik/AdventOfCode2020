import os

INPUT_PATH = os.path.join(os.path.dirname(__file__), '../../Inputs', 'day09.txt')


class Day09:
    PREAMBLE = 25

    def __init__(self):
        with open(INPUT_PATH, 'r') as infile:
            lines = infile.readlines()
        self.inputs = [int(_) for _ in lines]

        self.sums = [set() for _ in lines]
        for i in range(len(self.inputs)):
            for j in range(i + 1, min(i + self.PREAMBLE, len(self.inputs))):
                tmp_sum = self.inputs[i] + self.inputs[j]
                for k in range(j + 1, min(i + self.PREAMBLE + 1, len(self.inputs))):
                    self.sums[k].add(tmp_sum)

    def solve1(self):
        for i in range(self.PREAMBLE, len(self.inputs)):
            if self.inputs[i] not in self.sums[i]:
                return self.inputs[i]

    def solve2(self):
        num = self.solve1()
        for i in range(len(self.inputs)):
            total, min_val, max_val = (self.inputs[i],) * 3
            for j in range(i + 1, len(self.inputs)):
                total += self.inputs[j]
                if total > num:
                    break
                min_val = min(min_val, self.inputs[j])
                max_val = max(max_val, self.inputs[j])
                if total == num:
                    return min_val + max_val


def main():
    x = Day09()
    print(x.solve1())
    print(x.solve2())


if __name__ == '__main__':
    main()
