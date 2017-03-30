using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public delegate A a<A>();
    public delegate IFunctor<A> fa<A>();
    public delegate IMonad<A> ma<A>();

    public delegate A a_a<A>(A a);
    public delegate B a_b<A, B>(A a);
    public delegate IMonad<B> a_mb<A, B>(A a);
    public delegate B wa_b<A, B>(IComonad<A> wa);
    public delegate IMonad<B> Ca_mbJ_mb<A, B>(a_mb<A, B> amb);

    public delegate a_a<A> a_a_a<A>(A a);
    public delegate a_b<A, B> a_a_b<A, B>(A a);
    public delegate a_a<B> a_b_b<A, B>(A a);
    public delegate a_b<B, A> a_b_a<A, B>(A a);
    public delegate a_b<B, C> a_b_c<A, B, C>(A a);
    public delegate a_b<a_mb<A, B>, IMonad<B>> ma_Ca_mbJ_mb<A, B>(IMonad<A> ma);

}
