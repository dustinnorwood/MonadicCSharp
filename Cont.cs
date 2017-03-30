using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public partial class Cont<R,A>
    {
        private readonly Func<Func<A, R>, R> m_RunCont;
        public Func<Func<A, R>, R> RunCont {  get { return m_RunCont; } }

        public Cont(Func<Func<A,R>, R> runCont)
        {
            m_RunCont = runCont;
        }
    }

    public static class Cont
    {
        public static Cont<R, A> CallCC<R, A, B>(Func<Func<A, Cont<R, B>>, Cont<R, A>> f)
        {
            return new Cont<R, A>(f_ar => f(a => new Cont<R, B>(_ => f_ar(a))).RunCont(f_ar));
        }

        public static Func<Cont<R, string>, Cont<R, A>> Error<R, A>()
        {
            return err => new Cont<R, A>(f_ar => err.RunCont(str =>
                    {
                        System.Diagnostics.Debug.WriteLine("Error in Continuation: " + str);
                        return default(R);
                    }));
        }

        public static Func<Func<A, R>, R> RunCont<R, A>(Cont<R, A> cont)
        {
            return f_ar => cont.RunCont(f_ar);
        }
    }
}
