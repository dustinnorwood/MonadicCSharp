using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Writer<T,W> : IMonad<T> where W : IMonoid<W>, new()
    {

        public static Writer<T,W> Return(T x)
        {
            return new Writer<T, W>(x, (new W()).Mempty());
        }

        public Writer<U,W> Bind<U>(Func<T,Writer<U,W>> f)
        {
            Writer<U, W> uw = f(m_Value);
            return new Writer<U, W>(uw.Value, m_Log.Mappend(uw.Log));
        }

        public Writer<U,W> Then<U>(Writer<U,W> writer)
        {
            return writer;
        }

        public Writer<T,W> Join(Writer<Writer<T,W>,W> writer)
        {
            return writer.Bind<T>(Cat.Id);
        }

        IMonad<T> IMonad<T>.Return(T val)
        {
            return Return(val);
        }

        IMonad<U> IMonad<T>.Bind<U>(Func<T, IMonad<U>> func)
        {
            return Bind(t => (Writer<U, W>)func(t));
        }

        IMonad<U> IMonad<T>.Then<U>(IMonad<U> monad)
        {
            return Then((Writer<U, W>)monad);
        }

        IMonad<T> IMonad<T>.Join(IMonad<IMonad<T>> monad)
        {
            Func<Writer<Writer<T, W>, W>, Writer<T, W>> f = m => m.Value;
            return Join(new Writer<Writer<T,W>,W>(f((Writer<Writer<T, W>, W>)monad),((Writer<Writer<T,W>,W>)monad).Log));
        }
    }
}
