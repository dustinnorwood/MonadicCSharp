using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class StateValue<S, V>
    {
        private readonly V m_Value;
        public V Value { get { return m_Value; } }

        private readonly S m_State;
        public S State { get { return m_State; } }

        public StateValue(S state, V value)
        {
            m_State = state;
            m_Value = value;
        }
    }
}
