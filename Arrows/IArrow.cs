using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    interface IArrow<A,B> : ICategory<A,B>
    {
        IArrow<A,B> Arr(Func<A,B> func);
        IArrow<Tuple<A,C>,Tuple<B,C>> First<C>();
        IArrow<Tuple<C, A>, Tuple<C, B>> Second<C>();
    }
}
