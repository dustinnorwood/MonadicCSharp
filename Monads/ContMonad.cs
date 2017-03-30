using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Cont<R,A> : IMonad<A>
    {
        public Cont<R,A> Return(A a)
        {
            return new Cont<R, A>(f => f(a));
        }

        IMonad<A> IMonad<A>.Return(A val)
        {
            return Return(val);
        }

        public Cont<R,B> Bind<B>(Func<A,Cont<R,B>> f)
        {
            return new Cont<R,B>(g => m_RunCont(a => f(a).RunCont(g)));
        }

        IMonad<B> IMonad<A>.Bind<B>(Func<A, IMonad<B>> func)
        {
            return Bind(a => (Cont<R,B>)func(a));
        }

        public Cont<R,B> Then<B>(Cont<R,B> b)
        {
            return b;
        }

        IMonad<B> IMonad<A>.Then<B>(IMonad<B> monad)
        {
            return Then((Cont<R, B>)monad);
        }

        public static Cont<R,A> Join(Cont<R,Cont<R,A>> cont)
        {
            return cont.Bind<A>(Cat.Id);
        }

        IMonad<A> IMonad<A>.Join(IMonad<IMonad<A>> monad)
        {
            return Join((Cont<R, Cont<R, A>>)monad);
        }
    }
}
