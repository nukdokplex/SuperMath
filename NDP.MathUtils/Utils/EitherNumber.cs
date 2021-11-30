using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDP.MathUtils.Utils
{
    public class EitherNumber
    {
        private readonly int integer;
        private readonly CommonFraction fraction;
        private readonly float real;
        private readonly byte which;

        public EitherNumber(int integer)
        {
            this.integer = integer;
            this.which = 1;
        }

        public EitherNumber(CommonFraction fraction)
        {
            this.fraction = fraction;
            this.which = 2;
        }

        public EitherNumber(float real)
        {
            this.real = real;
            this.which = 3;
        }

        public T Match<T>(Func<int, T> intFunc, Func<CommonFraction, T> fracFunc, Func<float, T> realFunc)
        {
            if (intFunc == null) throw new ArgumentNullException(nameof(intFunc));
            if (fracFunc == null) throw new ArgumentNullException(nameof(fracFunc));
            if (realFunc == null) throw new ArgumentNullException(nameof(realFunc));

            switch (which)
            {
                case 1: return intFunc(integer);
                case 2: return fracFunc(fraction);
                case 3: return realFunc(real);
                default: throw new InvalidOperationException();
            }
        }

        public int IntegerOrDefault() => this.Match(f => f, s => default(int), t => default(int));

        public CommonFraction FractionOrDefault() => this.Match(f => default(CommonFraction), s => s, t => default(CommonFraction));

        public float RealOrDefault() => this.Match(f => default(float), s => default(float), t => t);

        public static implicit operator EitherNumber(int integer) => new EitherNumber(integer);

        public static implicit operator EitherNumber(CommonFraction fraction) => new EitherNumber(fraction);

        public static implicit operator EitherNumber(float real) => new EitherNumber(real);

        public static EitherNumber operator +(EitherNumber a, EitherNumber b)
        {
            byte type = 0;
            object result = a.Match<object>(
                delegate (int ai)
                {
                    return b.Match<object>(
                        (int bi) => { type = 1; return ai + bi; },
                        (CommonFraction bc) => { type = 2; return ai + bc; },
                        (float bf) => { type = 3; return ai + bf; }
                    );
                },

                delegate (CommonFraction ac)
                {
                    return b.Match<object>(
                        (int bi) => { type = 2; return ac + bi; },
                        (CommonFraction bc) => { type = 2; return ac + bc; },
                        (float bf) => { type = 3; return ac.GetRealValue() + bf; }
                    );
                },

                delegate (float af)
                {
                    return b.Match<object>(
                        (int bi) => { type = 3; return af + bi; },
                        (CommonFraction bc) => { type = 3; return af + bc.GetRealValue(); },
                        (float bf) => { type = 3; return af + bf; }
                    );
                }
            );

            switch (type)
            {
                case 1: return new EitherNumber((int)result);
                case 2: return new EitherNumber((CommonFraction)result);
                case 3: return new EitherNumber((float)result);
                default: throw new InvalidOperationException();
            }
        }

        public static EitherNumber operator -(EitherNumber a, EitherNumber b)
        {
            byte type = 0;
            object result = a.Match<object>(
                delegate (int ai)
                {
                    return b.Match<object>(
                        (int bi)            => { type = 1; return ai - bi; },
                        (CommonFraction bc) => { type = 2; return ai - bc; },
                        (float bf)          => { type = 3; return ai - bf; }
                    );
                },

                delegate (CommonFraction ac)
                {
                    return b.Match<object>(
                        (int bi)            => { type = 2; return ac - bi; },
                        (CommonFraction bc) => { type = 2; return ac - bc; },
                        (float bf)          => { type = 3; return ac.GetRealValue() - bf; }
                    );
                },

                delegate (float af)
                {
                    return b.Match<object>(
                        (int bi)            => { type = 3; return af - bi; },
                        (CommonFraction bc) => { type = 3; return af - bc.GetRealValue(); },
                        (float bf)          => { type = 3; return af - bf; }
                    );
                }
            );

            switch (type)
            {
                case 1: return new EitherNumber((int)result);
                case 2: return new EitherNumber((CommonFraction)result);
                case 3: return new EitherNumber((float)result);
                default: throw new InvalidOperationException();
            }
        }

        public static EitherNumber operator *(EitherNumber a, EitherNumber b)
        {
            byte type = 0;
            object result = a.Match<object>(
                delegate (int ai)
                {
                    return b.Match<object>(
                        (int bi) => { type = 1; return ai * bi; },
                        (CommonFraction bc) => { type = 2; return ai * bc; },
                        (float bf) => { type = 3; return ai * bf; }
                    );
                },

                delegate (CommonFraction ac)
                {
                    return b.Match<object>(
                        (int bi) => { type = 2; return ac * bi; },
                        (CommonFraction bc) => { type = 2; return ac * bc; },
                        (float bf) => { type = 3; return ac.GetRealValue() * bf; }
                    );
                },

                delegate (float af)
                {
                    return b.Match<object>(
                        (int bi) => { type = 3; return af * bi; },
                        (CommonFraction bc) => { type = 3; return af * bc.GetRealValue(); },
                        (float bf) => { type = 3; return af * bf; }
                    );
                }
            );

            switch (type)
            {
                case 1: return new EitherNumber((int)result);
                case 2: return new EitherNumber((CommonFraction)result);
                case 3: return new EitherNumber((float)result);
                default: throw new InvalidOperationException();
            }
        }

        public static EitherNumber operator /(EitherNumber a, EitherNumber b)
        {
            byte type = 0;
            object result = a.Match<object>(
                delegate (int ai)
                {
                    return b.Match<object>(
                        (int bi) => { type = 2; return new CommonFraction(ai, bi); },
                        (CommonFraction bc) => { type = 2; return ai / bc; },
                        (float bf) => { type = 3; return ai / bf; }
                    );
                },

                delegate (CommonFraction ac)
                {
                    return b.Match<object>(
                        (int bi) => { type = 2; return ac / bi; },
                        (CommonFraction bc) => { type = 2; return ac / bc; },
                        (float bf) => { type = 3; return ac.GetRealValue() / bf; }
                    );
                },

                delegate (float af)
                {
                    return b.Match<object>(
                        (int bi) => { type = 3; return af / bi; },
                        (CommonFraction bc) => { type = 3; return af / bc.GetRealValue(); },
                        (float bf) => { type = 3; return af / bf; }
                    );
                }
            );

            switch (type)
            {
                case 1: return new EitherNumber((int)result);
                case 2: return new EitherNumber((CommonFraction)result);
                case 3: return new EitherNumber((float)result);
                default: throw new InvalidOperationException();
            }
        }
    }
}

