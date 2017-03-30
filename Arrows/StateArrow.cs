using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class StateArrow<A,S,V> //: Kleisli<A,V> where S : State<S,V>
    {
        public StateArrow(Func<A,State<S,V>> f) { }
    }
}
