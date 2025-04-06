using System.Numerics;

namespace Fibonacci;

public interface IFibonacciService
{
    public FibonacciResponseModel ParseFibonacciModel(FibonacciModel model);
}