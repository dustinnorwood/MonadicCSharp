using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class MList<T> : IFunctor<T>
    {
        public static Func<MList<T>, MList<U>> FMapS<U>(Func<T, U> func)
        {
            return (_t =>
            {
                List<U> u = new List<U>();

                foreach (T t in _t)
                    u.Add(func(t));

                return new MList<U>(u);
            });
        }

        Func<IFunctor<T>, IFunctor<U>> IFunctor<T>.FMapS<U>(Func<T, U> func)
        {
            return t => FMapS(func)((MList<T>)t);
        }

        public MList<U> FMap<U>(Func<T, U> func)
        {
            return FMapS(func)(this);
        }

        IFunctor<U> IFunctor<T>.FMap<U>(Func<T, U> func)
        {
            return FMap(func);
        }
    }
}
