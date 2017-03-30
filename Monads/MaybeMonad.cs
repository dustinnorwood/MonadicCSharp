using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class Maybe<T> : IMonad<T>
    {
        public static Maybe<T> Return(T val)
        {
            if (val == null)
                return new Maybe<T>();
            else
                return new Maybe<T>(val);
        }

        IMonad<T> IMonad<T>.Return(T val) { return Return(val); }

        public Maybe<U> Bind<U>(Func<T, Maybe<U>> f)
        {
            if (m_HasValue)
                return f(m_Value);
            else return new Maybe<U>();
        }

        IMonad<U> IMonad<T>.Bind<U>(Func<T, IMonad<U>> func)
        {
            return Bind(t => (Maybe<U>)func(t));
        }

        public Maybe<U> Then<U>(Maybe<U> monad)
        {
            return monad;
        }

        IMonad<U> IMonad<T>.Then<U>(IMonad<U> monad)
        {
            return Then((Maybe<U>)monad);
        }

        public static Maybe<T> Join(Maybe<Maybe<T>> maybe)
        {
            return maybe.Bind<T>(Cat.Id);
        }

        IMonad<T> IMonad<T>.Join(IMonad<IMonad<T>> monad)
        {
            return Join((Maybe<Maybe<T>>)monad);
        }
    }
}
