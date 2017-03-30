using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Operators;

namespace MonadicCSharp
{
    public enum Loop { Continue, Break };
    public delegate void MListFilledDelegate<T>(T x, MList<T> xs);
    public delegate void MListEmptyDelegate();
    
    public delegate Loop MListUnindexedEventHandler<T>(T item);
    public delegate Loop MListIndexedEventHandler<T>(T item, int index);

    public partial class MList<T>
    {
        private readonly T[] m_List;

        public MList(IEnumerable<T> collection)
        {
            m_List = collection.ToArray();
        }

        public MList(params T[] ts)
        {
            m_List = new T[ts.Length];
            ts.CopyTo(m_List, 0);
        }

        public MList<T> Filled(MListFilledDelegate<T> action)
        {
            if (m_List.Length > 0)
                action(Head, Tail);
            return this;
        }

        public MList<T> Empty(MListEmptyDelegate action)
        {
            if (m_List.Length == 0)
                action();
            return this;
        }

        public T Head
        {
            get
            {
                if (m_List.Length > 0)
                    return m_List[0];
                else return default(T);
            }
        } 

        public MList<T> Tail
        {
            get
            {
                if (m_List.Length > 1)
                {
                    T[] t = new T[Length - 1];
                    for (int k = 0; k < t.Length; k++)
                        t[k] = m_List[k + 1];
                    return new MList<T>(t);
                }
                else return new MList<T>();
            }
        }

        public MList<T> Init
        {
            get
            {
                if (m_List.Length > 0)
                {
                    T[] t = new T[Length - 1];
                    for (int k = 0; k < t.Length; k++)
                        t[k] = m_List[k];
                    return new MList<T>(t);
                }
                else return new MList<T>();
            }
        }

        public T Last
        {
            get
            {
                if (m_List.Length > 0)
                    return m_List[Length - 1];
                else return default(T);
            }
        }

        public int Length { get { return m_List.Length; } }

        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < Length)
                {
                    return m_List[index];
                }
                else if (index < 0)
                    return Head;
                else return Last;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");
            int i = 0, c = this.Length;
            foreach (T t in this)
            {
                sb.Append(t.ToString());
                if (i < c - 1) sb.Append(',');
                i++;
            }
            sb.Append("]");
            return sb.ToString();
        }
    }

    public static class MList
    {
        //public static void For<T>(this MList<T> list, MListIndexedEventHandler<T> callback)
        //{
        //    For(list, 0, callback, 0);
        //}

        //public static void For<T>(this MList<T> list, int startIndex, MListIndexedEventHandler<T> callback)
        //{
        //    For(list, startIndex, callback, 0);
        //}

        //public static void For<T>(this MList<T> list, int startIndex, int stopIndex, MListIndexedEventHandler<T> callback)
        //{
        //    For(list, startIndex, stopIndex, callback, 0);
        //}

        //private static void For<T>(this MList<T> list, int startIndex, MListIndexedEventHandler<T> callback, int currentIndex)
        //{
        //    for(int k = currentIndex; k < list.Length; k++)
        //    { 
        //        if (currentIndex >= startIndex)
        //        {
        //            if (callback(list[k], currentIndex) == Loop.Break)
        //                break;
        //        }
        //    }
        //}

        //private static void For<T>(this MList<T> list, int startIndex, int stopIndex, MListIndexedEventHandler<T> callback, int currentIndex)
        //{
        //    int maxIndex = stopIndex < list.Length ? stopIndex : list.Length;
        //    for (int k = currentIndex; k < maxIndex; k++)
        //    {
        //        if (currentIndex >= startIndex)
        //        {
        //            if (callback(list[k], currentIndex) == Loop.Break)
        //                break;
        //        }
        //    }
        //}

        //public static void ForEach<T>(this MList<T> list, MListUnindexedEventHandler<T> callback)
        //{
        //    foreach(T t in list)
        //    {
        //        if (callback(t) == Loop.Break)
        //            break;
        //    }
        //}

        //public static T FoldLeft<T>(this MList<T> list, T accumulator, BinaryOperator<T> func)
        //{
        //    T t = accumulator;
        //    list.Filled((x, xs) => t = FoldLeft(xs, func(accumulator, x), func));
        //    return t;
        //}

        //public static T FoldRight<T>(this MList<T> list, T accumulator, BinaryOperator<T> func)
        //{
        //    T t = accumulator;
        //    list.Filled((x, xs) => t = func(x, FoldRight(xs, accumulator, func)));
        //    return t;
        //}

        public static MList<T> Prepend<T>(this MList<T> list, T element)
        {
            MList<T> t = new MList<T>(element);
            return MList<T>.Concat(new MList<MList<T>>(t, list));
        }

        public static MList<T> Append<T>(this MList<T> list, T element)
        {
            MList<T> t = new MList<T>(element);
            return MList<T>.Concat(new MList<MList<T>>(list, t));
        }

        public static MList<T> Drop<T>(this MList<T> list, int n)
        {
            MList<T> drop = new MList<T>();
            if (n > 0)
                list.Filled((x, xs) => drop = Drop(xs, n - 1));
            return drop;
        }

        public static MList<T> Replace<T>(this MList<T> list, int index, T replacement)
        {
            MList<T> t = new MList<T>();
            for(int i = 0; i < list.Length; i++)
            {
                if (i == index)
                    t = t.Append(replacement);
                else
                    t = t.Append(list[i]);
            }
            return t;
        }

        public static MList<char> ToMList(this string str)
        {
            return new MList<char>(str.ToCharArray());
        }
    }
}
