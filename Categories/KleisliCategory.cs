using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Kleisli<A,B> : ICategory<A,B>
    {
        public Kleisli<A, A> Id()
        {
            return new Kleisli<A, A>(a => (IMonad<A>)RunKleisli(a).FMap(b => a));
        }

        ICategory<A, A> ICategory<A, B>.Id()
        {
            return Id();
        }

        public Kleisli<C, B> Dot<C>(Kleisli<C, A> k)
        {
            return new Kleisli<C, B>(c => k.RunKleisli(c).Bind(RunKleisli));
        }

        ICategory<C, B> ICategory<A, B>.Dot<C>(ICategory<C, A> cat)
        {
            return Dot((Kleisli<C, A>)cat);
        }

        public Kleisli<C, B> Left<C>(Kleisli<C, A> k)
        {
            return Dot(k);
        }

        ICategory<C, B> ICategory<A, B>.Left<C>(ICategory<C, A> cat)
        {
            return Left((Kleisli<C, A>)cat);
        }

        public Kleisli<A, C> Right<C>(Kleisli<B, C> k)
        {
            return k.Dot(this);
        }

        ICategory<A, C> ICategory<A, B>.Right<C>(ICategory<B, C> cat)
        {
            return Right((Kleisli<B, C>)cat);
        }
    }
}
