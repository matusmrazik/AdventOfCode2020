import os
import re

INPUT_PATH = os.path.join(os.path.dirname(__file__), '../../Inputs', 'day04.txt')


def validate_field(name: str, value: str):
    if name == 'byr':
        return re.match(r'^(19[2-9][0-9]|200[0-2])$', value) is not None
    elif name == 'iyr':
        return re.match(r'^(201[0-9]|2020)$', value) is not None
    elif name == 'eyr':
        return re.match(r'^(202[0-9]|2030)$', value) is not None
    elif name == 'hgt':
        return re.match(r'^(1[5-8][0-9]cm|19[0-3]cm|59in|6[0-9]in|7[0-6]in)$', value) is not None
    elif name == 'hcl':
        return re.match(r'^(#[0-9a-f]{6})$', value) is not None
    elif name == 'ecl':
        return re.match(r'^(amb|blu|brn|gry|grn|hzl|oth)$', value) is not None
    elif name == 'pid':
        return re.match(r'^(\d{9})$', value) is not None
    return True


class Day04:

    def __init__(self):
        self.passports = []
        with open(INPUT_PATH, 'r') as infile:
            lines = infile.read()
        for line in lines.split('\n\n'):
            self.passports.append(re.split(r'\n| |:', line.strip()))

    def solve1(self):
        solution = 0
        for passport in self.passports:
            fields = {'byr', 'iyr', 'eyr', 'hgt', 'hcl', 'ecl', 'pid'}
            for i in range(0, len(passport), 2):
                fields.discard(passport[i])
            if len(fields) == 0:
                solution += 1
        return solution

    def solve2(self):
        solution = 0
        for passport in self.passports:
            valid, fields = True, {'byr', 'iyr', 'eyr', 'hgt', 'hcl', 'ecl', 'pid'}
            for i in range(0, len(passport), 2):
                if validate_field(passport[i], passport[i + 1]):
                    fields.discard(passport[i])
                else:
                    valid = False
                    break
            if valid and len(fields) == 0:
                solution += 1
        return solution


def main():
    x = Day04()
    print(x.solve1())
    print(x.solve2())


if __name__ == '__main__':
    main()
