using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class MType<T> : IApplicative<T>
    {
        public MType<T> Pure(T val)
        {
            return new MType<T>(val);
        }

        IApplicative<T> IApplicative<T>.Pure(T val)
        {
            return Pure(val);
        }

        public Func<MType<T>,MType<U>> ApS<U>(MType<Func<T,U>> f)
        {
            return t => new MType<U>(f.Value(t.Value));
        }

        Func<IApplicative<T>, IApplicative<U>> IApplicative<T>.ApS<U>(IApplicative<Func<T, U>> appl)
        {
            return t => ApS((MType<Func<T, U>>)appl)((MType<T>)t);
        }

        IApplicative<U> IApplicative<T>.Ap<U>(IApplicative<Func<T, U>> appl)
        {
            return Ap((MType<Func<T, U>>)appl);
        }

        public MType<U> Ap<U>(MType<Func<T, U>> appl)
        {
            return ApS(appl)(this);
        }
    }
}
