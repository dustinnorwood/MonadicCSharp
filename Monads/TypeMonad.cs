using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class MType<T> : IMonad<T>
    {
        public MType<T> Return(T t)
        {
            return new MType<T>(t);
        }

        IMonad<T> IMonad<T>.Return(T val)
        {
            return Return(val);
        }

        public MType<U> Bind<U>(Func<T,MType<U>> f)
        {
            return f(Value);
        }

        IMonad<U> IMonad<T>.Bind<U>(Func<T, IMonad<U>> func)
        {
            return Bind(t => (MType<U>)func(t));
        }

        public MType<U> Then<U>(MType<U> m)
        {
            return m;
        }

        IMonad<U> IMonad<T>.Then<U>(IMonad<U> monad)
        {
            return Then((MType<U>)monad);
        }

        public static MType<T> Join(MType<MType<T>> type)
        {
            return type.Bind<T>(Cat.Id);
        }

        IMonad<T> IMonad<T>.Join(IMonad<IMonad<T>> monad)
        {
            return Join((MType<MType<T>>)monad);
        }
    }
}
