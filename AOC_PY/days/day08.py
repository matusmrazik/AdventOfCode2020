import os

INPUT_PATH = os.path.join(os.path.dirname(__file__), '../../Inputs', 'day08.txt')


class Day08:

    def __init__(self):
        with open(INPUT_PATH, 'r') as infile:
            lines = infile.read().splitlines()
        parse = lambda x: (str(x[0]), int(x[1]))
        self.program = [parse(line.split(' ')) for line in lines]

    def run_program(self, change_line=None):
        acc, line, processed = 0, 0, [False for _ in self.program]
        while 1:
            if line == len(self.program):
                return True, acc
            if processed[line]:
                return False, acc
            processed[line] = True
            op, param = self.program[line]
            if line == change_line:
                if op == 'nop':
                    op = 'jmp'
                elif op == 'jmp':
                    op = 'nop'
            if op == 'nop':
                line += 1
            elif op == 'acc':
                acc += param
                line += 1
            elif op == 'jmp':
                line += param

    def solve1(self):
        _, acc = self.run_program()
        return acc

    def solve2(self):
        for line in range(len(self.program)):
            ok, acc = self.run_program(line)
            if ok:
                return acc
        return 0


def main():
    x = Day08()
    print(x.solve1())
    print(x.solve2())


if __name__ == '__main__':
    main()
