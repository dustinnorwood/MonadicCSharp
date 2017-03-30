using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public interface IMonad<T> : IApplicative<T>
    {
        IMonad<T> Return(T val); // a -> m a
        //Func<Func<T, IMonad<U>>, IMonad<U>> Bind<U>(IMonad<T> monad); // m a -> (a -> m b) -> m b
        //Func<IMonad<U>, IMonad<U>> Then<U>(IMonad<T> monad); // m a -> m b -> m b

        IMonad<U> Bind<U>(Func<T, IMonad<U>> func);
        IMonad<U> Then<U>(IMonad<U> monad);
        //IFunc<IMonad<U>, IMonad<U>> Forward<U>(IMonad<T> val);

        IMonad<T> Join(IMonad<IMonad<T>> monad);
    }
}
