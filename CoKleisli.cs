using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class CoKleisli<A,B>
    {
        private readonly Func<IComonad<A>, B> m_RunCoKleisli;
        public Func<IComonad<A>, B> RunCoKleisli { get { return m_RunCoKleisli; } }

        public CoKleisli(Func<IComonad<A>, B> run)
        {
            m_RunCoKleisli = run;
        }
    }
}
