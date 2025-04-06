using System.Numerics;

namespace Fibonacci;

public interface IFibonacciService
{
    public Task<BigInteger[]> GetFibonacciRangeCached(int start, int end);
}