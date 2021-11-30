using System;

namespace NDP.MathUtils.Utils
{
    public class Either3<T1, T2, T3>
    {
        private readonly T1 first;
        private readonly T2 second;
        private readonly T3 third;
        private readonly byte which;

        public Either3(T1 first)
        {
            this.first = first;
            this.which = 1;
        }

        public Either3(T2 second)
        {
            this.second = second;
            this.which = 2;
        }

        public Either3(T3 third)
        {
            this.third = third;
            this.which = 3;
        }

        public T Match<T>(Func<T1, T> firstFunc, Func<T2, T> secondFunc, Func<T3, T> thirdFunc)
        {
            if (firstFunc == null)
            {
                throw new ArgumentNullException(nameof(firstFunc));
            }

            if (secondFunc == null)
            {
                throw new ArgumentNullException(nameof(secondFunc));
            }

            if (thirdFunc == null)
            {
                throw new ArgumentNullException(nameof(thirdFunc));
            }

            switch (which)
            {
                case 1: return firstFunc(this.first);
                case 2: return secondFunc(this.second);
                case 3: return thirdFunc(this.third);
                default: throw new InvalidOperationException();
            }
        }

        public T1 FirstOrDefault() => this.Match(f => f, s => default(T1), t => default(T1));

        public T2 SecondOrDefault() => this.Match(f => default(T2), s => s, t => default(T2));

        public T3 ThirdOrDefault() => this.Match(f => default(T3), s => default(T3), t => t);

        public static implicit operator Either3<T1, T2, T3>(T1 first) => new Either3<T1, T2, T3>(first);

        public static implicit operator Either3<T1, T2, T3>(T2 second) => new Either3<T1, T2, T3>(second);

        public static implicit operator Either3<T1, T2, T3>(T3 third) => new Either3<T1, T2, T3>(third);
    }
}
