using HashCompareBench.Interfaces;
using System.Security.Cryptography;

namespace HashCompareBench.Algorithms
{
    public class Sha3Hash : IHashFunction
    {
        public string Name => "SHA3 (SHA3-256)";
        public int HashSizeInBits => 256;

        public byte[] ComputeHash(byte[] data)
        {
            return SHA3_256.HashData(data);
        }
    }
}
