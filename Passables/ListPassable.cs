using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class MList<T> : IPassable<T>
    {
        public IPassable<U1> In<U1>(Func<T, IPassable<U1>> f)
        {
            return (IPassable<U1>)FMap(t => f(t));
        }

        public IPassable<U1, U2> In<U1, U2>(Func<T, IPassable<U1, U2>> f)
        {
            return (IPassable<U1,U2>)FMap(t => f(t));
        }
    }
}
