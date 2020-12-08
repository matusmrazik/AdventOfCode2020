import os
import re

INPUT_PATH = os.path.join(os.path.dirname(__file__), '../../Inputs', 'day07.txt')

SRC_COLOR = 'shiny gold'


class Day07:

    def __init__(self):
        with open(INPUT_PATH, 'r') as infile:
            all_rules = infile.readlines()
        self.graphFwd = dict()
        self.graphBck = dict()
        for rule in all_rules:
            src_color, bag_rules = rule.rstrip('\n.').split(' bags contain ')

            if src_color not in self.graphBck:
                self.graphBck[src_color] = dict()

            if src_color not in self.graphFwd:
                self.graphFwd[src_color] = dict()
            node_fwd = self.graphFwd[src_color]

            if bag_rules == 'no other bags':
                continue
            for bag in bag_rules.split(', '):
                match = re.match(r'^(?P<count>[\d]+) (?P<color>[a-z ]+) bag[s]?$', bag)
                count = int(match.group('count'))
                dest_color = match.group('color')

                if dest_color not in self.graphBck:
                    self.graphBck[dest_color] = dict()
                self.graphBck[dest_color][src_color] = count

                if dest_color not in node_fwd:
                    node_fwd[dest_color] = count

    def solve1(self):
        solution, processed, queue = 0, set(), [_ for _ in self.graphBck[SRC_COLOR].keys()]
        while queue:
            color = queue.pop(0)
            if color in processed:
                continue
            queue += [_ for _ in self.graphBck[color].keys()]
            solution += 1
            processed.add(color)
        return solution

    def solve2(self):
        solution, queue = 0, [_ for _ in self.graphFwd[SRC_COLOR].items()]
        while queue:
            key, val = queue.pop(0)
            queue += [(k, v * val) for k, v in self.graphFwd[key].items()]
            solution += val
        return solution


def main():
    x = Day07()
    print(x.solve1())
    print(x.solve2())


if __name__ == '__main__':
    main()
