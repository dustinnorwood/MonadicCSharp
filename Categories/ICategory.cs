using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public interface ICategory<A,B>
    {
        ICategory<A,A> Id();
        ICategory<C, B> Dot<C>(ICategory<C, A> cat);
        ICategory<C, B> Left<C>(ICategory<C, A> cat);
        ICategory<A, C> Right<C>(ICategory<B, C> cat);
    }

    public static class Cat
    {
        public static A Id<A>(A a)
        {
            return a;
        }
    }
}
