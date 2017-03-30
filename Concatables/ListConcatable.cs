using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Collections;

namespace MonadicCSharp
{
    public partial class MList<T> : IConcatable<T>
    {
        public static MList<T> Concat(MList<MList<T>> monad)
        {
            List<T> list = new List<T>();
            foreach (MList<T> ml in monad)
            {
                foreach (T t in ml)
                    list.Add(t);
            }
            return new MList<T>(list);
        }

        IConcatable<T> IConcatable<T>.Concat(IConcatable<IConcatable<T>> xs)
        {
            return (IConcatable<T>)Concat((MList<MList<T>>)xs);
        }
    }
}
