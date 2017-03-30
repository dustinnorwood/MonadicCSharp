using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Reader<S,T> : IFunctor<T>
    {
        public static Func<Reader<S, T>, Reader<S, U>> FMapS<U>(Func<T, U> func)
        {
            return r => new Reader<S, U>(s => func(r.Run(s)));
        }

        Func<IFunctor<T>, IFunctor<U>> IFunctor<T>.FMapS<U>(Func<T, U> func)
        {
            return t => FMapS(func)((Reader<S, T>)t);
        }

        public Reader<S, U> FMap<U>(Func<T, U> func)
        {
            return FMapS(func)(this);
        }

        IFunctor<U> IFunctor<T>.FMap<U>(Func<T, U> func)
        {
            return FMap(func);
        }
    }
}
