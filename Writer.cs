using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Writer<T,W> where W : IMonoid<W>, new()
    {
        private readonly T m_Value;

        public T Value { get { return m_Value; } }

        private readonly IMonoid<W> m_Log;

        public IMonoid<W> Log { get { return m_Log; } }

        public Writer(T value, IMonoid<W> log)
        {
            m_Value = value;
            m_Log = log;
        }

        public static Writer<None,W> Tell(IMonoid<W> message)
        {
            return new Writer<None, W>(None.Instance, message);
        }
    }
}
