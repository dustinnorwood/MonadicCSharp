﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Writer<T,W> : IApplicative<T> where W : IMonoid<W>, new()
    {
        public static Writer<T,W> Pure(T val)
        {
            return new Writer<T, W>(val, (new W()).Mempty());
        }

        IApplicative<T> IApplicative<T>.Pure(T val)
        {
            return Pure(val);
        }
        
        public static Func<Writer<T,W>, Writer<U,W>> ApS<U>(Writer<Func<T,U>,W> appl)
        {
            return tw => new Writer<U, W>(appl.Value(tw.Value), tw.Log.Mappend(appl.Log));
        }

        Func<IApplicative<T>, IApplicative<U>> IApplicative<T>.ApS<U>(IApplicative<Func<T, U>> appl)
        {
            return t => ApS((Writer<Func<T, U>, W>)appl)((Writer<T,W>)t);
        }

        public Writer<U,W> Ap<U>(Writer<Func<T,U>,W> appl)
        {
            return ApS(appl)(this);
        }

        IApplicative<U> IApplicative<T>.Ap<U>(IApplicative<Func<T, U>> appl)
        {
            return Ap<U>((Writer<Func<T,U>,W>)appl);
        }
    }
}
