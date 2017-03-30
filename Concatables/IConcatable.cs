using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Collections
{
    public interface IConcatable<T>
    {
        IConcatable<T> Concat(IConcatable<IConcatable<T>> xs);
    }
}
