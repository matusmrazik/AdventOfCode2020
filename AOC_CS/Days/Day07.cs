using AOC_CS.Utils.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC_CS.Days
{
    class Day07
    {
        const string INPUT_FILE = "Inputs\\day07.txt";

        private Dictionary<string, Dictionary<string, int>> graphBck;
        private Dictionary<string, Dictionary<string, int>> graphFwd;

        public Day07()
        {
            graphBck = null;
        }

        private void ReadInput()
        {
            if (graphBck != null && graphFwd != null) return;
            var allRules = File.ReadAllLines(INPUT_FILE);

            var tmpGraphBck = new Dictionary<string, Dictionary<string, int>>();
            var tmpGraphFwd = new Dictionary<string, Dictionary<string, int>>();
            foreach (var rule in allRules)
            {
                var tmp = rule.TrimEnd('.').Split(" bags contain ");
                var srcColor = tmp[0];

                if (!tmpGraphBck.ContainsKey(srcColor))
                {
                    tmpGraphBck.Add(srcColor, new Dictionary<string, int>());
                }

                Dictionary<string, int> nodeFwd;
                try
                {
                    nodeFwd = tmpGraphFwd[srcColor];
                }
                catch (KeyNotFoundException)
                {
                    nodeFwd = new Dictionary<string, int>();
                    tmpGraphFwd.Add(srcColor, nodeFwd);
                }

                if (tmp[1] == "no other bags") continue;

                var destColors = tmp[1].Split(", ");
                foreach (var colorBag in destColors)
                {
                    var match = Regex.Match(colorBag, @"^(?<count>[\d]+) (?<color>[a-z ]+) bag[s]?$");
                    var count = int.Parse(match.Groups["count"].Value);
                    var destColor = match.Groups["color"].Value;

                    Dictionary<string, int> nodeBck;
                    try
                    {
                        nodeBck = tmpGraphBck[destColor];
                    }
                    catch (KeyNotFoundException)
                    {
                        nodeBck = new Dictionary<string, int>();
                        tmpGraphBck.Add(destColor, nodeBck);
                    }
                    nodeBck.Add(srcColor, count);

                    if (!nodeFwd.ContainsKey(destColor))
                    {
                        nodeFwd.Add(destColor, count);
                    }
                }
            }
            graphBck = tmpGraphBck;
            graphFwd = tmpGraphFwd;
        }

        public int Solve1()
        {
            ReadInput();
            const string SRC_COLOR = "shiny gold";
            var processed = new HashSet<string>(new[] { SRC_COLOR });
            var queue = new Queue<string>(graphBck[SRC_COLOR].Keys);
            var solution = 0;
            while (queue.Count > 0)
            {
                var color = queue.Dequeue();
                if (processed.Contains(color)) continue;
                foreach (var destColor in graphBck[color].Keys)
                {
                    queue.Enqueue(destColor);
                }
                ++solution;
                processed.Add(color);
            }
            return solution;
        }

        public int Solve2()
        {
            ReadInput();
            const string SRC_COLOR = "shiny gold";
            var queue = new Queue<KeyValuePair<string, int>>(graphFwd[SRC_COLOR].GetEntries());
            var solution = 0;
            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                foreach (var node in graphFwd[item.Key])
                {
                    queue.Enqueue(new KeyValuePair<string, int>(node.Key, node.Value * item.Value));
                }
                solution += item.Value;
            }
            return solution;
        }
    }
}
