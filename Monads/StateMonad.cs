using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class State<S, V> : IMonad<V>
    {
        public static State<S, V> Return(V v)
        {
            return new State<S, V>(s => new StateValue<S, V>(s ,v));
        }

        IMonad<V> IMonad<V>.Return(V val)
        {
            return Return(val);
        }

        public IMonad<U> Bind<U>(Func<V, State<S,U>> f)
        {
            return State<S,U>.Join(FMap(f));
        }

        IMonad<U> IMonad<V>.Bind<U>(Func<V, IMonad<U>> func)
        {
            return Bind(v => (State<S,U>)func(v));
        }

        public State<S,U> Then<U>(State<S,U> monad)
        {
            return monad;
        }

        IMonad<U> IMonad<V>.Then<U>(IMonad<U> monad)
        {
            return Then((State<S, U>)monad);
        }

        public static State<S,V> Join(State<S,State<S, V>> state)
        {
            return new State<S, V>(s => state.RunState(s).Value.RunState(s));
        }

        IMonad<V> IMonad<V>.Join(IMonad<IMonad<V>> monad)
        {
            return Join((State<S, State<S, V>>)monad);
        }
    }
}
