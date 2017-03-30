using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class CoKleisli<A, B> : IArrow<A, B>
    {

        public static CoKleisli<A, B> Arr(Func<IComonad<A>, B> f)
        {
            return new CoKleisli<A, B>(f);
        }

        IArrow<A, B> IArrow<A, B>.Arr(Func<A, B> func)
        {
            return Arr(a => func(a.Extract()));
        }

        public CoKleisli<Tuple<A, C>, Tuple<B, C>> First<C>()
        {
            return new CoKleisli<Tuple<A, C>, Tuple<B, C>>(t => new Tuple<B, C>(RunCoKleisli(t.Extend(w => w.Extract().Item1)), t.Extract().Item2));
        }

        IArrow<Tuple<A, C>, Tuple<B, C>> IArrow<A, B>.First<C>()
        {
            return First<C>();
        }

        public CoKleisli<Tuple<C, A>, Tuple<C, B>> Second<C>()
        {
            return new CoKleisli<Tuple<C, A>, Tuple<C, B>>(t => new Tuple<C, B>(t.Extract().Item1, RunCoKleisli(t.Extend(w => w.Extract().Item2))));
        }

        IArrow<Tuple<C, A>, Tuple<C, B>> IArrow<A, B>.Second<C>()
        {
            return Second<C>();
        }
    }
}
