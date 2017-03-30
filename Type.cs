using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class MType<T>
    {
        private readonly T m_Value;
        public T Value { get { return m_Value; } }

        public MType(T t)
        {
            m_Value = t;
        }
    }
}
