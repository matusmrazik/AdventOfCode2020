import os
import itertools as it

INPUT_PATH = os.path.join(os.path.dirname(__file__), '../../Inputs', 'day14.txt')


def to_bin_str(num: int, min_len: int = None) -> str:
    bin_str = bin(num)[2:]
    if min_len is None:
        return bin_str
    return f'{"0" * (min_len - len(bin_str))}{bin_str}'


def apply_mask_v1(value: str, mask: str) -> str:
    ret = list(value)
    for i in range(-1, -len(mask) - 1, -1):
        if mask[i] == 'X':
            continue
        ret[i] = mask[i]
    return ''.join(ret)


def apply_mask_v2(value: str, mask: str) -> str:
    ret = list(value)
    for i in range(-1, -len(mask) - 1, -1):
        if mask[i] == '0':
            continue
        ret[i] = mask[i]
    return ''.join(ret)


def find_floating_values(value: str) -> list[str]:
    n_floating, ret = value.count('X'), []
    for x in it.product(*(['01'] * n_floating)):
        temp, i = [], 0
        for c in value:
            if c != 'X':
                temp.append(c)
                continue
            temp.append(x[i])
            i += 1
        ret.append(''.join(temp))
    return ret


class Day14:

    def __init__(self):
        with open(INPUT_PATH, 'r') as infile:
            lines = infile.read().splitlines()
        self.inputs = [_.split(' = ') for _ in lines]

    def solve1(self):
        mem, mask = dict(), ''
        for item in self.inputs:
            if item[0] == 'mask':
                mask = item[1]
                continue
            mem_pos = int(item[0][4:-1])
            mem_val = to_bin_str(int(item[1]), min_len=len(mask))
            mem[mem_pos] = int(apply_mask_v1(mem_val, mask), base=2)
        return sum(mem.values())

    def solve2(self):
        mem, mask = dict(), ''
        for item in self.inputs:
            if item[0] == 'mask':
                mask = item[1]
                continue
            mem_pos = to_bin_str(int(item[0][4:-1]), min_len=len(mask))
            all_mem_pos = find_floating_values(apply_mask_v2(mem_pos, mask))
            mem_val = int(item[1])
            for pos in all_mem_pos:
                mem[int(pos, base=2)] = mem_val
        return sum(mem.values())


def main():
    x = Day14()
    print(x.solve1())
    print(x.solve2())


if __name__ == '__main__':
    main()
