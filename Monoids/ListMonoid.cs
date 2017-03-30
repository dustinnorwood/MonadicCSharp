using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class MList<T> : IMonoid<T>
    {
        public static MList<T> Mempty()
        {
            return new MList<T>();
        }

        IMonoid<T> IMonoid<T>.Mempty() { return Mempty(); }

        public MList<T> Mappend(MList<T> m)
        {
            MList<T> list = null;
            m.Filled((x, xs) => list = this.Append(x).Mappend(xs)).Empty(() => list = this);
            return list;
        }

        IMonoid<T> IMonoid<T>.Mappend(IMonoid<T> m)
        {
            return Mappend((MList<T>)m);
        }
    }
}
