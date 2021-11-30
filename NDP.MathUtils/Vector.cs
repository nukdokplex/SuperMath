using NDP.MathUtils.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDP.MathUtils
{
    public class Vector
    {
        private List<EitherNumber> Elements;

        private readonly int dimension = 0;

        public int Dimension { 
            get 
            {
                return dimension;
            }
            set
            {
                if (value == dimension) return;
                if (value < dimension) Elements.RemoveRange(value, dimension - value);
                if (value > dimension)
                {
                    for (int i = 0; i < value - dimension; i++)
                    {
                        Elements.Add(0);
                    }
                }
            }
        }

        public Vector(int elementsCount)
        {
            Elements = new List<EitherNumber>();

            dimension = elementsCount;

            for (int i = 0; i < elementsCount; i++)
            {
                Elements.Add(0);
            }
        }

        public Vector(params EitherNumber[] numbers)
        {
            Elements = new List<EitherNumber>(numbers);
            dimension = numbers.Count();
        }

        public EitherNumber this[int elementIndex]
        {
            get
            {
                return Elements[elementIndex];
            }
            set
            {
                Elements[elementIndex] = value;
            }
        }

        public float Determinator(Vector a)
        {
            float sum = 0.0f;

            for (int i = 0; i < Elements.Count(); i++)
            {
                sum += (float) Math.Pow(Elements[i].Real(), 2);
            }

            return (float) Math.Sqrt(sum);
        }

        public static EitherNumber operator *(Vector a, Vector b)
        {
            if (a.Dimension != b.Dimension) throw new InvalidOperationException("Vectors must be same dimension.");
            EitherNumber sum = 0;
            for (int i = 0; i < a.Dimension; i++)
            {
                sum += a[i] * b[i];
            }

            return sum;
        }

        
    }
}
