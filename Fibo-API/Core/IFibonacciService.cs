using System.Numerics;

namespace Fibonacci;

public interface IFibonacciService
{
    public Task<List<BigInteger>> GetFibonacciRangeCached(int start, int end);
}