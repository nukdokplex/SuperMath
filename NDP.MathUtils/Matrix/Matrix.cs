using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDP.MathUtils
{
    class Matrix
    {
        public List<List<object>> Elements;

        public Matrix(int rows, int cols)
        {
            Elements = new List<List<object>>();
        }
    }
}
