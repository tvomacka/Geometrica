using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometrica.DataStructures
{
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

        public override string ToString()
        {
            return $"[{matrix[0, 0]};{matrix[0, 1]};{matrix[0, 2]}][{matrix[1, 0]};{matrix[1, 1]};{matrix[1, 2]}][{matrix[2, 0]};{matrix[2, 1]};{matrix[2, 2]}]";
        }
    }
}
