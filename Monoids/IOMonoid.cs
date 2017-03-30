using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class IO<T>
    {
        public static IO<T> Mempty()
        {
            return Pure(default(T));
        }

        public IO<T> Mappend(IO<T> m)
        {
            return m;
        }

        public static Func<IO<T>,IO<U>> Ap<U>(IO<Func<T,U>> m1)
        {
            Func<T,U> f = null;
            T x2 = default(T);

            return m2 => m1.As(ref f).Then(m2).As(ref x2).Then(IO<U>.Return(f(x2)));
        }
    }
}
