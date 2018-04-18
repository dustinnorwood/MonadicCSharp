using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prelude
{
    public interface Ord<T>
    {
        Func<T,Func<T,Ordering>> Compare;
        Func<T,Func<T,Bool>> LessThan;
        Func<T,Func<T,Bool>> LessEqual;
        Func<T,Func<T,Bool>> GreaterEqual;
        Func<T,Func<T,Bool>> GreaterThan;
        Func<T,Func<T,Bool>> Max;
        Func<T,Func<T,Bool>> Min;
    }
}
