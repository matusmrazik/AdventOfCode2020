namespace AOC_CS.Utils
{
    static class Numbers
    {
        public static int Gcd(int a, int b)
        {
            while (b != 0)
            {
                var x = a % b;
                a = b;
                b = x;
            }
            return a;
        }

        public static long Gcd(long a, long b)
        {
            while (b != 0)
            {
                var x = a % b;
                a = b;
                b = x;
            }
            return a;
        }

        public static int Lcm(int a, int b)
        {
            return (a / Gcd(a, b)) * b;
        }

        public static long Lcm(long a, long b)
        {
            return (a / Gcd(a, b)) * b;
        }
    }
}
