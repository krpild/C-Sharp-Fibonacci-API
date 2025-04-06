using System.Numerics;

namespace Fibonacci;

//Algorithm inspired by: https://www.oranlooney.com/post/fibonacci/

public class Fibonacci 
{
    private static readonly BigInteger[] IdentityMatrix = {
        BigInteger.One, BigInteger.Zero, 
        BigInteger.Zero, BigInteger.One};

    private static readonly BigInteger[] QMatrix = { BigInteger.One, BigInteger.One, BigInteger.One, BigInteger.Zero };

    private BigInteger[] MultiplyMatrices(BigInteger[] fMatrix, BigInteger[] qMatrix)
    {
        return new []
        {
            fMatrix[0] * qMatrix[0] + fMatrix[1] * qMatrix[2],
            fMatrix[0] * qMatrix[1] + fMatrix[1] * qMatrix[3],
            fMatrix[2] * qMatrix[0] + fMatrix[3] * qMatrix[2],
            fMatrix[2] * qMatrix[1] + fMatrix[3] * qMatrix[3]
        };
    }

    private BigInteger[] MatrixPower(BigInteger[] matrix, int n)
    {
        BigInteger[] result = IdentityMatrix;
        while (n != 0)
        {
            if (n % 2 != 0) result = MultiplyMatrices(result, matrix);
            n /= 2;
            matrix = MultiplyMatrices(matrix, matrix);
        }
        return result;
    }

    public BigInteger CalculateFibonacci(int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index), index, "Fibonacci sequence does not have a negative index.");

        return MatrixPower(QMatrix, index)[1];
    }
    
}