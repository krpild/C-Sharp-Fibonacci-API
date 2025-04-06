using System.Diagnostics;
using System.Numerics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Fibonacci;

public class FibonacciService : IFibonacciService
{
    private readonly IMemoryCache _cache;
    private readonly CacheSettings _settings;
    private readonly Fibonacci _fibonacci = new Fibonacci();

    public FibonacciService(IMemoryCache cache, IOptions<CacheSettings> options)
    {
        _cache = cache;
        _settings = options.Value;
        
    }
    
    public async Task<FibonacciResponseModel> GetFibonacciRange(int start, int end)
    {
        var tasks = new List<Task<BigInteger>>();
        FibonacciResponseModel response = new FibonacciResponseModel();
        response.Sequence = new List<string>();
        
        
        for (int i = start; i <= end; i++)
        {
            int index = i;
            long currentMemory = GC.GetTotalMemory(false);

            tasks.Add(Task.Run(() => CachedFibonacci(index)));
            
            if (currentMemory > 1024 * 1024 * 5)
            {
                if (response.Errors == null)
                {
                    response.Errors = new List<string>();
                }
                response.Errors.Add("Program ran out of memory");
                break;
            }
            
        }
        var results = await Task.WhenAll(tasks);
            
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i] != null) // The compiler is LYING!! The expression isn't true when the program runs out of memory.
            {
                response.Sequence.Add(results[i].ToString());
            }
            
        }
        
        return response;
    }

    private BigInteger CachedFibonacci(int index)
    {
        return _cache.GetOrCreate($"f_{index}", entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            return _fibonacci.CalculateFibonacci(index);
        });
    }

    public FibonacciResponseModel ParseFibonacciModel(FibonacciModel model)
    {
        return GetFibonacciRange(Int32.Parse(model.Start), Int32.Parse(model.End)).Result;

    }
    
}