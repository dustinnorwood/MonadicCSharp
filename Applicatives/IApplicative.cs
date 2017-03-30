using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public interface IApplicative<T> : IFunctor<T>
    {
        IApplicative<T> Pure(T val); // a -> f a
        Func<IApplicative<T>, IApplicative<U>> LiftS<U>(IApplicative<Func<T, U>> appl);
        IApplicative<U> Lift<U>(IApplicative<Func<T, U>> appl); // f (a -> b) -> f a -> f b
    }
}
