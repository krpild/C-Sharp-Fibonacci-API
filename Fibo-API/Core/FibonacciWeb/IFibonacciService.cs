using System.Numerics;

namespace Fibonacci;

public interface IFibonacciService
{
    public Task<FibonacciResponseModel> ParseFibonacciModel(FibonacciRequestModel requestModel);
}