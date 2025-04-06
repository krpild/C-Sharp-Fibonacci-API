using System.Numerics;
using System.Text.Json;

namespace Fibonacci;

public static class Util
{
    public static bool IsModelValid(FibonacciRequestModel requestModel)
    {
        try
        {
            int start = Int32.Parse(requestModel.Start);
            int end = Int32.Parse(requestModel.End);
            
            if (!IsValidFibonacciIndex(start) ||
                !IsValidFibonacciIndex(end) ||
                !IsEndLargerThanStart(start, end)) return false;
            
            if (requestModel.MaxMemory is <= 0) return false;
            
            if (requestModel.MaxTime is <= 0) return false;

        }
        catch (Exception e)
        {
            //The service shouldn't stop working, so the goal is for the controller to say Bad Request.
            Console.WriteLine(e);
            return false;
        }
        return true;
    }
    
    private static bool IsValidFibonacciIndex(int value)
    {
        return value is >= 0 and <= 20000;
    }

    private static bool IsEndLargerThanStart(int start, int end)
    {
        return start <= end;
    }
}