using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Maybe<T> : IPassable<T>
    {
        public IPassable<U1> In<U1>(Func<T, IPassable<U1>> f)
        {
            if (m_HasValue)
                return f(m_Value);
            else return f(default(T));
        }

        public IPassable<U1, U2> In<U1, U2>(Func<T, IPassable<U1, U2>> f)
        {
            if (m_HasValue)
                return f(m_Value);
            else return f(default(T));
        }
    }
}
