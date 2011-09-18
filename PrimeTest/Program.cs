using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Diagnostics;
using System.IO;

namespace MyPrimeNumbersChallenge
{
    class Program
    {

        int maxArraySize = 1000000;
        int maxPrimeSize = int.MaxValue;

        //int maxArraySize = 100;
        //int maxPrimeSize = 100000;

        int maxByteCount;

        static void Main(string[] args)
        {
            Program pgm = new Program();
            pgm.Start();
        }

        private void Start()
        {
            //////////////////////////////////////////////////////////////////////
            SpeedTest();

            // 5,000,000 => 328,000 in 15s

            /////////////////////////////////////////////////////////////////////////

            //List<int> primes = GetEarlyPrimes(int.MaxValue);
            //WritePrimeHelpers(primes, 100000);

            //////////////////////////////////////////////////////////////////////////
            //PrintPrimes(int.MaxValue);

            //////////////////////////////////////////////////////////////////////////
            //maxByteCount = (maxPrimeSize / 8) + 1;
            //GenerateByteArray();

            ////////////////////////////////////////////////////////////////////////
            //RunAtkin();

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        private void RunAtkin()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //Atkin atkin = new Atkin(int.MaxValue - 1);
            Atkin atkin = new Atkin(2000000000, 1000000);
            sw.Stop();
            Console.WriteLine("Elapsed: " + sw.Elapsed);
            Console.WriteLine(atkin[19]);
        }

        private void GenerateByteArray()
        {
            byte[] byteList = GetByteList();
            PrintPrimeByteArrayMultipleClasses(byteList);
        }

        private byte[] GetByteList()
        {
            byte[] result = new byte[maxByteCount];

            using (System.IO.StreamReader primesFile = System.IO.File.OpenText(@"C:\workspaces\PrimeNumbersChallenge\PrimeTest\bin\Release\primes.txt"))
            {
                string line = string.Empty;
                int i = 0;
                int currentPrime = 0;
                bool isPrime = true;
                while (!primesFile.EndOfStream && i < maxPrimeSize)
                {
                    if (isPrime)
                    {
                        line = primesFile.ReadLine();
                        currentPrime = int.Parse(line);
                    }

                    isPrime = i == currentPrime;
                    result[(i / 8)] += (byte)(Convert.ToByte(isPrime) << (i % 8));

                    i++;
                };
            }

            return result;
        }

