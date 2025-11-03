using HashCompareBench.Interfaces;
using HashCompareBench.Models;
using HashCompareBench.Utils;

namespace HashCompareBench.Tests
{
    public static class RunsTest
    {
        private const double Z_CRITICAL = 1.96;

        public static RunsTestResult RunTest(IHashFunction hashFunction, int sampleSize, int messageSizeInBytes)
        {
            var messages = TestDataGenerator.GenerateRandomMessages(sampleSize, messageSizeInBytes);
            var zStatistics = new List<double>(sampleSize);
            int passCount = 0;

            foreach (var message in messages)
            {
                byte[] hash = hashFunction.ComputeHash(message);

                (int n0, int n1, int R) = StatisticsHelper.CalculateRuns(hash);

                double n = n0 + n1;
                if (n == 0) continue; // Pomijamy pusty hash

                double expectedR = (2.0 * n0 * n1 / n) + 1.0;

                double numerator = 2.0 * n0 * n1 * (2.0 * n0 * n1 - n);
                double denominator = (n * n) * (n - 1);

                // Unikamy dzielenia przez 0
                if (denominator == 0) continue;
                double stdDev = Math.Sqrt(numerator / denominator);

                // Unikamy dzielenia przez 0
                if (stdDev == 0) continue;
                double z = (R - expectedR) / stdDev;

                zStatistics.Add(z);

                if (Math.Abs(z) <= Z_CRITICAL)
                {
                    passCount++;
                }
            }

            double percentagePassing = (double)passCount / sampleSize;

            return new RunsTestResult
            {
                AlgorithmName = hashFunction.Name,
                IndividualZStatistics = zStatistics.ToArray(),
                PercentageOfHashesPassing = percentagePassing
            };
        }
    }
}
