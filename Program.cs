using HashCompareBench.Algorithms;
using HashCompareBench.HashFunctions;
using HashCompareBench.Interfaces;
using HashCompareBench.Models;
using HashCompareBench.Tests;
using HashCompareBench.Utils;

namespace HashCompareBench
{
    internal class Program
    {
        private const int SAMPLE_SIZE = 20000;
        private const int MESSAGE_SIZE_BYTES = 1024; // 1KB
        private const string OUTPUT_DIR = "TestResults";

        static void Main(string[] args)
        {
            Console.WriteLine($"Rozpoczynanie testów funkcji skrótu dla {SAMPLE_SIZE} próbek...");
            Directory.CreateDirectory(OUTPUT_DIR);

            var hashFunctions = new List<IHashFunction>
            {
                new Sha2Hash(),
                new Sha3Hash(),
                new AsconHash()
            };

            var hdResults = new List<HammingDistanceTestResult>();
            var bpResults = new List<BitPredictionTestResult>();
            var rtResults = new List<RunsTestResult>();

            foreach (var func in hashFunctions)
            {
                Console.WriteLine($"\n--- Testowanie: {func.Name} (Rozmiar skrótu: {func.HashSizeInBits} bitów) ---");

                Console.WriteLine("Uruchamianie testu odległości Hamminga...");
                hdResults.Add(HammingDistanceTest.RunTest(func, SAMPLE_SIZE, MESSAGE_SIZE_BYTES));

                Console.WriteLine("Uruchamianie testu predykcji bitów...");
                bpResults.Add(BitPredictionTest.RunTest(func, SAMPLE_SIZE, MESSAGE_SIZE_BYTES));

                Console.WriteLine("Uruchamianie testu serii...");
                rtResults.Add(RunsTest.RunTest(func, SAMPLE_SIZE, MESSAGE_SIZE_BYTES));
            }

            Console.WriteLine("\n--- Zakończono testy. Zapisywanie wyników... ---");

            var exporter = new ResultExporter(OUTPUT_DIR);
            exporter.SaveHammingDetails(hdResults, "1_hamming_details_per_sample.csv");
            exporter.SaveBitPredictionDetails(bpResults, "2_bit_prediction_details_per_bit.csv");
            exporter.SaveHammingSummaryTable(hdResults, "3_summary_table_hamming.csv");
            exporter.SaveBitPredictionSummaryTable(bpResults, "4_summary_table_bit_prediction.csv");
            exporter.SaveRunsTestSummaryTable(rtResults, "5_summary_table_runs_test.csv");

            Console.WriteLine($"Gotowe! Wyniki zapisano w folderze '{OUTPUT_DIR}'.");
        }
    }
}
