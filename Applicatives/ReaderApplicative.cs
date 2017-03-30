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

        public static Func<Reader<S, T>, Reader<S, U>> LiftS<U>(Reader<S, Func<T, U>> reader)
        {
            return r => new Reader<S, U>(s => reader.Run(s)(r.Run(s)));
        }

        Func<IApplicative<T>, IApplicative<U>> IApplicative<T>.LiftS<U>(IApplicative<Func<T, U>> appl)
        {
            return t => LiftS((Reader<S, Func<T, U>>)appl)((Reader<S, T>)t);
        }

        public Reader<S, U> Lift<U>(Reader<S, Func<T, U>> reader)
        {
            return LiftS(reader)(this);
        }

        IApplicative<U> IApplicative<T>.Lift<U>(IApplicative<Func<T, U>> appl)
        {
            return Lift((Reader<S, Func<T, U>>)appl);
        }
    }
}
