using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Kleisli<A,B> : IArrow<A,B>
    {

        public static Kleisli<A,B> Arr(Func<A,IMonad<B>> f)
        {
            return new Kleisli<A, B>(f);
        }

        IArrow<A,B> IArrow<A,B>.Arr(Func<A, B> func)
        {
            return Arr(a => (IMonad<B>)func(a));
        }

        public Kleisli<Tuple<A,C>, Tuple<B,C>> First<C>()
        {
            return new Kleisli<Tuple<A, C>, Tuple<B, C>>(t => (IMonad<Tuple<B,C>>)RunKleisli(t.Item1).FMap(b => new Tuple<B, C>(b, t.Item2)));
        }

        IArrow<Tuple<A,C>,Tuple<B,C>> IArrow<A,B>.First<C>()
        {
            return First<C>();
        }

        public Kleisli<Tuple<C, A>, Tuple<C, B>> Second<C>()
        {
            return new Kleisli<Tuple<C, A>, Tuple<C, B>>(t => (IMonad<Tuple<C, B>>)RunKleisli(t.Item2).FMap(b => new Tuple<C, B>(t.Item1, b)));
        }

        IArrow<Tuple<C, A>, Tuple<C, B>> IArrow<A, B>.Second<C>()
        {
            return Second<C>();
        }
    }
}
