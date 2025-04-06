using System.Numerics;
using System.Text.Json;

namespace Core;

public static class Util
{
    public static bool IsModelValid(FibonacciRequestModel requestModel)
    {
        
        if (!IsValidFibonacciIndex(requestModel.Start) ||
            !IsValidFibonacciIndex(requestModel.End) ||
            !IsEndLargerThanStart(requestModel.Start, requestModel.End)) return false;
        
        if (requestModel.MaxMemory is <= 0) return false;
        
        if (requestModel.MaxTime is <= 0) return false;

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