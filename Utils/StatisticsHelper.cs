using System.Collections;

namespace HashCompareBench.Utils
{
    public static class StatisticsHelper
    {
        /// <summary>
        /// Oblicza średnią.
        /// </summary>
        public static double CalculateMean(IEnumerable<double> values)
        {
            return values.Average();
        }

        /// <summary>
        /// Oblicza odchylenie standardowe.
        /// </summary>
        public static double CalculateStdDev(IEnumerable<double> values, double mean)
        {
            if (values.Count() <= 1) return 0;

            double sumOfSquares = values.Sum(val => (val - mean) * (val - mean));
            return Math.Sqrt(sumOfSquares / (values.Count() - 1));
        }

        /// <summary>
        /// Oblicza odległość Hamminga między dwoma tablicami bajtów.
        /// </summary>
        public static double CalculateHammingDistance(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length != hash2.Length)
            {
                throw new ArgumentException("Skróty muszą mieć tę samą długość.");
            }

            double distance = 0;
            for (int i = 0; i < hash1.Length; i++)
            {
                // Oblicz x3 = x1 XOR x2
                byte xorByte = (byte)(hash1[i] ^ hash2[i]);
                // Policz jedynki w bajcie xorByte
                distance += CountSetBits(xorByte);
            }
            return distance;
        }

        /// <summary>
        /// Liczy bity '1' w pojedynczym bajcie.
        /// </summary>
        private static int CountSetBits(byte b)
        {
            int count = 0;
            for (int i = 0; i < 8; i++)
            {
                if ((b & (1 << i)) != 0)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Oblicza parametry dla testu serii: n0 (zera), n1 (jedynki) i R (liczba serii).
        /// </summary>
        public static (int n0, int n1, int R) CalculateRuns(byte[] hash)
        {
            BitArray bitArray = new BitArray(hash);
            int n0 = 0, n1 = 0, R = 0;

            if (bitArray.Length == 0)
            {
                return (0, 0, 0);
            }

            // Policz n0 i n1
            for (int i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i]) n1++;
                else n0++;
            }

            // Policz serie (R)
            R = 1; // Zawsze jest co najmniej jedna seria
            for (int i = 1; i < bitArray.Length; i++)
            {
                if (bitArray[i] != bitArray[i - 1])
                {
                    R++; // Nowa seria
                }
            }

            return (n0, n1, R);
        }
    }
}
