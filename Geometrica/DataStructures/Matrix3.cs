namespace Geometrica.DataStructures;

public readonly struct Matrix3
{
    private readonly double[,] matrix;

    public Matrix3()
    {
        matrix = new double[3, 3];
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                matrix[i, j] = 0;
            }
        }
    }

    public Matrix3(double m00, double m01, double m02, double m10, double m11, double m12, double m20, double m21, double m22)
    {
        matrix = new double[3, 3];
        matrix[0, 0] = m00;
        matrix[0, 1] = m01;
        matrix[0, 2] = m02;
        matrix[1, 0] = m10;
        matrix[1, 1] = m11;
        matrix[1, 2] = m12;
        matrix[2, 0] = m20;
        matrix[2, 1] = m21;
        matrix[2, 2] = m22;
    }

    public double this[int row, int column]
    {
        get => matrix[row, column];
        set => matrix[row, column] = value;
    }

    public static Matrix3 Identity 
    { 
        get => new(1, 0, 0, 0, 1, 0, 0, 0, 1); 
    }

    public double Determinant()
    {
        return matrix[0, 0] * matrix[1, 1] * matrix[2, 2] +
            matrix[0, 1] * matrix[1, 2] * matrix[2, 0] +
            matrix[0, 2] * matrix[1, 0] * matrix[2, 1] -
            matrix[0, 2] * matrix[1, 1] * matrix[2, 0] -
            matrix[0, 1] * matrix[1, 0] * matrix[2, 2] -
            matrix[0, 0] * matrix[1, 2] * matrix[2, 1];
    }

    public override string ToString()
    {
        return $"[{matrix[0, 0]};{matrix[0, 1]};{matrix[0, 2]}][{matrix[1, 0]};{matrix[1, 1]};{matrix[1, 2]}][{matrix[2, 0]};{matrix[2, 1]};{matrix[2, 2]}]";
    }

    public Matrix3 Transpose()
    {
        return new Matrix3(this[0,0], this[1,0], this[2,0], this[0,1], this[1,1], this[2,1], this[0,2], this[1,2], this[2,2]);
    }
}
