using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Reader<S,T> : IApplicative<T>
    {

        public Reader<S, T> Pure(T val)
        {
            return Return(val);
        }

        IApplicative<T> IApplicative<T>.Pure(T val)
        {
            return Pure(val);
        }

        public static Func<Reader<S, T>, Reader<S, U>> ApS<U>(Reader<S, Func<T, U>> reader)
        {
            return r => new Reader<S, U>(s => reader.Run(s)(r.Run(s)));
        }

        Func<IApplicative<T>, IApplicative<U>> IApplicative<T>.ApS<U>(IApplicative<Func<T, U>> appl)
        {
            return t => ApS((Reader<S, Func<T, U>>)appl)((Reader<S, T>)t);
        }

        public Reader<S, U> Ap<U>(Reader<S, Func<T, U>> reader)
        {
            return ApS(reader)(this);
        }

        IApplicative<U> IApplicative<T>.Ap<U>(IApplicative<Func<T, U>> appl)
        {
            return Ap((Reader<S, Func<T, U>>)appl);
        }
    }
}
