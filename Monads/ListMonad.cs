using System;
using System.Linq;
using System.Text;

using Collections;
using CSharpMonadicModel;

namespace MonadicCSharp
{
    public partial class MList<T> : IMonad<T>
    {
        public static MList<T> Return(T val)
        {
            return Pure(val);
        }

        IMonad<T> IMonad<T>.Return(T val) { return Return(val); }

        public MList<U> Bind<U>(Func<T, MList<U>> f)
        {
            return MList<U>.Join(FMap(f));
        }

        IMonad<U> IMonad<T>.Bind<U>(Func<T, IMonad<U>> func)
        {
            return Bind(t => (MList<U>)func(t));
        }

        public MList<U> Then<U>(MList<U> monad)
        {
            return monad;
        }

        IMonad<U> IMonad<T>.Then<U>(IMonad<U> monad)
        {
            return Then((MList<U>)monad);
        }

        public static MList<T> Join(MList<MList<T>> list)
        {
            return Concat(list);
        }

        IMonad<T> IMonad<T>.Join(IMonad<IMonad<T>> monad)
        {
            return Join((MList<MList<T>>)monad);
        }
    }
}
