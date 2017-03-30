using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Circuit<A, B> : IArrow<A, B>
    {
        public Circuit<A,B> Arr(Func<A,B> f)
        {
            return new Circuit<A, B>(a => new Tuple<Circuit<A, B>, B>(Arr(f), f(a)));
        }

        IArrow<A, B> IArrow<A, B>.Arr(Func<A, B> func)
        {
            return Arr(func);
        }

        public Circuit<Tuple<A,C>, Tuple<B,C>> First<C>()
        {
            return new Circuit<Tuple<A, C>, Tuple<B, C>>(t =>
            {
                Tuple<Circuit<A,B>,B> c1 = UnCircuit(t.Item1);
                return new Tuple<Circuit<Tuple<A, C>, Tuple<B, C>>, Tuple<B, C>>(
                    c1.Item1.First<C>(), 
                    new Tuple<B, C>(c1.Item2, t.Item2));
            });
        }

        IArrow<Tuple<A, C>, Tuple<B, C>> IArrow<A, B>.First<C>()
        {
            return First<C>();
        }

        public Circuit<Tuple<C,A>,Tuple<C,B>> Second<C>()
        {
            return new Circuit<Tuple<C, A>, Tuple<C, B>>(t =>
            {
                Tuple<Circuit<A, B>, B> c1 = UnCircuit(t.Item2);
                return new Tuple<Circuit<Tuple<C, A>, Tuple<C, B>>, Tuple<C, B>>(
                    c1.Item1.Second<C>(),
                    new Tuple<C, B>(t.Item1, c1.Item2));
            });
        }

        IArrow<Tuple<C, A>, Tuple<C, B>> IArrow<A, B>.Second<C>()
        {
            return Second<C>();
        }
    }
}
