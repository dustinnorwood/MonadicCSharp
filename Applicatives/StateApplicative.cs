using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class State<S,V> : IApplicative<V>
    {
        public static State<S, V> Pure(V v)
        {
            return Return(v);
        }

        IApplicative<V> IApplicative<V>.Pure(V val)
        {
            return Pure(val);
        }

        public static Func<State<S, V>, State<S, U>> ApS<U>(State<S, Func<V, U>> appl)
        {
            return sv => new State<S, U>(s => new StateValue<S, U>(s, appl.RunState(s).Value(sv.RunState(s).Value)));
        }

        Func<IApplicative<V>,IApplicative<U>> IApplicative<V>.ApS<U>(IApplicative<Func<V, U>> appl)
        {
            return v => ApS((State<S, Func<V, U>>)appl)((State<S, V>)v);
        }

        IApplicative<U> IApplicative<V>.Ap<U>(IApplicative<Func<V, U>> appl)
        {
            return Ap((State<S, Func<V, U>>)appl);
        }

        public State<S, U> Ap<U>(State<S, Func<V, U>> appl)
        {
            return State<S, V>.ApS(appl)(this);
        }
    }
}
