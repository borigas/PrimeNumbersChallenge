using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using OmahaMTG.Challenge.Challenges;
using OmahaMTG.Challenge.ChallengeCommon;

namespace MyPrimeNumbersChallenge
{
    public class PrimeNumberChallenge : IPrimeNumberChallenge
    {
        //private const int MAX_INT_PRIME = 2147483647; // int.MaxValue. Also, it is prime
        //private const int MAX_INT_PRIME = 2147483587; // Last prime before int.MaxValue
        //private const int MAX_INT_PRIME = 999999937;    // Last prime before 1,000,000,000
        private const int MAX_INT_PRIME = 499999993;    // Last prime before 500,000,000
        //private const int MAX_INT_PRIME = 9973;       // Last prime before 10,000
        private const int BYTE_ARRAY_SIZE = 1000000;
        //private const int BYTE_ARRAY_SIZE = 100;

        private static readonly Atkin atkin = new Atkin(MAX_INT_PRIME, BYTE_ARRAY_SIZE);

        public BigInteger GetNextPrime(BigInteger startingValue)
        {
            //return BinarySearchEarlyPrimes(startingValue);

            //return GetNextPrime_IteratePossiblePrimes(startingValue);

            //return GetNextPrime_IteratePossibleIntPrimes(startingValue);

            //return GetNextPrime_ByteArrayLookup(startingValue);

            return GetNextPrime_AtkinLookup(startingValue);
        }

        private BigInteger GetNextPrime_AtkinLookup(BigInteger startingValue)
        {
            if (startingValue < MAX_INT_PRIME)
            {
                int start = (int)startingValue + 1;
                while (atkin[start] == false)
                //while(true)
                {
                    start++;
                }

                //int result = byteIndex * 8 + byteOffset;

                return start;
            }
            else
            {
                return -1;
            }
        }

        private BigInteger GetNextPrime_ByteArrayLookup(BigInteger startingValue)
        {
            if (startingValue < MAX_INT_PRIME)
            {
                int start = (int)startingValue + 1;
                int byteIndex = start / 8;
                int byteOffset = start % 8;
                while (PrimeByteArray.GetByte(byteIndex).GetBit(byteOffset) == false)
                //while(true)
                {
                    byteOffset++;
                    if (byteOffset == 8)
                    {
                        byteOffset = 0;
                        byteIndex++;
                    }
                }

                int result = byteIndex * 8 + byteOffset;

                return result;
            }
            else
            {
                return -1;
            }
        }

        private static BigInteger GetNextPrime_IteratePossibleIntPrimes(BigInteger startingValue)
        {
            //------------------------------------------------------------------------------
            // Handle ints only
            if (startingValue > MAX_INT_PRIME)
            {
                return -1;
            }
            int value = (int)startingValue;
            int matched = 0;
            int differed = 0;
            // Iterative approach
            for (int i = value + (value % 2 == 0 ? 1 : 2); true; i += 2)
            {
                bool primeByBasicMethod = i.IsPrime();
                //bool miller = i.IsPrimeMillerRabin();
                //if (basic != miller)
                //{
                //    differed++;
                //}
                //else
                //{
                //    matched++;
                //}

                //if (!miller)
                //{
                //    Console.WriteLine("false");
                //}

                if (primeByBasicMethod)
                {
                    return i;
                }
            }
        }

        private static BigInteger GetNextPrime_IteratePossiblePrimes(BigInteger startingValue)
        {
            //------------------------------------------------------------------------------
            for (BigInteger i = startingValue + (startingValue.IsEven ? 1 : 2); true; i += 2)
            {
                if (i.IsPrime())
                {
                    return i;
                }
            }
        }

        private static BigInteger BinarySearchEarlyPrimes(BigInteger startingValue)
        {
            //------------------------------------------------------------------------------
            // binary search a predefined array of primes
            if (startingValue <= PrimeHelper.EarlyPrimes[PrimeHelper.EarlyPrimes.Length - 1])
            {
                int index = Array.BinarySearch(PrimeHelper.EarlyPrimes, (int)startingValue);
                if (index < 0)
                {
                    index = ~index;
                }
                else
                {
                    index++;
                }
                return PrimeHelper.EarlyPrimes[index];
            }
            else
            {
                return -1;
            }

            //// Binary search generated list
            //if (startingValue <= PrimeList.Primes[PrimeList.Primes.Count - 1])
            //{
            //    int index = PrimeList.Primes.BinarySearch((int)startingValue);
            //    if (index < 0)
            //    {
            //        index = ~index;
            //    }
            //    else
            //    {
            //        index++;
            //    }
            //    return PrimeList.Primes[index];
            //}
            //else
            //{
            //    return -1;
            //}
        }

        public string AuthorNotes
        {
            get
            {
                return "Ben Origas";
            }
        }
    }
}