        private void PrintPrimeByteArrayMultipleClasses(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();

            int j = 0;

            List<int> arrayStarts = new List<int>();
            while (j < maxByteCount)
            {
                if (j % maxArraySize == 0)
                {
                    sb.Append("namespace MyPrimeNumbersChallenge{" + Environment.NewLine +
                        "\tpublic static class PrimeByteArray" + j + "{" + Environment.NewLine);

                    arrayStarts.Add(j);
                    sb.Append("\t\tpublic static byte[] _primes_" + j + " = {");
                }

                if (((j + 1) % maxArraySize == 0) || (j == maxByteCount - 1))
                {
                    sb.Append(bytes[j] + "};" + Environment.NewLine);

                    sb.Append("\t}" + Environment.NewLine +
                        "}");

                    File.WriteAllText(@"C:\workspaces\PrimeNumbersChallenge\PrimeNumbersChallenge\PrimeByteArray" + arrayStarts[arrayStarts.Count - 1] + ".cs", sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(bytes[j] + ",");
                }
                j++;
            }

            // Create main file to access all the arrays
            sb.Append("using System;" + Environment.NewLine +
                            "namespace MyPrimeNumbersChallenge{" + Environment.NewLine +
                            "\tstatic class PrimeByteArray{" + Environment.NewLine);

            sb.Append("\t\tpublic static byte GetByte(int index){" + Environment.NewLine +
                "\t\t\tswitch (index / " + maxArraySize + "){" + Environment.NewLine);

            for (int i = 0; i < arrayStarts.Count; i++)
            {
                sb.Append("\t\t\t\tcase " + i + ": return PrimeByteArray" + arrayStarts[i] + "._primes_" + arrayStarts[i] + "[index % " + maxArraySize + "];" + Environment.NewLine);
            }

            sb.Append("\t\t\t}" + Environment.NewLine +
                "\t\t\tthrow new IndexOutOfRangeException(\"Index \" + index + \" was too large\");" + Environment.NewLine);

            sb.Append("\t\t}" + Environment.NewLine +
                "\t}" + Environment.NewLine +
                "}");

            File.WriteAllText(@"C:\workspaces\PrimeNumbersChallenge\PrimeNumbersChallenge\PrimeByteArray.cs", sb.ToString());
        }

        private void PrintPrimeByteArrayPartialClasses(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();

            int j = 0;

            List<int> arrayStarts = new List<int>();
            while (j < maxByteCount)
            {
                if (j % maxArraySize == 0)
                {
                    sb.Append("using System;" + Environment.NewLine +
                        "namespace MyPrimeNumbersChallenge{" + Environment.NewLine +
                        "\tstatic partial class PrimeByteArray{" + Environment.NewLine);

                    arrayStarts.Add(j);
                    sb.Append("\t\tprivate static byte[] _primes_" + j + " = {");
                }

                if (((j + 1) % maxArraySize == 0) || (j == maxByteCount - 1))
                {
                    sb.Append(bytes[j] + "};" + Environment.NewLine);

                    sb.Append("\t}" + Environment.NewLine +
                        "}");

                    File.WriteAllText(@"C:\workspaces\PrimeNumbersChallenge\PrimeNumbersChallenge\PrimeByteArray" + arrayStarts[arrayStarts.Count - 1] + ".cs", sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(bytes[j] + ",");
                }
                j++;
            }

            // Create main file to access all the arrays
            sb.Append("using System;" + Environment.NewLine +
                            "namespace MyPrimeNumbersChallenge{" + Environment.NewLine +
                            "\tstatic partial class PrimeByteArray{" + Environment.NewLine);

            sb.Append("\t\tpublic static byte GetByte(int index){" + Environment.NewLine +
                "\t\t\tswitch (index / " + maxArraySize + "){" + Environment.NewLine);

            for (int i = 0; i < arrayStarts.Count; i++)
            {
                sb.Append("\t\t\t\tcase " + i + ": return _primes_" + arrayStarts[i] + "[index % " + maxArraySize + "];" + Environment.NewLine);
            }

            sb.Append("\t\t\t}" + Environment.NewLine +
                "\t\t\tthrow new IndexOutOfRangeException(\"Index \" + index + \" was too large\");" + Environment.NewLine);

            sb.Append("\t\t}" + Environment.NewLine +
                "\t}" + Environment.NewLine +
                "}");

            File.WriteAllText(@"C:\workspaces\PrimeNumbersChallenge\PrimeNumbersChallenge\PrimeByteArray.cs", sb.ToString());
        }

        private void PrintPrimeByteArrayClass(byte[] bytes)
        {
            using (StreamWriter sw = new StreamWriter(@"C:\workspaces\PrimeNumbersChallenge\PrimeNumbersChallenge\PrimeByteArray.cs"))
            {
                StringBuilder sb = new StringBuilder("using System;" + Environment.NewLine +
                    "namespace MyPrimeNumbersChallenge{" + Environment.NewLine +
                    "\tstatic class PrimeByteArray{" + Environment.NewLine);

                int j = 0;
                List<int> arrayStarts = new List<int>();
                while (j < maxByteCount)
                {
                    if (j % maxArraySize == 0)
                    {
                        arrayStarts.Add(j);
                        sb.Append("\t\tprivate static byte[] _primes_" + j + " = {");
                    }

                    if (((j + 1) % maxArraySize == 0) || (j == maxByteCount - 1))
                    {
                        sb.Append(bytes[j] + "};" + Environment.NewLine);
                        sw.Write(sb.ToString());
                        sw.Flush();
                        sb.Clear();
                    }
                    else
                    {
                        sb.Append(bytes[j] + ",");
                    }
                    j++;
                }

                sb.Append("\t\tpublic static byte GetByte(int index){" + Environment.NewLine +
                    "\t\t\tswitch (index / " + maxArraySize + "){" + Environment.NewLine);

                for (int i = 0; i < arrayStarts.Count; i++)
                {
                    sb.Append("\t\t\t\tcase " + i + ": return _primes_" + arrayStarts[i] + "[index % " + maxArraySize + "];" + Environment.NewLine);
                }

                sb.Append("\t\t\t}" + Environment.NewLine +
                    "\t\t\tthrow new IndexOutOfRangeException(\"Index \" + index + \" was too large\");" + Environment.NewLine);

                sb.Append("\t\t}" + Environment.NewLine +
                    "\t}" + Environment.NewLine +
                    "}");

                sw.Write(sb.ToString());
                sb.Clear();
            }

            //File.WriteAllText(@"C:\workspaces\PrimeNumbersChallenge\PrimeNumbersChallenge\PrimeByteArray.cs", sb.ToString());
        }

        private void PrintPrimes(int stopAt)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"primes.txt", false))
            {
                for (int i = 2; i < stopAt; i++)
                {
                    if (i.IsPrime())
                    {
                        file.WriteLine(i);
                    }
                    if (i % 100000 == 0)
                    {
                        file.Flush();
                        Console.WriteLine("{0:0,000}", i);
                    }
                }
            }
        }

