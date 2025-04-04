using System.Numerics;

namespace Fibonacci;

//Algorithm inspired by: https://www.oranlooney.com/post/fibonacci/

public class Fibonacci
{
    private static readonly byte[] IdentityMatrix = { 1, 0, 0, 1};

    private static readonly byte[] QMatrix = { 1, 1, 1, 0 };
    
    private static T[] ConvertByteArrayTo<T>(byte[] source) where T : INumber<T>
    {
        var result = new T[4];
        for (int i = 0; i < 4; i++)
        {
            result[i] = T.CreateChecked(source[i]);
        }
        return result;
    }

    private T[] MultiplyMatrices<T>(T[] fMatrix, T[] qMatrix) where T : INumber<T>
    {
        return new T[]
        {
            fMatrix[0] * qMatrix[0] + fMatrix[1] * qMatrix[2],
            fMatrix[0] * qMatrix[1] + fMatrix[1] * qMatrix[3],
            fMatrix[2] * qMatrix[0] + fMatrix[3] * qMatrix[2],
            fMatrix[2] * qMatrix[1] + fMatrix[3] * qMatrix[3]
        };
    }

    private T[] MatrixPower<T>(T[] matrix, int n) where T : INumber<T>
    {
        T[] result = ConvertByteArrayTo<T>(IdentityMatrix);
        while (n != 0)
        {
            if (n % 2 != 0) result = MultiplyMatrices(result, matrix);
            n /= 2;
            matrix = MultiplyMatrices(matrix, matrix);
        }
        return result;
    }

    public T CalculateFibonacci<T>(int index) where T : INumber<T>
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index), index, "Fibonacci sequence does not have a negative index.");

        return MatrixPower(ConvertByteArrayTo<T>(QMatrix), index)[1];
    }

    public T[] GenerateFibonacciArray<T>(int start, int end) where T : INumber<T>
    {
        var result = new T[end - start + 1];
        for (int i = 0; i <= end - start; i++)
        {
            result[i] = CalculateFibonacci<T>(start + i);
        }
        return result;
    }
    
}