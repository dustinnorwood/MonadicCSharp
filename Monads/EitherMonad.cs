using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class Either<A,B> : IMonad<B>
    {
        public static Either<A,B> Return(B val)
        {
                return Either<A,B>.Right(val);
        }

        IMonad<B> IMonad<B>.Return(B val) { return Return(val); }

        public Either<A,C> Bind<C>(Func<B, Either<A,C>> f)
        {
            if (m_IsRight)
                return f(m_Right);
            else return Either<A,C>.Left(m_Left);
        }

        IMonad<C> IMonad<B>.Bind<C>(Func<B, IMonad<C>> func)
        {
            return Bind(b => (Either<A,C>)func(b));
        }

        public Either<A,C> Then<C>(Either<A,C> monad)
        {
            return monad;
        }

        IMonad<C> IMonad<B>.Then<C>(IMonad<C> monad)
        {
            return Then((Either<A,C>)monad);
        }

        public static Either<A,B> Join(Either<A,<Either<A,B>> either)
        {
            return either.Bind<B>(Cat.Id);
        }

        IMonad<B> IMonad<B>.Join(IMonad<IMonad<B>> monad)
        {
            return Join((Either<A,<Either<A,B>>)monad);
        }
    }
}
