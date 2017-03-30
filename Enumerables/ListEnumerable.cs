using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class MList<T> : IEnumerable<T>
    {
        public IEnumerator GetEnumerator()
        {
            return m_List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            List<T> list = new List<T>();
            foreach (T t in m_List)
                list.Add(t);

            return list.GetEnumerator();
        }
    }
}
