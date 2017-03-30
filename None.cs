using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public class None
    {
        public static None Instance = new None();

        public None() { }

        public static None Return(None n)
        {
            return Instance;
        }

        public static Func<Func<None,None>,None> Bind(None n)
        {
            return f => Instance;
        }

        public static Func<None,None> Then(None t)
        {
            return u => Instance;
        }

        public static None Pure(None n)
        {
            return Return(n);
        }

        public static Func<IApplicative<T>,None> Lift<T>(IApplicative<Func<T,None>> i)
        {
            return a => Instance;
        }
    }
}
