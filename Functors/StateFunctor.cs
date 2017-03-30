using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class State<S,V> : IFunctor<V>
    {
        public static Func<State<S, V>, State<S, U>> FMapS<U>(Func<V, U> f)
        {
            return sv => new State<S, U>(s => new StateValue<S, U>(s, f(sv.RunState(s).Value)));
        }

        Func<IFunctor<V>, IFunctor<U>> IFunctor<V>.FMapS<U>(Func<V, U> func)
        {
            return v => FMapS(func)((State<S,V>)v);
        }

        public State<S,U> FMap<U>(Func<V, U> func)
        {
            return FMapS(func)(this);
        }

        IFunctor<U> IFunctor<V>.FMap<U>(Func<V, U> func)
        {
            return FMap(func);
        }
    }
}
