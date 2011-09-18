using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;

namespace MyPrimeNumbersChallenge
{
    public class Atkin
    {
        private readonly int limit;
        private List<byte[]> byteArrays = new List<byte[]>();
        private int byteArraySize;

        public Atkin(int limit, int byteArraySize)
        {
            this.limit = limit;
            this.byteArraySize = byteArraySize;
            for (int i = 0; i < Math.Ceiling((double)(limit + 1) / (double)byteArraySize / 8.0); i++)
            {
                byteArrays.Add(new byte[byteArraySize]);
            }
            FindPrimes();
        }

        public bool this[int i]
        {
            get
            {
                int byteIndex = i / 8;

                int arrayIndex = byteIndex / byteArraySize;
                int arrayOffset = byteIndex % byteArraySize;
                int bitIndex = i % 8;
                return this[arrayIndex, arrayOffset, bitIndex];
            }
            set
            {
                int byteIndex = i / 8;

                int arrayIndex = byteIndex / byteArraySize;
                int arrayOffset = byteIndex % byteArraySize;
                int bitIndex = i % 8;
                this[arrayIndex, arrayOffset, bitIndex] = value;
            }
        }

        private bool this[int i, int j, int k]
        {
            get
            {
                return byteArrays[i][j].GetBit(k);
            }
            set
            {
                if (value)
                {
                    byteArrays[i][j] |= (byte)(1 << k);
                }
                else
                {
                    byteArrays[i][j] &= (byte)~(1 << k);
                }
            }
        }

        private void FindPrimes()
        {
            var sqrt = (int)Math.Sqrt(limit);

            Parallel.For(1, sqrt, x =>
            //for (long x = 1; x <= sqrt; x++)
            {
                for (int y = 1; y <= sqrt; y++)
                {
                    if ((x & 1) == 1 || (y & 1) == 1)
                    {
                        var n = 4 * x * x + y * y;
                        if (n <= limit && (n % 12 == 1 || n % 12 == 5))
                            this[n] ^= true;

                        n = 3 * x * x + y * y;
                        if (n <= limit && n % 12 == 7)
                            this[n] ^= true;

                        n = 3 * x * x - y * y;
                        if (x > y && n <= limit && n % 12 == 11)
                            this[n] ^= true;
                    }
                }
            });

            Parallel.For(5, (sqrt + 1), n =>
            //for (int n = 5; n <= sqrt; n++)
            {
                if (this[n])
                {
                    var s = n * n;
                    for (int k = s; k <= limit && k >= 0; k += s)
                        this[k] = false;
                }
            });

            this[2] = true;
            this[3] = true;
        }
    }
}
