using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Writer<T,W> : IFunctor<T> where W : IMonoid<W>, new()
    {
        public static Func<Writer<T,W>,Writer<U,W>> FMapS<U>(Func<T,U> func)
        {
            return w => new Writer<U, W>(func(w.Value), w.Log);
        }

        Func<IFunctor<T>,IFunctor<U>> IFunctor<T>.FMapS<U>(Func<T, U> func)
        {
            return t => FMapS(func)((Writer<T,W>)t);
        }

        public Writer<U,W> FMap<U>(Func<T,U> func)
        {
            return FMapS(func)(this);
        }

        IFunctor<U> IFunctor<T>.FMap<U>(Func<T, U> func)
        {
            return FMap(func);
        }
    }
}
