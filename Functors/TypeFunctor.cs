using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class MType<T> : IFunctor<T>
    {
        public static Func<MType<T>, MType<U>> FMapS<U>(Func<T,U> func)
        {
            return t => new MType<U>(func(t.Value));
        }

        Func<IFunctor<T>, IFunctor<U>> IFunctor<T>.FMapS<U>(Func<T, U> func)
        {
            return t => FMapS(func)((MType<T>)t);
        }

        public MType<U> FMap<U>(Func<T, U> func)
        {
            return FMapS(func)(this);
        }

        IFunctor<U> IFunctor<T>.FMap<U>(Func<T, U> func)
        {
            return FMap(func);
        }
    }
}
