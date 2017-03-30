using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public interface IComonad<T> : IFunctor<T>
    {
        T Extract();
        IComonad<IComonad<T>> Duplicate();
        IComonad<U> Extend<U>(Func<IComonad<T>, U> func);
    }
}
