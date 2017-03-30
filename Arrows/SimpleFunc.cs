using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public class SimpleFunc<A, B> //: IArrow<A, B>
    {
        private readonly Func<A, B> m_RunF;
        public Func<A, B> RunF { get { return m_RunF; } }

        public SimpleFunc(Func<A, B> r)
        {
            m_RunF = r;
        }

        public static SimpleFunc<A, B> Arr(Func<A, B> f) { return new SimpleFunc<A, B>(f); }

        //IArrow<A, B> IArrow<A, B>.Arr(Func<A, B> func) { return Arr(func); }

        public static SimpleFunc<Tuple<A, C>, Tuple<B, C>> First<C>(SimpleFunc<A, B> s)
        {
            return new SimpleFunc<Tuple<A, C>, Tuple<B, C>>(t => new Tuple<B, C>(s.RunF(t.Item1), t.Item2));
        }

        //IArrow<Tuple<A, C>, Tuple<B, C>> IArrow<A, B>.First<C>(IArrow<A, B> arrow)
        //{
        //    return First<C>((SimpleFunc<A, B>)arrow);
        //}

        public static SimpleFunc<Tuple<C, A>, Tuple<C, B>> Second<C>(SimpleFunc<A, B> s)
        {
            return new SimpleFunc<Tuple<C, A>, Tuple<C, B>>(t => new Tuple<C, B>(t.Item1, s.RunF(t.Item2)));
        }

        //IArrow<Tuple<C, A>, Tuple<C, B>> IArrow<A, B>.Second<C>(IArrow<A, B> arrow)
        //{
        //    return Second<C>((SimpleFunc<A, B>)arrow);
        //}

        public static SimpleFunc<A, B> Id()
        {
            return new SimpleFunc<A, B>(a => default(B));
        }

        //ICategory<A, B> ICategory<A, B>.Id()
        //{
        //    return Id();
        //}

        public static Func<SimpleFunc<C, A>, SimpleFunc<C, B>> Dot<C>(SimpleFunc<A, B> cat)
        {
            return s => new SimpleFunc<C, B>(c => cat.RunF(s.RunF(c)));
        }

        //Func<ICategory<C, A>, ICategory<C, B>> ICategory<A, B>.Dot<C>(ICategory<A, B> cat)
        //{
        //    return ca => Dot<C>((SimpleFunc<A, B>)cat)((SimpleFunc<C,A>)ca);
        //}

        public SimpleFunc<C, B> Dot<C>(SimpleFunc<C, A> s)
        {
            return Dot<C>(this)(s);
        }

        //ICategory<C, B> ICategory<A, B>.Dot<C>(ICategory<C, A> cat)
        //{
        //    return Dot((SimpleFunc<C, A>)cat);
        //}

        SimpleFunc<C, B> Left<C>(SimpleFunc<C, A> s)
        {
            return new SimpleFunc<C, B>(c => RunF(s.RunF(c)));
        }

        //ICategory<C, B> ICategory<A, B>.Left<C>(ICategory<C, A> cat)
        //{
        //    return Left((SimpleFunc<C, A>)cat);
        //}

        public SimpleFunc<A, C> Right<C>(SimpleFunc<B, C> s)
        {
            return new SimpleFunc<A, C>(a => s.RunF(RunF(a)));
        }

        //ICategory<A, C> ICategory<A, B>.Right<C>(ICategory<B, C> cat)
        //{
        //    return Right((SimpleFunc<B, C>)cat);
        //}

        public SimpleFunc<Tuple<A, Ap>, Tuple<B, Bp>> Split<Ap, Bp>(SimpleFunc<Ap, Bp> prime)
        {
            return First<Ap>(this).Right(SimpleFunc<Ap, Bp>.Second<B>(prime));
        }

        //IArrow<Tuple<A, Ap>, Tuple<B, Bp>> IArrow<A, B>.Split<Ap, Bp>(IArrow<Ap, Bp> arrow)
        //{
        //    return Split((SimpleFunc<Ap, Bp>)arrow);
        //}

        public SimpleFunc<A, Tuple<B, Bp>> Fan<Bp>(SimpleFunc<A, Bp> prime)
        {
            return SimpleFunc<A, Tuple<A, A>>.Spl().Right(this.Split(prime));
        }

        //IArrow<A, Tuple<B, Bp>> IArrow<A, B>.Fan<Bp>(IArrow<A, Bp> arrow)
        //{
        //    return Fan((SimpleFunc<A, Bp>)arrow);
        //}

        public static Func<SimpleFunc<D, A>, Func<SimpleFunc<D, B>, SimpleFunc<D, C>>> LiftA2<C, D>(Func<A, Func<B, C>> op)
        {
            return f => g => f.Fan(g).Right(Unspl(op));
        }

        public static SimpleFunc<A, Tuple<A, A>> Spl()
        {
            return new SimpleFunc<A, Tuple<A, A>>(a => new Tuple<A, A>(a, a));
        }

        public static SimpleFunc<Tuple<A, B>, C> Unspl<C>(Func<A, Func<B, C>> f)
        {
            return SimpleFunc<Tuple<A, B>, C>.Arr(Uncurry(f));
        }

        public static Func<Tuple<T, U>, V> Uncurry<T, U, V>(Func<T, Func<U, V>> f)
        {
            return t => f(t.Item1)(t.Item2);
        }
    }
}
