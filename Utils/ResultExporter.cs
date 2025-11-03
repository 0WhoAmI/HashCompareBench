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

        // --- PLIK 1: Szczegółowe "kropki" dla Testu Hamminga ---
        public void SaveHammingDetails(List<HammingDistanceTestResult> results, string fileName)
        {
            string filePath = Path.Combine(_outputDir, fileName);
            var sb = new StringBuilder();
            sb.AppendLine("Algorithm,SampleIndex,HammingDistance");

            foreach (var res in results)
            {
                for (int i = 0; i < res.IndividualDistances.Count; i++)
                {
                    sb.AppendLine($"{res.AlgorithmName},{i},{res.IndividualDistances[i]}");
                }
            }

            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine($"Zapisano: {filePath}");
        }

        // --- PLIK 2: Szczegółowe "kropki" dla Testu Predykcji Bitów ---
        public void SaveBitPredictionDetails(List<BitPredictionTestResult> results, string fileName)
        {
            string filePath = Path.Combine(_outputDir, fileName);
            var sb = new StringBuilder();
            sb.AppendLine("Algorithm,BitPosition,ZStatistic,ProbabilityOfOne");

            foreach (var res in results)
            {
                for (int i = 0; i < res.BitProbabilities.Length; i++)
                {
                    sb.AppendLine($"{res.AlgorithmName},{i},{res.ZStatistics[i]},{res.BitProbabilities[i]}");
                }
            }

            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine($"Zapisano: {filePath}");
        }

        // --- PLIK 3: Tabela porównawcza dla Testu Hamminga ---
        public void SaveHammingSummaryTable(List<HammingDistanceTestResult> results, string fileName)
        {
            string filePath = Path.Combine(_outputDir, fileName);
            var sb = new StringBuilder();
            sb.AppendLine("Function,Max,Min,Avg,SD");

            foreach (var res in results)
            {
                double max = res.IndividualDistances.Max();
                double min = res.IndividualDistances.Min();
                sb.AppendLine($"{res.AlgorithmName},{max},{min},{res.MeanDistance},{res.StandardDeviation}");
            }

            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine($"Zapisano: {filePath}");
        }

        // --- PLIK 4: Tabela porównawcza dla Testu Predykcji Bitów ---
        public void SaveBitPredictionSummaryTable(List<BitPredictionTestResult> results, string fileName)
        {
            string filePath = Path.Combine(_outputDir, fileName);
            var sb = new StringBuilder();
            sb.AppendLine("Function,Max,Min,Avg,SD");

            foreach (var res in results)
            {
                double max = res.ZStatistics.Max();
                double min = res.ZStatistics.Min();
                double avg = res.ZStatistics.Average();
                double sd = StatisticsHelper.CalculateStdDev(res.ZStatistics, avg);

                sb.AppendLine($"{res.AlgorithmName},{max},{min},{avg},{sd}");
            }

            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine($"Zapisano: {filePath}");
        }

        // --- PLIK 5: Tabela porównawcza dla Testu Serii ---
        public void SaveRunsTestSummaryTable(List<RunsTestResult> results, string fileName)
        {
            string filePath = Path.Combine(_outputDir, fileName);
            var sb = new StringBuilder();
            sb.AppendLine("Function,Max,Min,Avg,SD");

            foreach (var res in results)
            {
                double max = res.IndividualZStatistics.Max();
                double min = res.IndividualZStatistics.Min();
                double avg = res.IndividualZStatistics.Average();
                double sd = StatisticsHelper.CalculateStdDev(res.IndividualZStatistics, avg);

                sb.AppendLine($"{res.AlgorithmName},{max},{min},{avg},{sd}");
            }

            File.WriteAllText(filePath, sb.ToString());
            Console.WriteLine($"Zapisano: {filePath}");
        }
    }
}
