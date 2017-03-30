using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class Maybe<T> : IFunctor<T>
    {
        public static Func<Maybe<T>, Maybe<U>> FMapS<U>(Func<T, U> func)
        {
            return _t =>
            {
                if (_t.HasValue)
                    return new Maybe<U>(func(_t.Value));
                else return new Maybe<U>();
            };
        }

        Func<IFunctor<T>, IFunctor<U>> IFunctor<T>.FMapS<U>(Func<T, U> func)
        {
            return t => FMapS(func)((Maybe<T>)t);
        }

        public Maybe<U> FMap<U>(Func<T, U> func)
        {
            return FMapS(func)(this);
        }

        IFunctor<U> IFunctor<T>.FMap<U>(Func<T, U> func)
        {
            return FMap(func);
        }
    }
}
