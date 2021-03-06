﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Cont<R,A> : IApplicative<A>
    {
        public Cont<R,A> Pure(A a)
        {
            return new Cont<R, A>(f => f(a));
        }

        IApplicative<A> IApplicative<A>.Pure(A val)
        {
            return Pure(val);
        }

        public static Func<Cont<R,A>,Cont<R,B>> ApS<B>(Cont<R,Func<A,B>> c_rab)
        {
            return c_ra => new Cont<R, B>(f_br => c_rab.RunCont(f_ab => c_ra.RunCont(a => f_br(f_ab(a)))));
        }

        Func<IApplicative<A>,IApplicative<B>> IApplicative<A>.ApS<B>(IApplicative<Func<A, B>> appl)
        {
            return ia => ApS((Cont<R, Func<A, B>>)appl)((Cont<R, A>)ia);
        }

        public Cont<R,B> Ap<B>(Cont<R,Func<A,B>> c_rab)
        {
            return ApS(c_rab)(this);
        }

        IApplicative<B> IApplicative<A>.Ap<B>(IApplicative<Func<A, B>> appl)
        {
            return Ap((Cont<R, Func<A, B>>)appl);
        }
    }
}
