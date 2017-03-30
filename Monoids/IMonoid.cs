using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public interface IMonoid<T>
    {
        IMonoid<T> Mempty();
        IMonoid<T> Mappend(IMonoid<T> m);
    }
}
