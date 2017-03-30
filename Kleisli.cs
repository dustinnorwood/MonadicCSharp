using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Kleisli<A,B>
    {
        private Func<A, IMonad<B>> m_RunKleisli;
        public Func<A,IMonad<B>> RunKleisli {  get { return m_RunKleisli; } }

        public Kleisli(Func<A, IMonad<B>> runKleisli)
        {
            m_RunKleisli = runKleisli;
        }
    }
}
