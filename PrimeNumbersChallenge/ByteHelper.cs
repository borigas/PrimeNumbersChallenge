using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPrimeNumbersChallenge
{
    static class ByteHelper
    {
        public static bool GetBit(this byte b, int bitNumber)
        {
            return (b & (1 << bitNumber)) != 0;
        }
    }
}
