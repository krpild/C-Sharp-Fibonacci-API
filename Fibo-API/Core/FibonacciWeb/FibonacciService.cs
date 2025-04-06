using System.Diagnostics;
using System.Numerics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Core;

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
    
    public async Task<FibonacciResponseModel> GetFibonacciRange(FibonacciRequestModel model)
    {
        int start = model.Start;
        int end = model.End;
        var tasks = new List<Task<BigInteger>>();
        FibonacciResponseModel response = new FibonacciResponseModel();
        var results = new BigInteger[end - start + 1];
        response.Sequence = new List<string>();
        
        Stopwatch sw = new Stopwatch();
        sw.Start();
        var first = await Task.Run(async () =>
        {
            
            if (model.Cache is null or true) return CachedFibonacci(start);
            return _fibonacci.CalculateFibonacci(start);
        });
        await Task.Delay(500);
        sw.Stop();
        
        if (model.MaxTime != 0 && sw.ElapsedMilliseconds > model.MaxTime)
        {
            throw new TimeoutException("First Fibonacci number took too long.");
        }
        
        response.Sequence.Add(first.ToString());
        sw.Start();
        
        for (int i = start + 1; i <= end; i++)
        {
            int index = i;
            long currentMemory = GC.GetTotalMemory(false);
            if (currentMemory > model.MaxMemory && model.MaxMemory != null)
            {
                
                AddError(response, "Program ran out of memory");
                break;
            }
            
            sw.Stop();
            if (model.MaxTime != null && sw.ElapsedMilliseconds > model.MaxTime)
            {
                
                AddError(response, "Program ran out of time");
                break;
            }
            sw.Start();

            tasks.Add(Task.Run(async () => {
                await Task.Delay(500);
                if (model.Cache is null or true) return CachedFibonacci(index);
                return _fibonacci.CalculateFibonacci(index);
                
            }));
            
            
        }
        results = await Task.WhenAll(tasks);
            
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

    public async Task<FibonacciResponseModel> ParseFibonacciModel(FibonacciRequestModel requestModel)
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        return await Task.Run(() => GetFibonacciRange(requestModel));

    }
    
    private void AddError(FibonacciResponseModel response, string message)
    {
        response.Errors ??= new List<string>();
        response.Errors.Add(message);
    }
}