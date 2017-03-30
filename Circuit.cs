using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Circuit<A,B>
    {
        private readonly Func<A, Tuple<Circuit<A, B>, B>> m_UnCircuit;
        public Func<A, Tuple<Circuit<A, B>, B>> UnCircuit { get { return m_UnCircuit; } }

        public Circuit(Func<A,Tuple<Circuit<A,B>,B>> uncircuit)
        {
            m_UnCircuit = uncircuit;
        }
    }

    //public static class Circuit
    //{
    //    public static Func<MList<A>,MList<B>> RunCircuit<A,B>(Circuit<A,B> circuit)
    //    {
    //        return alist => ListMatch<A, MList<B>>.New(alist, new MList<B>())
    //            .Filled((x, xs) =>
    //            {
    //                Tuple<Circuit<A, B>, B> c1 = circuit.UnCircuit(x);
    //                return RunCircuit<A, B>(c1.Item1)(xs).Prepend(c1.Item2);
    //            })
    //            .Item;
    //    }

    //    public static Func<Func<A,Func<Acc,Tuple<B,Acc>>>,Circuit<A,B>> Accum<A,B,Acc>(Acc acc)
    //    {
    //        return f => new Circuit<A, B>(input =>
    //        {
    //            Tuple<B, Acc> output = f(input)(acc);
    //            return new Tuple<Circuit<A, B>, B>(Accum<A,B,Acc>(output.Item2)(f), output.Item1);
    //        });
    //    }

    //    public static Func<Func<A,Func<B,B>>,Circuit<A,B>> Accum1<A,B>(B acc)
    //    {
    //        return f => Accum<A, B, B>(acc)(a => b =>
    //          {
    //              B b1 = f(a)(b);
    //              return new Tuple<B, B>(b1, b1);
    //          });
    //    }

    //    public static Circuit<double,double> Total()
    //    {
    //        return Accum1<double, double>(0)(a => b => a + b);
    //    }

    //    public static Func<Random, Circuit<None,int>> Generator(Tuple<int,int> t)
    //    {
    //        return rng => Accum<None, int, Random>(rng)(_ => rng1 => new Tuple<int,Random>(rng1.Next(t.Item1, t.Item2),rng1));
    //    }

    //    public static MList<string> dictionary = new MList<string>("dog", "cat", "bird");

    //    public static Circuit<None,string> PickWord(Random rng)
    //    {

    //    }
    //}
}
