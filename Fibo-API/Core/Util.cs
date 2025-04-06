namespace Fibonacci;

public static class Util
{
    public static bool IsValidFibonacciIndex(this int value)
    {
        return value is >= 0 and <= 10000;
    }

    public static bool IsEndLargerThanStart(this int start, int end)
    {
        return start <= end;
    }
    
    
}