using Fibonacci;

namespace Test;

public class Test
{
    static void Main(String[] args)
    {
        
        FibonacciCalculation fibo = new FibonacciCalculation();
        
        for (int i = 0; i <= 100; i++)
        {
            Console.WriteLine(fibo.Fibonacci(i));
        }
    } 
}