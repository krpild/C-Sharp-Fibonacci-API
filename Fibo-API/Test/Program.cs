using System.Diagnostics;
using System.Numerics;
using Fibonacci;

namespace Test;

public class Test
{
    static void Main(String[] args)
    {
        
        Fibonacci.Fibonacci fibo = new Fibonacci.Fibonacci();
        Stopwatch sw = new Stopwatch();

        sw.Start();
        var result = fibo.GenerateFibonacciArray<int>(0, 7);

        sw.Stop();

        for (int i = 0; i < result.Length; i++)
        {
            Console.WriteLine(result[i]);
        }

        Console.WriteLine("Elapsed={0}",sw.Elapsed);
    } 
}