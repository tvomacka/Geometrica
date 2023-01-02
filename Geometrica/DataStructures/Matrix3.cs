namespace Geometrica.DataStructures;

public class Matrix3
{
    private double[,] matrix;

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

    public double this[int row, int column]
    {
        get => matrix[row, column];
        set => matrix[row, column] = value;
    }

    public override string ToString()
    {
        return $"[{matrix[0, 0]};{matrix[0, 1]};{matrix[0, 2]}][{matrix[1, 0]};{matrix[1, 1]};{matrix[1, 2]}][{matrix[2, 0]};{matrix[2, 1]};{matrix[2, 2]}]";
    }
}
