using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public interface IReturnable<T>
    {
        T Return();
    }
}
