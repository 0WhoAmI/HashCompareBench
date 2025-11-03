using HashCompareBench.Interfaces;
using HashCompareBench.Models;
using HashCompareBench.Utils;

namespace HashCompareBench.Tests
{
    public static class HammingDistanceTest
    {
        private const double Z_CRITICAL = 1.96;

        public static HammingDistanceTestResult RunTest(IHashFunction hashFunction, int sampleSize, int messageSizeInBytes)
        {
            var messagePairs = TestDataGenerator.GenerateSimilarMessagePairs(sampleSize, messageSizeInBytes);
            var distances = new List<double>(sampleSize);

            foreach (var pair in messagePairs)
            {
                byte[] h1 = hashFunction.ComputeHash(pair.Message1);
                byte[] h2 = hashFunction.ComputeHash(pair.Message2);
                distances.Add(StatisticsHelper.CalculateHammingDistance(h1, h2));
            }

            double mean = StatisticsHelper.CalculateMean(distances);
            double stdDev = StatisticsHelper.CalculateStdDev(distances, mean);

            int n = hashFunction.HashSizeInBits;
            double expected = n / 2.0;

            double zStatistic = (mean - expected) / (stdDev / Math.Sqrt(sampleSize));

            bool isPassing = Math.Abs(zStatistic) <= Z_CRITICAL;

            return new HammingDistanceTestResult
            {
                AlgorithmName = hashFunction.Name,
                MeanDistance = mean,
                ExpectedDistance = expected,
                StandardDeviation = stdDev,
                ZStatistic = zStatistic,
                IsPassing = isPassing
            };
        }
    }
}
