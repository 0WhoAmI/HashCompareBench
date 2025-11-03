using HashCompareBench.Models;
using System.Text;

namespace HashCompareBench.Utils
{
    public class ResultExporter
    {
        private readonly string _outputDir;

        public ResultExporter(string outputDir)
        {
            _outputDir = outputDir;
            // Upewnijmy się, czy katalog istnieje
            Directory.CreateDirectory(_outputDir);
        }

        public void SaveHammingResults(List<HammingDistanceTestResult> results, string fileName)
        {
            string filePath = Path.Combine(_outputDir, fileName);
            var sb = new StringBuilder();
            sb.AppendLine("Algorithm,MeanDistance,ExpectedDistance,StdDev,ZStatistic,IsPassing");

            foreach (var res in results)
            {
                sb.AppendLine($"{res.AlgorithmName},{res.MeanDistance},{res.ExpectedDistance},{res.StandardDeviation},{res.ZStatistic},{res.IsPassing}");
            }

            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine($"Zapisano: {filePath}");
        }

        public void SaveBitPredictionResults(List<BitPredictionTestResult> results, string fileName)
        {
            string filePath = Path.Combine(_outputDir, fileName);
            var sb = new StringBuilder();
            sb.AppendLine("Algorithm,BitPosition,ProbabilityOfOne,ZStatistic");

            foreach (var res in results)
            {
                for (int i = 0; i < res.BitProbabilities.Length; i++)
                {
                    sb.AppendLine($"{res.AlgorithmName},{i},{res.BitProbabilities[i]},{res.ZStatistics[i]}");
                }
            }

            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine($"Zapisano: {filePath}");
        }

        public void SaveRunsTestSummary(List<RunsTestResult> results, string fileName)
        {
            string filePath = Path.Combine(_outputDir, fileName);
            var sb = new StringBuilder();
            sb.AppendLine("Algorithm,PercentageOfHashesPassing");

            foreach (var res in results)
            {
                sb.AppendLine($"{res.AlgorithmName},{res.PercentageOfHashesPassing}");
            }

            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine($"Zapisano: {filePath}");
        }

        public void SaveRunsTestDetails(List<RunsTestResult> results, string fileName)
        {
            string filePath = Path.Combine(_outputDir, fileName);
            var sb = new StringBuilder();
            sb.AppendLine("Algorithm,ZStatistic");

            foreach (var res in results)
            {
                foreach (var z in res.IndividualZStatistics)
                {
                    sb.AppendLine($"{res.AlgorithmName},{z}");
                }
            }

            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine($"Zapisano: {filePath}");
        }
    }
}
