using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonadicCSharp
{
    public static class IEnumerableExtension
    {
        public delegate void EnumerableEventHandler<T>(T item);
        public delegate void EnumerableIndexedEventHandler<T>(T item, int index);

        public static void For<T>(this IEnumerable<T> list, EnumerableIndexedEventHandler<T> callback)
        {
            int k = 0;
            if (list != null && callback != null)
            {
                IEnumerator<T> ie = list.GetEnumerator();
                while(ie.MoveNext())
                {
                    callback(ie.Current, k);
                    k++;
                }
            }
        }

        public static void For<T>(this IEnumerable<T> list, int startIndex, EnumerableIndexedEventHandler<T> callback)
        {
            int k = 0;
            if (list != null && callback != null)
            {
                IEnumerator<T> ie = list.GetEnumerator();
                while (ie.MoveNext())
                {
                    if(k >= startIndex)
                        callback(ie.Current, k);
                    k++;
                }
            }
        }

        public static void For<T>(this IEnumerable<T> list, int startIndex, int endIndex, EnumerableIndexedEventHandler<T> callback)
        {
            int k = 0;
            if (list != null && callback != null)
            {
                IEnumerator<T> ie = list.GetEnumerator();
                while (ie.MoveNext())
                {
                    if (k >= startIndex)
                    {
                        if (k >= endIndex)
                            break;
                        else
                            callback(ie.Current, k);
                    }
                    k++;
                }
            }
        }

        public static void ForEach<T>(this IEnumerable<T> list, EnumerableEventHandler<T> callback)
        {
            if (list != null && callback != null)
            {
                foreach(T t in list)
                    callback(t);
            }
        }

        public static IEnumerable<T> Given<T>(this IEnumerable<T> list, Predicate<T> pred)
        {
            List<T> newList = new List<T>();
            foreach (T t in list)
            {
                if (pred(t))
                    newList.Add(t);
            }

            return newList;
        }

        public static IEnumerable<T> SubArray<T>(this IEnumerable<T> list, int startIndex, int count)
        {
            if (list == null) return null;
            List<T> subList = new List<T>();
            int k = 0, listCount = list.Count();
            IEnumerator<T> ie = list.GetEnumerator();
            while (ie.MoveNext())
            {
                if (k >= startIndex && k < startIndex + count && k < listCount)
                {
                    subList.Add(ie.Current);
                }
            }

            return subList;
        }

        public static string Reverse(this string str)
        {
            StringBuilder sb = new StringBuilder();
            str.ToCharArray().For((_, i) => sb.Append(str[str.Length - 1 - i]));
            return sb.ToString();
        }

        public static IEnumerable<U> Merge<T, U>(this IEnumerable<T> l1, IEnumerable<T> l2, Func<T, T, U> func)
        {
            List<U> ret = new List<U>();
            var e1 = l1.GetEnumerator();
            var e2 = l2.GetEnumerator();
            while (e1.MoveNext() && e2.MoveNext())
            {
                ret.Add(func(e1.Current, e2.Current));
            }

            return ret;
        }

        public static IEnumerable<U> Map<T, U>(this IEnumerable<T> _t, Func<T, U> f)
        {
            List<U> u = new List<U>();
            foreach (T t in _t)
                u.Add(f(t));
            return u;
        }

        public static string[] SplitBy(this string t, int size)
        {
            List<string> tp = new List<string>();
            int count = 0;
            while (count < t.Length)
            {
                if (count + size > t.Length)
                    tp.Add(t.Substring(count));
                else tp.Add(t.Substring(count, size));
                count += size;
            }
            return tp.ToArray();
        }

        public static IEnumerable<T> MonadicJoin<T>(this IEnumerable<IEnumerable<T>> list)
        {
            List<T> l = new List<T>();
            foreach (IEnumerable<T> ie in list)
            {
                foreach (T t in ie)
                    l.Add(t);
            }
            return l;
        }
    }
}
