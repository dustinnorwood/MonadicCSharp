using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class Maybe<T>
    {
        //public static Maybe<T> Nothing = new Maybe<T>();

        private readonly bool m_HasValue;
        private readonly T m_Value;

        public Maybe() { }

        public Maybe(T value)
        {
            if (value != null)
            {
                m_Value = value;
                m_HasValue = true;
            }
            else
            {
                m_HasValue = false;
            }
        }

        public bool HasValue { get { return m_HasValue; } }

        public T Value { get { if (m_HasValue) return m_Value; else throw new InvalidOperationException(); } }

        public Maybe<T> Just(Action<T> action)
        {
            if (m_HasValue)
                action(m_Value);
            return this;
        }

        public Maybe<T> Nothing(Action action)
        {
            if (!m_HasValue)
                action();

            return this;
        }

        public override string ToString()
        {
            if (m_HasValue)
                return "Just " + m_Value.ToString();
            else return "Nothing";
        }
    }

    //public static class MaybeExt
    //{
    //    public static Maybe<U> Lift<T, U>(this Maybe<Func<T, U>> appl, Maybe<T> t)
    //    {
    //        return Maybe<T>.Lift(appl)(t);
    //    }
    //}
}
