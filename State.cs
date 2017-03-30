using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public delegate StateValue<S, V> StateRunner<S, V>(S state);

    public partial class State<S,V>
    {
        private readonly StateRunner<S,V> m_RunState;
        public StateRunner<S,V> RunState { get { return m_RunState; } }

        public State(StateRunner<S,V> r)
        {
            m_RunState = r;
        }

        public static State<S, S> Get(None n)
        {
            return new State<S, S>(s => new StateValue<S, S>(s, s));
        }

        public static State<S, S> Get()
        {
            return new State<S, S>(s => new StateValue<S, S>(s, s));
        }

        public static State<S, None> Put(S s)
        {
            return new State<S, None>(ns => new StateValue<S, None>(s, None.Instance));
        }

        public Func<S, State<S, None>> Modify(Func<S, S> func)
        {
            return s => Put(func(Get().RunState(s).Value));
        }

        public Func<S, State<S,V>> GetS(Func<S, V> func)
        {
            return s => Return(func(Get().RunState(s).Value));
        }

        public V EvalState(S s)
        {
            return RunState(s).Value;
        }

        public S ExecState(S s)
        {
            return RunState(s).State;
        }
    }
}
