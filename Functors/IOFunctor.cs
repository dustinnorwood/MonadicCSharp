using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class IO<T> : IFunctor<T>
    {
        public static Func<IO<T>, IO<U>> FMapS<U>(Func<T, U> func)
        {
            return _t => IO<U>.Return(func(_t.Value));
        }

        Func<IFunctor<T>, IFunctor<U>> IFunctor<T>.FMapS<U>(Func<T, U> func)
        {
            return t => FMapS(func)((IO<T>)t);
        }

        public IO<U> FMap<U>(Func<T,U> func)
        {
            return FMapS(func)(this);
        }

        IFunctor<U> IFunctor<T>.FMap<U>(Func<T, U> func)
        {
            return FMap(func);
        }
    }
}
