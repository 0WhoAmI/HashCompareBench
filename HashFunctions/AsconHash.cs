using HashCompareBench.Interfaces;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;

namespace HashCompareBench.HashFunctions
{
    public class AsconHash : IHashFunction
    {
        public string Name => "ASCON-Hash";
        public int HashSizeInBits => 256;

        public byte[] ComputeHash(byte[] data)
        {
            IDigest digest = new AsconHash256();
            byte[] output = new byte[digest.GetDigestSize()];
            digest.BlockUpdate(data, 0, data.Length);
            digest.DoFinal(output, 0);

            return output;
        }
    }
}
