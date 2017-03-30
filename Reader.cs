using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Reader<S,T>
    {
        private readonly Func<S, T> m_Func;

        public T Run(S s) { return m_Func(s); }

        public Reader(Func<S, T> func)
        {
            m_Func = func;
        }
    }
}
