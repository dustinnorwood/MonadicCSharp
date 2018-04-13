using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class Either<A,B> : IFunctor<B>
    {
        public static Func<Either<A,B>, Either<A,C>> FMapS<C>(Func<B, C> func)
        {
            return _t =>
            {
                _t.OnRight(func)
                  .OnLeft(a => Either<A,C>.Left(a));
            }
        }

        Func<IFunctor<B>, IFunctor<C>> IFunctor<B>.FMapS<C>(Func<B, C> func)
        {
            return t => FMapS(func)((Either<A,B>)t);
        }

        public Either<A,C> FMap<C>(Func<B, C> func)
        {
            return FMapS(func)(this);
        }

        IFunctor<C> IFunctor<B>.FMap<C>(Func<B, C> func)
        {
            return FMap(func);
        }
    }
}
