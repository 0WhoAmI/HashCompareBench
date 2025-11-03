namespace HashCompareBench.Models
{
    public class BitPredictionTestResult
    {
        public string AlgorithmName { get; set; }
        public double[] BitProbabilities { get; set; }
        public double[] ZStatistics { get; set; }
        public bool IsPassing { get; set; } // True, jeśli wszystkie bity przeszły test
    }
}
