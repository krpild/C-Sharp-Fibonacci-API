using System.Numerics;

namespace Core;

public interface IFibonacciService
{
    public Task<FibonacciResponseModel> ParseFibonacciModel(FibonacciRequestModel requestModel);
}