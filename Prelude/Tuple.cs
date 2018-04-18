using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prelude
{
    public partial class Tuple<T1, T2>
    {
        private readonly T1 m_Item1;
        private readonly T2 m_Item2;

        private Tuple(T1 item1, T2 item2)
        {
            m_Item1 = item1;
            m_Item2 = item2;
        }

        public static Func<T1,Func<T2,Tuple<T1,T2>>> Tup = t1 => t2 => new Tuple(t1,t2);

        public static Func<Tuple<T1,T2>,T1> Fst = t => t.m_Item1;

        public static Func<Tuple<T1,T2>,T2> Snd = t => t.m_Item2;

        public static Func<Func<Tuple<T1,T2>,U>,Func<T1,Func<T2,U>>> Curry<U> = f => {
            return t1 => t2 => f(Tup(t1)(t2));
        }

        public static Func<Func<T1,Func<T2,U>>,Func<Tuple<T1,T2>,U>> Uncurry<U> = f => {
            return t => f(Fst(t))(Snd(t));
        }
    }
}
