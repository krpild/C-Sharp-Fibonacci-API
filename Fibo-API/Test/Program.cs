using System.Diagnostics;
using Fibonacci;

namespace Test;

public class Test
{
    static void Main(String[] args)
    {
        
        FibonacciCalculation fibo = new FibonacciCalculation();
        Stopwatch sw = new Stopwatch();

        sw.Start();
        for (int i = 0; i <= 10000; i++)
        {
            fibo.Fibonacci(i);
        }

        sw.Stop();

        Console.WriteLine("Elapsed={0}",sw.Elapsed);
    } 
}