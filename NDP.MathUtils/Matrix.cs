using NDP.MathUtils.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDP.MathUtils
{
    public class Matrix
    {
        private List<List<EitherNumber>> Elements;

        //just for remember i - rows, j - columns

        public Matrix(int rows, int columns)
        {
            Elements = new List<List<EitherNumber>>();
            for (int i = 0; i < rows; i++)
            {
                Elements.Add(new List<EitherNumber>());
                for (int j = 0; j < columns; j++)
                {
                    Elements[i].Add(0);
                }
            }
        }

        public Matrix(int size)
        {
            Elements = new List<List<EitherNumber>>();
            for (int i = 0; i < size; i++)
            {
                Elements.Add(new List<EitherNumber>());
                for (int j = 0; j < size; j++)
                {
                    Elements[i].Add(0);
                }
            }
        }

        public int GetRowCount()
        {
            return Elements.Count;
        }

        public int GetColumnCount()
        {
            return Elements[0].Count;
        }

        public Matrix Transpond()
        {
            Matrix r = new Matrix(GetColumnCount(), GetRowCount());

            for (int i = 0; i < GetRowCount(); i++)
            {
                for (int j = 0; j < GetColumnCount(); j++)
                {
                    r[j, i] = this[i, j];
                }
            }

            return r;
        }

        public List<List<EitherNumber>> GetRows()
        {
            return Elements;
        }

        public List<List<EitherNumber>> GetColumns()
        {
            return Transpond().GetRows();
        }

        public List<EitherNumber> GetRow(int row)
        {
            return GetRows()[row];
        }

        public List<EitherNumber> GetColumn(int column)
        {
            return GetColumns()[column];
        }

        public EitherNumber this[int row, int column]
        {
            get 
            {
                return Elements[row][column];
            }
            set
            {
                Elements[row][column] = value;
            }
        }

        public EitherNumber GetElementAt(int row, int column)
        {
            return Elements[row][column];
        }

        #region Operators for Either3 Triade
            
        

        #endregion

        public static Matrix operator +(Matrix a, Matrix b) 
        {
            if (a.GetRowCount() == b.GetRowCount() && a.GetColumnCount() == b.GetColumnCount())
            {
                Matrix matrix = a.MemberwiseClone() as Matrix;
                for (int i = 0; i < matrix.GetRowCount(); i++)
                {
                    for (int j = 0; j < matrix.GetColumnCount(); j++)
                    {
                        matrix[i, j] += b[i, j];
                    }
                }

                return matrix;
            }

            throw new InvalidOperationException("Matrixes with different dimensions can't be summed.");
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.GetColumnCount() == b.GetRowCount())
            {
                Matrix r = new Matrix(a.GetRowCount(), b.GetColumnCount());
                for (int i = 0; i < r.GetRowCount(); i++)
                {
                    for (int j = 0; j < r.GetColumnCount(); j++)
                    {

                    }
                }
            }

            throw new InvalidOperationException("Count of columns in left matrix must be equal to count of rows in right matrix.");
        }


    }
}