        private void WritePrimeHelpers(List<int> primes, int primesPerFile)
        {
            int numberOfFiles = (primes.Count / primesPerFile) + 1;
            List<string> fileNames = new List<string>();
            List<string> arrayNames = new List<string>();

            for (int i = 0; i < numberOfFiles; i++)
            {
                Console.WriteLine("Printing " + i);

                int startOfRange = i * primesPerFile;
                int endOfRange = Math.Min((i + 1) * primesPerFile - 1, primes.Count);
                fileNames.Add(string.Format(@"primes{0:###}.cs", i));
                arrayNames.Add("_" + endOfRange);


                StringBuilder fileContents = new StringBuilder("using System.Collections.Generic;" + Environment.NewLine + Environment.NewLine +
                    "namespace MyPrimeNumbersChallenge" + Environment.NewLine +
                    "{" + Environment.NewLine +
                    "\tpublic static partial class PrimeList" + Environment.NewLine +
                    "\t{" + Environment.NewLine +
                    "\t\tpublic static int[] _" + endOfRange + " = {");

                for (int j = startOfRange; j < endOfRange; j++)
                {
                    if (j != startOfRange)
                    {
                        fileContents.Append(", ");
                    }
                    fileContents.Append(primes[j]);
                }

                fileContents.Append("};" + Environment.NewLine +
                    "\t}" + Environment.NewLine +
                    "}");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileNames[i], false))
                {
                    file.Write(fileContents);
                }
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"primes.cs", false))
            {
                StringBuilder sb = new StringBuilder("using System.Collections.Generic;" + Environment.NewLine + Environment.NewLine +
                    "namespace MyPrimeNumbersChallenge" + Environment.NewLine +
                    "{" + Environment.NewLine +
                    "\tpublic static partial class PrimeList" + Environment.NewLine +
                    "\t{" + Environment.NewLine);

                sb.Append("\t\tprivate static List<int> _primes;" + Environment.NewLine);
                sb.Append("\t\tpublic static List<int> Primes" + Environment.NewLine +
                    "\t\t{" + Environment.NewLine +
                    "\t\t\tget" + Environment.NewLine +
                    "\t\t\t{" + Environment.NewLine +
                    "\t\t\t\tif(_primes == null)" + Environment.NewLine +
                    "\t\t\t\t{" + Environment.NewLine +
                    "\t\t\t\t\t_primes = new List<int>();" + Environment.NewLine);

                foreach (string listName in arrayNames)
                {
                    sb.Append("\t\t\t\t\t_primes.AddRange(" + listName + ");" + Environment.NewLine);
                }

                sb.Append("\t\t\t\t}" + Environment.NewLine +
                    "\t\t\t\treturn _primes;" + Environment.NewLine +
                    "\t\t\t}" + Environment.NewLine +
                    "\t\t}" + Environment.NewLine);

                sb.Append("\t}" + Environment.NewLine +
                    "}");

                file.Write(sb);
            }
        }

        private List<int> GetEarlyPrimes(int stopAt)
        {
            List<int> earlyPrimes = new List<int>();
            int loopCount = 0;
            int arrayIndex = 0;
            while (loopCount < stopAt)
            {
                if (loopCount.IsPrime())
                {
                    earlyPrimes.Add(loopCount);
                    PrimeHelper.EarlyPrimes = earlyPrimes.ToArray();
                    arrayIndex++;
                }
                if (loopCount % 100000 == 0)
                {
                    Console.WriteLine("{0:0,000}", loopCount);
                }
                loopCount++;
            }
            earlyPrimes.Remove(1);
            return earlyPrimes;
        }

        private void SpeedTest()
        {
            Random random = new Random();
            BigInteger bigInt;
            byte[] bytes = new byte[10];

            Stopwatch sw = new Stopwatch();
            sw.Start();
            int successCount = 0;
            int thrownOut = 0;
            while (sw.ElapsedMilliseconds < 60000)
            {
                //random.NextBytes(bytes);
                //bigInt = new BigInteger(bytes);
                bigInt = new BigInteger(random.Next());
                PrimeNumberChallenge pnc = new PrimeNumberChallenge();
                BigInteger nextPrime = pnc.GetNextPrime(bigInt);
                if (nextPrime != -1)
                {
                    successCount++;
                }
                else
                {
                    thrownOut++;
                }
            }

            sw.Stop();

            Console.WriteLine("Success: " + successCount);
            Console.WriteLine("Thrown out: " + thrownOut);
            Console.WriteLine("Elapsed: " + sw.Elapsed);
        }
    }
}
