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

        public CommonFraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public float GetRealValue()
        {
            return ((float)Numerator) / ((float)Denominator);
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

        

        public override string ToString()
        {
            return $"({Numerator}/{Denominator})";
        }

        public CommonFraction Clone() => new CommonFraction(Numerator, Denominator);
        
    }
}
