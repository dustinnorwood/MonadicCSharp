﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class Maybe<T> : IApplicative<T>
    {
        public static Maybe<T> Pure(T val)
        {
            return new Maybe<T>(val);
        }

        IApplicative<T> IApplicative<T>.Pure(T val) { return Pure(val); }

        public static Func<Maybe<T>, Maybe<U>> LiftS<U>(Maybe<Func<T, U>> appl)
        {
            return _t =>
                {
                    if (appl.HasValue && _t.HasValue)
                    {
                        return new Maybe<U>(appl.Value(_t.Value));
                    }
                    else return new Maybe<U>();
                };
        }

        Func<IApplicative<T>, IApplicative<U>> IApplicative<T>.LiftS<U>(IApplicative<Func<T, U>> appl)
        {
            return t => LiftS<U>((Maybe<Func<T, U>>)appl)((Maybe<T>)t);
        }

        IApplicative<U> IApplicative<T>.Lift<U>(IApplicative<Func<T, U>> appl)
        {
            return Lift((Maybe<Func<T, U>>)appl);
        }


        public Maybe<U> Lift<U>(Maybe<Func<T, U>> appl)
        {
            return Maybe<T>.LiftS(appl)(this);
        }
    }
}
