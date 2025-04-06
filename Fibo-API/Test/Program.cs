using System.Diagnostics;
using Core;

namespace Test;

public class Test
{
    static void Main(String[] args)
    {
        Fibonacci fibonacci = new Fibonacci();
        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i <= 20000; i++)
        {
            fibonacci.CalculateFibonacci(i);
        }
        sw.Stop();
        Console.WriteLine("Milliseconds spent: " + sw.ElapsedMilliseconds);
        
    } 
}