using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class StateValue<S, V> : IPassable<S, V>
    {
        public IPassable<U> In<U>(Func<S, V, IPassable<U>> f)
        {
            return f(m_State, m_Value);
        }

        public IPassable<U, W> In<U, W>(Func<S, V, IPassable<U, W>> f)
        {
            return f(m_State, m_Value);
        }
    }
}
