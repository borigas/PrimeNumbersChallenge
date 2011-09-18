using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPrimeNumbersChallenge
{
    public static class IntHelper
    {
        private static Random _random;
        private static Random Random
        {
            get
            {
                if (_random == null)
                {
                    _random = new Random();
                }
                return _random;
            }
        }

        public static bool IsPrimeMillerRabin(this int n)
        {
            // Miller-Rabin from http://rosettacode.org/wiki/Miller-Rabin_primality_test#C.23
            if (n < 2)
            {
                return false;
            }
            if (n != 2 && n % 2 == 0)
            {
                return false;
            }
            int s = n - 1;
            while (s % 2 == 0)
            {
                s >>= 1;
            }
            for (int i = 1; i < 2; i++)
            {
                Random r = new Random();
                double a = r.Next((int)n - 1) + 1;
                int temp = s;
                long mod = checked((long)Math.Pow(a, (double)temp) % n);
                while (temp != n - 1 && mod != 1 && mod != n - 1)
                {
                    mod = (mod * mod) % n;
                    temp = temp * 2;
                }
                if (mod != n - 1 && temp % 2 == 0)
                {
                    return false;
                }
            }
            return true;

            // // My attempt at a Miller-Rabin implementation
            //int d = n - 1;
            //int s = 0;
            //while ((d & 1) == 0)
            //{
            //    d = d >> 1;
            //    s++;
            //}
            //int a = Random.Next(2, d - 1);

            //long x = checked((long)Math.Pow(a, d) % n);
            //if (x == 1 || x == n - 1) { return true; }
            //for (int r = 1; r < s; r++)
            //{
            //    x = (int)Math.Pow(x, 2) % n;
            //    if (x == 1)
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        continue;
            //    }
            //}
            //return true;

            //// Miller-Rabin from http://rosettacode.org/wiki/Miller-Rabin_primality_test#C.23
            //if (n < 2)
            //{
            //    return false;
            //}
            //if (n != 2 && n % 2 == 0)
            //{
            //    return false;
            //}
            //int s = n - 1;
            //while (s % 2 == 0)
            //{
            //    s >>= 1;
            //}
            //for (int i = 1; i < 11; i++)
            //{
            //    Random r = new Random();
            //    double a = r.Next((int)n - 1) + 1;
            //    int temp = s;
            //    int mod = (int)Math.Pow(a, (double)temp) % n;
            //    while (temp != n - 1 && mod != 1 && mod != n - 1)
            //    {
            //        mod = (mod * mod) % n;
            //        temp = temp * 2;
            //    }
            //    if (mod != n - 1 && temp % 2 == 0)
            //    {
            //        return false;
            //    }
            //}
            //return true;

            // My attempt at a Miller-Rabin implementation
            //int d = n - 1;
            //int s = 0;
            //while ((d & 1) == 1)
            //{
            //    d = d >> 1;
            //    s++;
            //}
            //int a = Random.Next(2, d - 1);

            //int x = (int)Math.Pow(a, d) % n;
            //if (x == 1 || x == n - 1) { return true; }
            //for (int r = 1; r < s; r++)
            //{
            //    x = (int)Math.Pow(x, 2) % n;
            //    if (x == 1)
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        continue;
            //    }
            //}
            //return true;
        }

        public static bool IsPrime(this int n)
        {
            return n.IsPrime_ModdingWithEarlierPrimes();
        }

        private static bool IsPrime_ModdingWithEarlierPrimes(this int n)
        {
            // 1st Implementation (Simple modding) -------------------------------------------------------
            // Check by dividing by a list of precomputed primes
            int squareRoot = n.SquareRoot();
            //for (int i = 0; i < PrimeHelper.EarlyPrimes.Length; i++)
            for (int i = 0; i < PrimeHelper.EarlyPrimes.Length; i++)
            {
                if (n % PrimeHelper.EarlyPrimes[i] == 0 && n != PrimeHelper.EarlyPrimes[i])
                {
                    return false;
                }

                if (PrimeHelper.EarlyPrimes[i] > squareRoot)
                {
                    return true;
                }
            }

            // Check stuff past the end of the array by dividing by increments of 2
            for (int i = PrimeHelper.EarlyPrimes[PrimeHelper.EarlyPrimes.Length - 1]; i <= squareRoot; i += 2)
            //for (int i = PrimeList.Primes[PrimeList.Primes.Count - 1]; i <= squareRoot; i += 2)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static int SquareRoot(this int n)
        {
            return (int)Math.Ceiling(Math.Sqrt(n));
        }

        //public static int SquareRoot(this int n)
        //{
        //    //double epsilon = 1.0e-9;
        //    //double epsilon = 1.0e-9;
        //    double epsilon = 0.5;
        //    double guess = n / 2.0;

        //    double value = n;

        //    double result = ((value / guess) + guess) / 2;

        //    while (Math.Abs(result - guess) > epsilon)
        //    {
        //        guess = result;
        //        result = ((value / guess) + guess) / 2;
        //    }

        //    return (int)Math.Ceiling(result);
        //}

        //public static int SquareRoot(this int n)
        //{
        //    if (n == 0) return 0;
        //    if (n > 0)
        //    {
        //        int bitLength = Convert.ToInt32(Math.Ceiling(Math.Log(n, 2)));
        //        int root = 1 << (bitLength / 2);
        //        while (!isSqrt(n, root))
        //        {
        //            root += n / root;
        //            root /= 2;
        //        }
        //        return root;
        //    } throw new ArithmeticException("NaN");
        //}


        //private static Boolean isSqrt(int n, int root)
        //{
        //    int lowerBound = root * root;
        //    //BigInteger upperBound = (root + 1) * (root + 1);
        //    int upperBound = lowerBound + 2 * root + 1;
        //    return (n >= lowerBound && n < upperBound);
        //}
    }
}
