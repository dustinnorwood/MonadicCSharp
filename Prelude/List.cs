using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prelude
{
    public partial class List<T>
    {
        public static List<T> Nil = new List<T>();
        public static Func<T,Func<List<T>,List<T>>> Cons = t => ts => new List<T>(t,ts);

        private bool m_Nil;
        private T m_Head;
        private List<T> m_Tail;

        private List()
        {
            m_Nil = true;
        }

        private List(T t, List<T> ts)
        {
            m_Nil = false;
            m_Head = t;
            m_Tail = ts;
        }
    }
}
