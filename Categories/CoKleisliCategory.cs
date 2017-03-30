using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class CoKleisli<A,B> : ICategory<A,B>
    {
        public CoKleisli<A, A> Id()
        {
            return new CoKleisli<A, A>(a => a.Extract());
        }

        ICategory<A, A> ICategory<A, B>.Id()
        {
            return Id();
        }

        public CoKleisli<C, B> Dot<C>(CoKleisli<C, A> k)
        {
            return new CoKleisli<C, B>(c => RunCoKleisli(c.Extend(cp => k.RunCoKleisli(cp))));
        }

        ICategory<C, B> ICategory<A, B>.Dot<C>(ICategory<C, A> cat)
        {
            return Dot((CoKleisli<C, A>)cat);
        }

        public CoKleisli<C, B> Left<C>(CoKleisli<C, A> k)
        {
            return Dot(k);
        }

        ICategory<C, B> ICategory<A, B>.Left<C>(ICategory<C, A> cat)
        {
            return Left((CoKleisli<C, A>)cat);
        }

        public CoKleisli<A, C> Right<C>(CoKleisli<B, C> k)
        {
            return k.Dot(this);
        }

        ICategory<A, C> ICategory<A, B>.Right<C>(ICategory<B, C> cat)
        {
            return Right((CoKleisli<B, C>)cat);
        }
    }
}
