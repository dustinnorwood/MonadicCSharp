using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Cont<R,A> : IFunctor<A>
    {
        public static Func<Cont<R,A>,Cont<R,B>> FMapS<B>(Func<A,B> f)
        {
            return c => new Cont<R, B>(g => c.RunCont(a => g(f(a))));
        }

        Func<IFunctor<A>,IFunctor<B>> IFunctor<A>.FMapS<B>(Func<A, B> func)
        {
            return i => FMapS(func)((Cont<R, A>)i);
        }

        public Cont<R,B> FMap<B>(Func<A,B> f)
        {
            return FMapS(f)(this);
        }

        IFunctor<B> IFunctor<A>.FMap<B>(Func<A, B> func)
        {
            return FMap(func);
        }
    }
}
