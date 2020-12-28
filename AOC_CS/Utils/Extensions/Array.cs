using System.Collections.Generic;

namespace AOC_CS.Utils.Extensions
{
    static class Array
    {
        public static int ArgMin<T>(this T[] col) where T : System.IComparable<T>
        {
            if (col.Length == 0) return -1;
            int result = 0;
            for (int i = 1; i < col.Length; ++i)
            {
                if (col[i].CompareTo(col[result]) < 0)
                    result = i;
            }
            return result;
        }

        public static int ArgMax<T>(this T[] col) where T : System.IComparable<T>
        {
            if (col.Length == 0) return -1;
            int result = 0;
            for (int i = 1; i < col.Length; ++i)
            {
                if (col[i].CompareTo(col[result]) > 0)
                    result = i;
            }
            return result;
        }
    }
}
