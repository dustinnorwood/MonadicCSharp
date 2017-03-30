using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public interface IFunctor<T>
    {
        // fmap :: Functor f => (T -> U) -> f T -> f U
        Func<IFunctor<T>, IFunctor<U>> FMapS<U>(Func<T, U> func);
        IFunctor<U> FMap<U>(Func<T, U> func);
    }
}
