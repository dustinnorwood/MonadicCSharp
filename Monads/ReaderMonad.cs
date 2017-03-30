using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Reader<S,T> : IMonad<T>
    {
        public static Reader<S,T> Return(T val)
        {
            return new Reader<S, T>(_ => val);
        }

        IMonad<T> IMonad<T>.Return(T val)
        {
            return Return(val);
        }

        public Reader<S,U> Bind<U>(Func<T,Reader<S,U>> func)
        {
            return Reader<S,U>.Join(FMapS(func)(this));
        }

        IMonad<U> IMonad<T>.Bind<U>(Func<T, IMonad<U>> func)
        {
            return Bind(t => (Reader<S,U>)func(t));
        }

        public Reader<S,U> Then<U>(Reader<S,U> reader)
        {
            return reader;
        }

        IMonad<U> IMonad<T>.Then<U>(IMonad<U> monad)
        {
            return Then((Reader<S, U>)monad);
        }

        public static Reader<S,T> Join(Reader<S,Reader<S,T>> reader)
        {
            return new Reader<S, T>(s => reader.Run(s).Run(s));
        }

        IMonad<T> IMonad<T>.Join(IMonad<IMonad<T>> monad)
        {
            return Join((Reader<S, Reader<S, T>>)monad);
        }
        
    }
}
