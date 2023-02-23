namespace Geometrica.Algebra
{
    public class Matrix4
    {
        private readonly double[,] _matrix;

        public Matrix4()
        {
            _matrix = new double[4, 4];
            for (var i = 0; i < 4; i++)
            {
                for (var j = 0; j < 4; j++)
                {
                    _matrix[i, j] = 0;
                }
            }
        }

        public override string ToString()
        {
            var s = "";
            for (var i = 0; i < 4; i++)
            {
                s += $"[{_matrix[i, 0]};{_matrix[i, 1]};{_matrix[i, 2]};{_matrix[i, 3]}]";
            }

            return s;
        }
    }
}
