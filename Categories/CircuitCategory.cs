using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Circuit<A, B> : ICategory<A, B>
    {
        public Circuit<A, A> Id()
        {
            return new Circuit<A, A>(a => new Tuple<Circuit<A, A>, A>(Id(), a));
        }

        ICategory<A, A> ICategory<A, B>.Id()
        {
            return Id();
        }

        public Circuit<C, B> Dot<C>(Circuit<C, A> circuit)
        {
            return new Circuit<C, B>(c =>
            {
                Tuple<Circuit<C, A>, A> c1 = circuit.UnCircuit(c);
                Tuple<Circuit<A, B>, B> c2 = this.UnCircuit(c1.Item2);
                return new Tuple<Circuit<C, B>, B>(c2.Item1.Dot(c1.Item1), c2.Item2);
            });
        }

        ICategory<C, B> ICategory<A, B>.Dot<C>(ICategory<C, A> cat)
        {
            return Dot((Circuit<C, A>)cat);
        }

        public Circuit<C, B> Left<C>(Circuit<C, A> circuit)
        {
            return Dot(circuit);
        }

        ICategory<C, B> ICategory<A, B>.Left<C>(ICategory<C, A> cat)
        {
            return Left((Circuit<C, A>)cat);
        }

        public Circuit<A, C> Right<C>(Circuit<B, C> circuit)
        {
            return circuit.Dot(this);
        }

        ICategory<A, C> ICategory<A, B>.Right<C>(ICategory<B, C> cat)
        {
            return Right((Circuit<B, C>)cat);
        }
    }
}
