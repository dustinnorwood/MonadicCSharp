using System;
using System.Linq;
using System.Text;

namespace Prelude
{
    public static class P
    {
        public static Func<A,A> Id<A> = a => a;

        public static Func<A,Func<B,A>> Const<A,B> = a => _ => a;

        public static Func<Func<A,B>,Func<A,C>> Dot<A,B,C>(this Func<B,C> f) {
            return g => a => f(g(a));
        }

        public static Func<Func<A,Func<B,C>>,Func<B,Func<A,C>>> Flip<A,B,C> = {
            return f => b => a => f(a)(b);
        }

        public static Func<Func<A,Bool>,Func<Func<A,A>,Func<A,A>>> Until<A> = {
            return p => f => a => {
                A _a = a;
                do {
                    _a = f(a);
                } while(p(a).Equals(Bool.False));
                return _a;
            }
        }

        public static Func<A,Func<A,A>> AsTypeOf<A> = a => _ => a;

        public static Func<Func<A,B>,Func<MList<A>,MList<B>> Map<A,B>= {
            f => la => {
                MList<B> bs;
                la.Filled((x,xs) => {bs = MList<B>.Cons(f(x))(Map(f)(bs))})
                  .Empty(() => {bs = MList<B>.Nil;});
                return bs;
            }
        }
    }
}
