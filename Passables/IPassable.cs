using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public interface IPassable<T1>
    {
        IPassable<U1> In<U1>(Func<T1, IPassable<U1>> f);
        IPassable<U1,U2> In<U1,U2>(Func<T1, IPassable<U1,U2>> f);
    }

    public interface IPassable<T1, T2>
    {
        IPassable<U1> In<U1>(Func<T1, T2, IPassable<U1>> f);
        IPassable<U1,U2> In<U1,U2>(Func<T1, T2, IPassable<U1,U2>> f);
    }
}
