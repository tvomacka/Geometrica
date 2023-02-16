namespace Geometrica.Algebra;

public readonly struct Matrix3
{
    private readonly double[,] _matrix;

    public Matrix3()
    {
        _matrix = new double[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                _matrix[i, j] = 0;
            }
        }
    }

    public Matrix3(double m00, double m01, double m02, double m10, double m11, double m12, double m20, double m21, double m22)
    {
        _matrix = new double[3, 3];
        _matrix[0, 0] = m00;
        _matrix[0, 1] = m01;
        _matrix[0, 2] = m02;
        _matrix[1, 0] = m10;
        _matrix[1, 1] = m11;
        _matrix[1, 2] = m12;
        _matrix[2, 0] = m20;
        _matrix[2, 1] = m21;
        _matrix[2, 2] = m22;
    }

    public double this[int row, int column]
    {
        get => _matrix[row, column];
        set => _matrix[row, column] = value;
    }

    public static Matrix3 Identity
    {
        get => new(1, 0, 0, 0, 1, 0, 0, 0, 1);
    }

    public double Determinant()
    {
        return _matrix[0, 0] * _matrix[1, 1] * _matrix[2, 2] +
            _matrix[0, 1] * _matrix[1, 2] * _matrix[2, 0] +
            _matrix[0, 2] * _matrix[1, 0] * _matrix[2, 1] -
            _matrix[0, 2] * _matrix[1, 1] * _matrix[2, 0] -
            _matrix[0, 1] * _matrix[1, 0] * _matrix[2, 2] -
            _matrix[0, 0] * _matrix[1, 2] * _matrix[2, 1];
    }

    public override string ToString()
    {
        return $"[{_matrix[0, 0]};{_matrix[0, 1]};{_matrix[0, 2]}][{_matrix[1, 0]};{_matrix[1, 1]};{_matrix[1, 2]}][{_matrix[2, 0]};{_matrix[2, 1]};{_matrix[2, 2]}]";
    }

    public Matrix3 Transpose()
    {
        return new Matrix3(this[0, 0], this[1, 0], this[2, 0], this[0, 1], this[1, 1], this[2, 1], this[0, 2], this[1, 2], this[2, 2]);
    }
}
