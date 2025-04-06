using System.Numerics;
using System.Text.Json;

namespace Fibonacci;

public static class Util
{
    public static bool IsModelValid(this FibonacciModel model)
    {
        try
        {
            int start = Int32.Parse(model.Start);
            int end = Int32.Parse(model.Start);
            
            if (!IsValidFibonacciIndex(start) ||
                !IsValidFibonacciIndex(end) ||
                !IsEndLargerThanStart(start, end)) return false;
            
            if (model.MaxMemory is <= 0) return false;
            
            if (model.MaxTime is <= 0) return false;

        }
        catch (Exception e)
        {
            //The service shouldn't stop working, so the goal is for the controller to say Bad Request.
            Console.WriteLine(e);
            return false;
        }
        return true;
    }
    
    private static bool IsValidFibonacciIndex(this int value)
    {
        return value is >= 0 and <= 10000;
    }

    private static bool IsEndLargerThanStart(this int start, int end)
    {
        return start <= end;
    }
}