namespace HashCompareBench.Models
{
    public class RunsTestResult
    {
        public string AlgorithmName { get; set; }
        public double[] IndividualZStatistics { get; set; }
        public double PercentageOfHashesPassing { get; set; }
    }
}
