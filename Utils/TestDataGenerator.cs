using System.Security.Cryptography;

namespace HashCompareBench.Utils
{
    public static class TestDataGenerator
    {
        private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();
        private static readonly Random _random = new Random();

        /// <summary>
        /// Generuje listę losowych wiadomości.
        /// </summary>
        public static List<byte[]> GenerateRandomMessages(int count, int messageSizeInBytes)
        {
            var messages = new List<byte[]>(count);
            for (int i = 0; i < count; i++)
            {
                byte[] message = new byte[messageSizeInBytes];
                _rng.GetBytes(message);
                messages.Add(message);
            }
            return messages;
        }

        /// <summary>
        /// Generuje pary wiadomości różniące się jednym bitem na losowej pozycji.
        /// </summary>
        public static List<(byte[] Message1, byte[] Message2)> GenerateSimilarMessagePairs(int count, int messageSizeInBytes)
        {
            var pairs = new List<(byte[], byte[])>(count);
            for (int i = 0; i < count; i++)
            {
                byte[] message1 = new byte[messageSizeInBytes];
                _rng.GetBytes(message1);

                byte[] message2 = new byte[messageSizeInBytes];
                Array.Copy(message1, message2, message1.Length);

                // Losowy bit do zmiany
                int randomByteIndex = _random.Next(0, messageSizeInBytes);
                int randomBitIndex = _random.Next(0, 8); // 0-7

                // Odwracamy bit za pomocą operatora XOR
                message2[randomByteIndex] = (byte)(message2[randomByteIndex] ^ (1 << randomBitIndex));

                pairs.Add((message1, message2));
            }
            return pairs;
        }
    }
}
