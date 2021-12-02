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

        public Matrix(int rows, int columns, params EitherNumber[][] elements)
        {
            Elements = new List<List<EitherNumber>>();
            if (elements.GetLength(0) != rows) throw new ArgumentOutOfRangeException();
            for (int i = 0; i < rows; i++)
            {
                if (elements[i].Length != columns) throw new ArgumentOutOfRangeException();
                Elements.Add(new List<EitherNumber>());
                for (int j = 0; j < columns; j++)
                {
                    Elements[i].Add(elements[i][j]);
                }
            }
        }

        public Matrix(params Vector[] rows)
        {
            Elements = new List<List<EitherNumber>>();

            int columns = rows[0].Dimension;

            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i].Dimension != columns) throw new ArgumentOutOfRangeException("All vectors must be same dimesion.");
                Elements.Add(new List<EitherNumber>());
                for (int j = 0; j < columns; j++)
                {
                    Elements[i].Add(rows[i][j]);
                }
            }
        }

        public int GetRowCount()
        {
            return Elements.Count;
        }

        public int GetColumnCount()
        {
            if (Elements.Count == 0) return 0;
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

        public Vector GetRowAsVector(int row)
        {
            return new Vector(GetRow(row).ToArray());
        }

        public Vector GetColumnAsVector(int column)
        {
            return new Vector(GetColumn(column).ToArray());
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

        public static Matrix operator +(Matrix a, Matrix b) 
        {
            if (a.GetRowCount() == b.GetRowCount() && a.GetColumnCount() == b.GetColumnCount())
            {
                Matrix matrix = a.Clone();
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

        public static Matrix operator +(Matrix a, EitherNumber n)
        {
            Matrix matrix = a.Clone();
            for (int i = 0; i < matrix.GetRowCount(); i++)
            {
                for (int j = 0; j < matrix.GetColumnCount(); j++)
                {
                    matrix[i, j] += n;
                }
            }

            return matrix;
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
                        r[i, j] = a.GetRowAsVector(i) * b.GetColumnAsVector(j);
                    }
                }

                return r;
            }

            throw new InvalidOperationException("Count of columns in left matrix must be equal to count of rows in right matrix.");
        }

        public static Matrix operator *(Matrix a, EitherNumber n)
        {
            var r = a.Clone();
            for (int i = 0; i < r.GetRowCount(); i++)
            {
                for (int j = 0; j < r.GetColumnCount(); j++)
                {
                    r[i, j] *= n;
                }
            }
            return r;
        }

        public static Matrix operator *(EitherNumber n, Matrix a) => a * n;

        public static Matrix operator /(Matrix a, EitherNumber n)
        {
            for (int i = 0; i < a.GetRowCount(); i++)
            {
                for (int j = 0; j < a.GetColumnCount(); j++)
                {
                    a[i, j] /= n;
                }
            }

            return a;
        }

        public void DeleteRow(int row) => Elements.RemoveAt(row);

        public void DeleteColumn(int column)
        {
            for (int i = 0; i < GetRowCount(); i++)
            {
                Elements[i].RemoveAt(column);
            }
        }

        public Matrix Minor(int row, int column)
        {
            if (GetColumnCount() == 1 && GetRowCount() == 1) return new Matrix(0);
            if (GetColumnCount() == 0 || GetRowCount() == 0) throw new InvalidOperationException("Can't get minor from empty matrix.");

            Matrix matrix = Clone();
            matrix.DeleteRow(row);
            matrix.DeleteColumn(column);

            return matrix;            
        }

        public Matrix AlgrebraicComplements()
        {
            var m = new Matrix(GetRowCount(), GetColumnCount());
            for (int i = 0; i < GetRowCount(); i++)
            {
                for (int j = 0; j < GetColumnCount(); j++)
                {
                    m[i, j] = AlgebraicComplement(i, j);
                }
            }
            return m;
        }

        public EitherNumber AlgebraicComplement(int row, int column)
        {
            return ((row + column + 2) % 2 == 0 ? 1 : -1) * Minor(row, column).Determinator();
        }

        public Matrix Invertible()
        {
            EitherNumber det = Determinator();
            if (det.Real() == 0.0f) throw new InvalidOperationException("Determinator of matrix is zero, so invertible matrix doesn't exist.");
            return Transpond().AlgrebraicComplements() / det;

        }

        public EitherNumber Determinator()
        {
            if (GetColumnCount() != GetRowCount()) throw new InvalidOperationException("Can't calculate determinator of non-square matrix.");
            if (GetRowCount() == 1)
            {
                return this[0, 0];
            }
            EitherNumber det = 0;

            for (int i = 0; i < GetRowCount(); i++)
            {
                det += ((i + 2) % 2 == 0 ? 1 : -1) * Minor(i, 1).Determinator() * this[i, 1];
            }

            return det;
        }
        
        public Matrix Clone()
        {
            var m = new Matrix(GetRowCount(), GetColumnCount());
            for (int i = 0; i < GetRowCount(); i++)
            {
                for (int j = 0; j < GetColumnCount(); j++)
                {
                    m[i, j] = this[i, j].Clone();
                }
            }
            return m;
        }
    }
}
