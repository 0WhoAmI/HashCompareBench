using HashCompareBench.Interfaces;
using System.Security.Cryptography;

namespace HashCompareBench.Algorithms
{
    public class Sha2Hash : IHashFunction
    {
        public string Name => "SHA2 (SHA-256)";
        public int HashSizeInBits => 256;

        public byte[] ComputeHash(byte[] data)
        {
            return SHA256.HashData(data);
        }
    }
}
