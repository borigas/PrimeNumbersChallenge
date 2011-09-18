using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace MyPrimeNumbersChallenge
{
    public static class BigIntHelper
    {
        public static bool IsPrime(this BigInteger bigInt)
        {
            //Console.WriteLine("IsPrime?: " + bigInt);
            BigInteger squareRoot = bigInt.SquareRoot();
            for (int i = 0; i < PrimeHelper.EarlyPrimes.Length; i++)
            {
                //Console.WriteLine("Checking " + bigInt + " against: " + PrimeHelper.EarlyPrimes[i]);
                if (bigInt % PrimeHelper.EarlyPrimes[i] == 0)
                {
                    return false;
                }

                if (PrimeHelper.EarlyPrimes[i] > squareRoot)
                {
                    return true;
                }
            }

            for (BigInteger i = PrimeHelper.EarlyPrimes[PrimeHelper.EarlyPrimes.Length - 1]; i <= squareRoot; i += 2)
            {
                if (bigInt % i == 0)
                {
                    return false;
                }
            }

            return true;
        }


        // Approximates square root
        //public static BigInteger SquareRoot(this BigInteger bigInt)
        //{
        //    int length = bigInt.ToString().Length;

        //    BigInteger result;
        //    // If is odd
        //    if (length % 2 == 1)
        //    {
        //        int n = (length - 1) / 2;
        //        result = new BigInteger(2 * Math.Pow(10, n));
        //    }
        //    else
        //    {
        //        int n = (length - 2) / 2;
        //        result = new BigInteger(6 * Math.Pow(10, n));
        //    }
        //    return result;
        //}

        public static BigInteger SquareRoot(this BigInteger n)
        {
            if (n == 0) return 0;
            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);
                while (!isSqrt(n, root))
                {
                    root += n / root;
                    root /= 2;
                }
                return root;
            } throw new ArithmeticException("NaN");
        }


        private static Boolean isSqrt(BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            //BigInteger upperBound = (root + 1) * (root + 1);
            BigInteger upperBound = lowerBound + 2 * root + 1;
            return (n >= lowerBound && n < upperBound);
        }
    }
}
