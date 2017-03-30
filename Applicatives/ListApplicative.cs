using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class MList<T> : IApplicative<T>
    {
        public static MList<T> Pure(T val)
        {
            return new MList<T>(new T[] { val });
        }

        IApplicative<T> IApplicative<T>.Pure(T val) { return Pure(val); }

        public static Func<MList<T>, MList<U>> LiftS<U>(MList<Func<T, U>> appl)
        {
            return _t =>
            {
                List<U> list = new List<U>();
                foreach (Func<T, U> f in appl)
                {
                    foreach (T t in _t)
                        list.Add(f(t));
                }

                return new MList<U>(list);
            };
        }

        Func<IApplicative<T>, IApplicative<U>> IApplicative<T>.LiftS<U>(IApplicative<Func<T, U>> appl)
        {
            return t => LiftS((MList<Func<T, U>>)appl)((MList<T>)t);
        }

        IApplicative<U> IApplicative<T>.Lift<U>(IApplicative<Func<T, U>> appl)
        {
            return Lift((MList<Func<T, U>>)appl);
        }


        public MList<U> Lift<U>(MList<Func<T, U>> appl)
        {
            return MList<T>.LiftS(appl)(this);
        }
    }
}
