namespace HashCompareBench.Models
{
    public class HammingDistanceTestResult
    {
        public string AlgorithmName { get; set; }
        public double MeanDistance { get; set; }
        public double ExpectedDistance { get; set; }
        public double StandardDeviation { get; set; }
        public double ZStatistic { get; set; }
        public bool IsPassing { get; set; }
    }
}
