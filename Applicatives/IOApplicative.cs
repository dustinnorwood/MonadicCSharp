using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class IO<T> : IApplicative<T>
    {
        public static IO<T> Pure(T val)
        {
            return new IO<T>(val);
        }

        IApplicative<T> IApplicative<T>.Pure(T val) { return Pure(val); }

        public static Func<IO<T>, IO<U>> ApS<U>(IO<Func<T, U>> appl)
        {
            return _t => IO<U>.Pure(appl.Value(_t.Value));
        }

        Func<IApplicative<T>,IApplicative<U>> IApplicative<T>.ApS<U>(IApplicative<Func<T, U>> appl)
        {
            return t => ApS((IO<Func<T, U>>)appl)((IO<T>)t);
        }

        IApplicative<U> IApplicative<T>.Ap<U>(IApplicative<Func<T, U>> appl)
        {
            return Ap<U>((IO<Func<T, U>>)appl);
        }

        public IO<U> Ap<U>(IO<Func<T, U>> appl)
        {
            return ApS(appl)(this);
        }
    }
}
