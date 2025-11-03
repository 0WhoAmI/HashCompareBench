using HashCompareBench.Interfaces;
using HashCompareBench.Models;
using HashCompareBench.Utils;
using System.Collections;

namespace HashCompareBench.Tests
{
    public static class BitPredictionTest
    {
        private const double Z_CRITICAL = 1.96;

        public static BitPredictionTestResult RunTest(IHashFunction hashFunction, int sampleSize, int messageSizeInBytes)
        {
            int n = hashFunction.HashSizeInBits; // Długość skrótu
            int P = sampleSize; // Liczba próbek

            var messages = TestDataGenerator.GenerateRandomMessages(P, messageSizeInBytes);
            int[] oneCounts = new int[n];

            foreach (var message in messages)
            {
                byte[] hash = hashFunction.ComputeHash(message);
                BitArray bitArray = new BitArray(hash);

                for (int i = 0; i < n; i++)
                {
                    if (bitArray[i])
                    {
                        oneCounts[i]++;
                    }
                }
            }

            double[] probabilities = new double[n];
            double[] zStatistics = new double[n];
            bool allBitsPass = true;

            // Wartość oczekiwana to 50%
            double expected = 0.5;

            //Obliczenie statystyki dla każdej pozycji bitu
            for (int i = 0; i < n; i++)
            {
                probabilities[i] = (double)oneCounts[i] / P;

                double stdError = 0.5 / Math.Sqrt(P);
                zStatistics[i] = (probabilities[i] - expected) / stdError;

                if (Math.Abs(zStatistics[i]) > Z_CRITICAL)
                {
                    allBitsPass = false;
                }
            }

            return new BitPredictionTestResult
            {
                AlgorithmName = hashFunction.Name,
                BitProbabilities = probabilities,
                ZStatistics = zStatistics,
                IsPassing = allBitsPass
            };
        }
    }
}
