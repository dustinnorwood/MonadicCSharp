using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class State<S,V> : IPassable<StateRunner<S, V>>
    {
        public IPassable<U1> In<U1>(Func<StateRunner<S,V>, IPassable<U1>> f)
        {
            return f(m_RunState);
        }

        public IPassable<U1, U2> In<U1, U2>(Func<StateRunner<S, V>, IPassable<U1, U2>> f)
        {
            return f(m_RunState);
        }
    }
}
