namespace HashCompareBench.Interfaces
{
    public interface IHashFunction
    {
        string Name { get; }
        int HashSizeInBits { get; }
        byte[] ComputeHash(byte[] data);
    }
}
