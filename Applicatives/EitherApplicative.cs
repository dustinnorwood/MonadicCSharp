using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class Either<A,B> : IApplicative<B>
    {
        public static Either<A,B> Pure(B val)
        {
            return Either<A,B>.Right(val);
        }

        IApplicative<B> IApplicative<B>.Pure(B val) { return Pure(val); }

        public static Func<Either<A,B>, Either<A,C>> ApS<C>(Either<A,Func<B, C>> appl)
        {
            return t =>
                {
                    t.OnRight(appl.OnRight) // TODO: Make a set of functions ~ { whenRight :: Either a b -> (b -> Either a c) -> Either a c, whenLeft :: Either a b -> (a -> Either a c) -> Either a c }
                     .OnLeft(a => Either<A,C>.Left(a));
                    if (appl.m_IsRight && _t.m_IsRight)
                    {
                        return Either<A,C>.Right(appl.m_Right(_t.m_Right));
                    }
                    else return Either<A,C>.Left(_t.m_Left);
                };
        }

        Func<IApplicative<B>, IApplicative<C>> IApplicative<B>.ApS<C>(IApplicative<Func<B, C>> appl)
        {
            return t => ApS<C>((Either<A,Func<B, C>>)appl)((Either<A,B>)t);
        }

        IApplicative<C> IApplicative<B>.Ap<C>(IApplicative<Func<B, C>> appl)
        {
            return Ap((Either<A,Func<B, C>>)appl);
        }


        public Either<A,C> Ap<C>(Either<A,Func<B, C>> appl)
        {
            return Either<A,B>.ApS(appl)(this);
        }
    }
}
