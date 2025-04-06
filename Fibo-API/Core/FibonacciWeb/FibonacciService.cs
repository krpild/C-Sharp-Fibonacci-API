using System.Numerics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Fibonacci;

public class FibonacciService : IFibonacciService
{
    private readonly IMemoryCache _cache;
    private readonly CacheSettings _settings;
    private Fibonacci _fibonacci = new Fibonacci();

    public FibonacciService(IMemoryCache cache, IOptions<CacheSettings> options)
    {
        _cache = cache;
        _settings = options.Value;
        
    }
    
    public async Task<BigInteger[]> GetFibonacciRange(int start, int end)
    {
        var tasks = new List<Task<BigInteger>>();

        for (int i = start; i <= end; i++)
        {
            int index = i;
            tasks.Add(Task.Run(() => CachedFibonacci(index)));
        }
        var results = await Task.WhenAll(tasks);
        
        return results;
    }

    private BigInteger CachedFibonacci(int index)
    {
        return _cache.GetOrCreate($"f_{index}", entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            return _fibonacci.CalculateFibonacci(index);
        });
    }

    public String[] ParseFibonacciModel(FibonacciModel model)
    {
        var result = GetFibonacciRange(Int32.Parse(model.Start), Int32.Parse(model.End)).Result;
        String[] items = new string[result.Length];
        
        for (int i = 0; i < result.Length; i++)
        {
            items[i] = result[i].ToString();
        }

        return items;
    }
    
}