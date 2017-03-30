//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace MonadicCSharp
//{
//    public class MaybeKleisli<A, B> : IKleisli<A, B>
//    {
//        private Func<A, Maybe<B>> m_RunKleisli;
//        public Func<A, Maybe<B>> RunKleisli { get { return m_RunKleisli; } }
//        Func<A, IMonad<B>> IKleisli<A, B>.RunKleisli { get { return a => RunKleisli(a); } }

//        public MaybeKleisli(Func<A, Maybe<B>> f)
//        {
//            m_RunKleisli = f;
//        }

//        public static MaybeKleisli<A, B> Arr(Func<A, Maybe<B>> f)
//        {
//            return new MaybeKleisli<A, B>(f);
//        }

//        IKleisli<A, B> IKleisli<A, B>.Arr(Func<A, IMonad<B>> f)
//        {
//            return Arr(a => (Maybe<B>)f(a));
//        }

//        public static MaybeKleisli<Tuple<A, C>, Tuple<B, C>> First<C>(MaybeKleisli<A, B> k)
//        {
//            return new MaybeKleisli<Tuple<A, C>, Tuple<B, C>>(t => k.RunKleisli(t.Item1).FMap(b => new Tuple<B, C>(b, t.Item2)));
//        }

//        public static MaybeKleisli<Tuple<C, A>, Tuple<C, B>> Second<C>(MaybeKleisli<A, B> k)
//        {
//            return new MaybeKleisli<Tuple<C, A>, Tuple<C, B>>(t => k.RunKleisli(t.Item2).FMap(b => new Tuple<C, B>(t.Item1, b)));
//        }

//        public static MaybeKleisli<A, A> Id()
//        {
//            return new MaybeKleisli<A, A>(a => new Maybe<A>(a));
//        }

//        public static Func<MaybeKleisli<C, A>, MaybeKleisli<C, B>> Dot<C>(MaybeKleisli<A, B> k)
//        {
//            return ca => new MaybeKleisli<C, B>(c => Maybe<B>.Join(ca.RunKleisli(c).FMap(a => k.RunKleisli(a))));
//        }

//        public MaybeKleisli<C, B> Left<C>(MaybeKleisli<C, A> k)
//        {
//            return new MaybeKleisli<C, B>(c => Maybe<B>.Join(k.RunKleisli(c).FMap(a => this.RunKleisli(a))));
//        }

//        public MaybeKleisli<A, C> Right<C>(MaybeKleisli<B, C> k)
//        {
//            return new MaybeKleisli<A, C>(a => Maybe<C>.Join(this.RunKleisli(a).FMap(b => k.RunKleisli(b))));
//        }

//        public MaybeKleisli<Tuple<A, Ap>, Tuple<B, Bp>> Split<Ap, Bp>(MaybeKleisli<Ap, Bp> k)
//        {
//            return new MaybeKleisli<Tuple<A, Ap>, Tuple<B, Bp>>(t => Maybe<Tuple<B, Bp>>.Join(RunKleisli(t.Item1).FMap(b => k.RunKleisli(t.Item2).FMap(bp => new Tuple<B, Bp>(b, bp)))));
//        }

//        public MaybeKleisli<A, Tuple<B, Bp>> Fan<Bp>(MaybeKleisli<A, Bp> k)
//        {
//            return new MaybeKleisli<A, Tuple<B, Bp>>(a => Maybe<Tuple<B, Bp>>.Join(RunKleisli(a).FMap(b => k.RunKleisli(a).FMap(bp => new Tuple<B, Bp>(b, bp)))));
//        }

//        IKleisli<Tuple<A, C>, Tuple<B, C>> IKleisli<A, B>.First<C>(IKleisli<A, B> k)
//        {
//            return First<C>((MaybeKleisli<A, B>)k);
//        }

//        IKleisli<Tuple<C, A>, Tuple<C, B>> IKleisli<A, B>.Second<C>(IKleisli<A, B> k)
//        {
//            return Second<C>((MaybeKleisli<A, B>)k);
//        }

//        IKleisli<A, A> IKleisli<A, B>.Id()
//        {
//            return Id();
//        }

//        Func<IKleisli<C, A>, IKleisli<C, B>> IKleisli<A, B>.Dot<C>(IKleisli<A, B> k)
//        {
//            return ca => Dot<C>((MaybeKleisli<A, B>)k)((MaybeKleisli<C, A>)ca);
//        }

//        IKleisli<C, B> IKleisli<A, B>.Left<C>(IKleisli<C, A> k)
//        {
//            return Left((MaybeKleisli<C, A>)k);
//        }

//        IKleisli<A, C> IKleisli<A, B>.Right<C>(IKleisli<B, C> k)
//        {
//            return Right((MaybeKleisli<B, C>)k);
//        }

//        IKleisli<Tuple<A, Ap>, Tuple<B, Bp>> IKleisli<A, B>.Split<Ap, Bp>(IKleisli<Ap, Bp> k)
//        {
//            return Split((MaybeKleisli<Ap, Bp>)k);
//        }

//        IKleisli<A, Tuple<B, Bp>> IKleisli<A, B>.Fan<Bp>(IKleisli<A, Bp> k)
//        {
//            return Fan((MaybeKleisli<A, Bp>)k);
//        }
//    }
//}
