using System;

namespace NDP.MathUtils
{
    public class CommonFraction
    {
        public int Numerator;
        public int Denominator;

        public bool IsInteger { 
            get {
                return Numerator % Denominator == 0;
            } 
        }

        public float RealValue
        {
            get
            {
                return Numerator / Denominator;
            }
        }

        public CommonFraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public bool IsNegative()
        {
            return (Numerator < 0 ? 0 : 1) + (Denominator < 0 ? 0 : 1) == 1;
        }

        public void Minimize()
        {
            int gcf = NDP.MathUtils.MathUtils.GreatestCommonFactor(Numerator, Denominator);
            Numerator /= gcf;
            Denominator /= gcf;
            // Division of numerator and denominator by one number cause no changes in fraction
            // For this we calculate greatest common factor to make the best possible minimization
            if (Numerator < 0 && Denominator < 0 || Denominator < 0)
            {
                Numerator = -Numerator;
                Denominator = -Denominator;
            }
            // This strategy is to collapse two negatiations or move it to numerator
        }

        public CommonFraction Minimized()
        {
            CommonFraction r = this;
            r.Minimize();
            return r;
        }

        #region Operators

        public static CommonFraction operator +(CommonFraction a, CommonFraction b)
        {
            CommonFraction fraction = new CommonFraction(1, 1);
            fraction.Numerator = (
                a.Numerator * b.Denominator +
                b.Numerator * a.Denominator
            );
            fraction.Denominator = (
                a.Denominator * b.Denominator
            );
            return fraction;
        }

        public static CommonFraction operator +(int a, CommonFraction b)
        {
            CommonFraction fraction = new CommonFraction(b.Numerator, b.Denominator);
            fraction.Numerator += a * fraction.Denominator;
            return fraction;
        }

        public static CommonFraction operator +(CommonFraction a, int b)
        {
            CommonFraction fraction = a.MemberwiseClone() as CommonFraction;
            fraction.Numerator += b * fraction.Denominator;
            return fraction;
        }

        public static CommonFraction operator -(CommonFraction a, CommonFraction b)
        {
            CommonFraction fraction = b.MemberwiseClone() as CommonFraction;

            if (fraction.Numerator < 0 && fraction.Denominator < 0 || fraction.Denominator < 0)
            {
                fraction.Numerator = -fraction.Numerator;
                fraction.Denominator = -fraction.Denominator;
            }
            fraction.Numerator = -fraction.Numerator;

            return a + fraction;
        }

        public static CommonFraction operator -(CommonFraction a, int b)
        {
            return a + -b;
        }

        public static CommonFraction operator -(int a, CommonFraction b)
        {
            CommonFraction fraction = b.MemberwiseClone() as CommonFraction;

            if (fraction.Numerator < 0 && fraction.Denominator < 0 || fraction.Denominator < 0)
            {
                fraction.Numerator = -fraction.Numerator;
                fraction.Denominator = -fraction.Denominator;
            }
            fraction.Numerator = -fraction.Numerator;

            return a + fraction;
        }

        public static CommonFraction operator -(CommonFraction a)
        {
            CommonFraction fraction = a.MemberwiseClone() as CommonFraction;

            if (fraction.Numerator < 0 && fraction.Denominator < 0 || fraction.Denominator < 0)
            {
                fraction.Numerator = -fraction.Numerator;
                fraction.Denominator = -fraction.Denominator;
            }
            fraction.Numerator = -fraction.Numerator;

            return fraction;
        }

        public static CommonFraction operator *(CommonFraction a, CommonFraction b)
        {
            CommonFraction fraction = new CommonFraction(1, 1);
            fraction.Numerator = a.Numerator * b.Numerator;
            fraction.Denominator = a.Denominator * b.Denominator;
            return fraction;
        }

        public static CommonFraction operator *(int a, CommonFraction b)
        {
            CommonFraction fraction = b.MemberwiseClone() as CommonFraction;
            fraction.Numerator *= a;
            return fraction;
        }

        public static CommonFraction operator *(CommonFraction a, int b)
        {
            CommonFraction fraction = a.MemberwiseClone() as CommonFraction;
            fraction.Numerator *= b;
            return fraction;
        }

        public static CommonFraction operator /(CommonFraction a, CommonFraction b)
        {
            CommonFraction fraction = a.MemberwiseClone() as CommonFraction;
            fraction.Numerator *= b.Denominator;
            fraction.Denominator *= b.Numerator; // Butterfly!
            return fraction;
        }

        public static CommonFraction operator /(CommonFraction a, int b)
        {
            CommonFraction fraction = a.MemberwiseClone() as CommonFraction;
            fraction.Denominator *= b;
            return fraction;
        }

        public static CommonFraction operator /(int a, CommonFraction b)
        {
            CommonFraction fraction = new CommonFraction(a, 1);
            return fraction / b;
        }

        

        public static bool operator !=(CommonFraction a, CommonFraction b) => !a.Equals(b);
        public static bool operator ==(CommonFraction a, CommonFraction b) => a.Equals(b);

        public static bool operator !=(CommonFraction a, float b) => a.RealValue != b;

        public static bool operator ==(CommonFraction a, float b) => a.RealValue == b;

        public static bool operator !=(float a, CommonFraction b) => b != a;

        public static bool operator ==(float a, CommonFraction b) => b != a;

        public static bool operator !=(CommonFraction a, int b) => !a.IsInteger || (a.Minimized().Numerator != b);

        public static bool operator ==(CommonFraction a, int b) => a.IsInteger && (a.Minimized().Numerator == b);

        public static bool operator !=(int a, CommonFraction b) => b != a;

        public static bool operator ==(int a, CommonFraction b) => b == a;
        #endregion

        public override string ToString()
        {
            return $"({Numerator}/{Denominator})";
        }

        public CommonFraction Clone() => new CommonFraction(Numerator, Denominator);

        public override bool Equals(object obj)
        {
            CommonFraction f = new CommonFraction(1,1);
            try
            {
                f = (CommonFraction)obj;
                f.Minimize();
            }
            catch
            {
                return false;
            }
            var t = this.Minimized();
            return Math.Abs(t.Numerator) == Math.Abs(f.Numerator) && Math.Abs(t.Denominator) == Math.Abs(f.Denominator) && t.IsNegative() == f.IsNegative();
        }
    }
}
