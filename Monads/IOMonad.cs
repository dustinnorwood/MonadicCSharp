using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class IO<T> : IMonad<T>
    {
        public static IO<T> Return(T val)
        {
            return Pure(val);
        }

        IMonad<T> IMonad<T>.Return(T val) { return Return(val); }

        public IO<U> Bind<U>(Func<T, IO<U>> f)
        {
            return f(Value);
        }

        IMonad<U> IMonad<T>.Bind<U>(Func<T, IMonad<U>> func)
        {
            return (IMonad<U>)Bind(t => (IO<T>)func(t));
        }

        public IO<U> Then<U>(IO<U> monad)
        {
            return monad;
        }

        IMonad<U> IMonad<T>.Then<U>(IMonad<U> monad)
        {
            return Then((IO<U>)monad);
        }

        public IO<T> Join(IO<IO<T>> io)
        {
            return io.Bind<T>(Cat.Id);
        }

        IMonad<T> IMonad<T>.Join(IMonad<IMonad<T>> monad)
        {
            return Join((IO<IO<T>>)monad);
        }
    }
}

