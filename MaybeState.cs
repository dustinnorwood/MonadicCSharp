using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public class MMatch<T,V>
    {
        private readonly bool m_Handled;

        private readonly V m_Item;
        public V Item { get { return m_Item; } }

        private readonly T m_Value;
        public T Value { get { return m_Value; } }

        public MMatch(T value, V item)
        {
            m_Value = value;
            m_Item = item;
            m_Handled = false;
        }

        public MMatch(V item)
        {
            m_Value = default(T);
            m_Item = item;
            m_Handled = false;
        }

        private MMatch(T value, V item, bool handled)
        {
            m_Value = value;
            m_Item = item;
            m_Handled = handled;
        }

        public MMatch<T,V> Case(T value, Func<T,V> f)
        {
            if (m_Value != null && !m_Handled && m_Value.Equals(value))
                return new MMatch<T, V>(m_Value, f(m_Value), true);
            else return this;
        }

        public MMatch<T,V> Case(Predicate<T> condition, Func<T,V> f)
        {
            if (m_Value != null && !m_Handled && condition(m_Value))
                return new MMatch<T, V>(m_Value, f(m_Value), true);
            else return this;
        }

        public MMatch<T,V> Default(Func<T,V> f)
        {
            if (m_Value != null && !m_Handled)
                return new MMatch<T, V>(m_Value, f(m_Value), true);
            else return this;
        }

        public static MMatch<T,V> New(T value, V item)
        {
            return new MMatch<T, V>(value, item);
        }

        public static MMatch<T,V> New(V item)
        {
            return new MMatch<T, V>(item);
        }
    }
    public class MaybeBox<T,V> : MMatch<Maybe<T>,V>
    {
        public MaybeBox(Maybe<T> maybe, V item):base(maybe, item)
        {
        }

        public MaybeBox(V item):base(item)
        {
        }

        public static new MaybeBox<T,V> New(Maybe<T> maybe, V item)
        {
            if (maybe.HasValue)
                return new MaybeBox<T, V>(maybe, item);
            else return new MaybeBox<T, V>(item);
        }

        public static new MaybeBox<T,V> New(V item) { return new MaybeBox<T, V>(item); }
        
        public MaybeBox<T,V> Just(Func<T,V> f)
        {
            if (Value == null)
                return this;
            return Value.HasValue ? new MaybeBox<T, V>(Value, f(Value.Value)) : this;
        }

        public MaybeBox<T,V> Nothing(Func<V> f)
        {
            if (Value == null)
                return this;
            return Value.HasValue ? this : new MaybeBox<T, V>(f());
        }
    }

    public class ListMatch<T,V> : MMatch<MList<T>,V>
    {
        public ListMatch(MList<T> mlist, V item):base(mlist, item)
        {
        }

        public ListMatch(V item):base(item)
        {
        }

        public static new ListMatch<T, V> New(MList<T> mlist, V item)
        {
            return new ListMatch<T, V>(mlist, item);
        }

        public static new ListMatch<T, V> New(V item) { return new ListMatch<T, V>(item); }

        public ListMatch<T, V> Filled(Func<T, MList<T>, V> f)
        {
            MList<T> m = Value;
            if (m != null && m.Length > 0)
                return new ListMatch<T, V>(m, f(m.Head, m.Tail));
            else return this;
        }

        public ListMatch<T, V> Empty(Func<V> f)
        {
            MList<T> m = Value;
            if (m == null || m.Length > 0)
                return this;
            else return new ListMatch<T,V>(f());
        }
    }
}
