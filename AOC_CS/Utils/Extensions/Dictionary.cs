using System.Collections.Generic;
using System.Linq;

namespace AOC_CS.Utils.Extensions
{
    static class Dictionary
    {
        public static IEnumerable<KeyValuePair<TKey, TValue>> GetEntries<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            return dict.Aggregate(
                new List<KeyValuePair<TKey, TValue>>(),
                (acc, val) =>
                {
                    acc.Add(val);
                    return acc;
                }
            );
        }
    }
}
