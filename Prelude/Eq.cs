using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prelude
{
    public interface Eq<T>
    {
        Func<T,Func<T,Bool>> Equals;
        Func<T,Func<T,Bool>> NotEquals;
    }

    public partial class Bool : Eq<Bool>
    {
        public Func<Bool,Func<Bool,Bool>> Equals = b1 => b2 => Not(NotEquals(b1)(b2));
        public Func<Bool,Func<Bool,Bool>> NotEquals = b1 => b2 => Xor(b1)(b2);
    }
}
