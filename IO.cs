using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class IO<T>
    {
        private readonly T m_Value;
        public T Value { get { return m_Value; } }

        public IO(T value)
        {
            m_Value = value;
        }

        public static IO<T> As(IO<T> monad, ref T val)
        {
            val = monad.Value;
            return monad;
        }

        public IO<T> As(ref T val)
        {
            val = m_Value;
            return this;
        }
    }

    public static class IO
    {
        public static IO<None> None = new IO<None>(MonadicCSharp.None.Instance);
        public static IO<string> GetLine(None n) { return IO<string>.Return(Console.ReadLine()); }

        public static IO<None> PutStrLn(string str) { Console.WriteLine(str); return IO<None>.Return(MonadicCSharp.None.Instance); }

        public static IO<None> Do<T>(this IO<T> io, Action<T> a)
        {
            return io.Bind(t => { a(t); return None; });
        }
    }
}
