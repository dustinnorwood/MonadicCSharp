using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prelude
{
    public partial class Bool
    {
        public static Bool False = new Bool(false);
        public static Bool True = new Bool(true);
        public static Bool Otherwise = True;

        private readonly bool m_Bool;

        private Bool(bool b)
        {
            m_Bool = b;
        }

        public static Func<Bool,Func<Bool,Bool>> And = {
            return b1 => b2 => b1.m_Bool ? (b2.m_Bool ? True : False) : False;
        }

        public static Func<Bool,Func<Bool,Bool>> Or = {
            return b1 => b2 => b1.m_Bool ? True : (b2.m_Bool ? True : False);
        }

        public static Func<Bool,Func<Bool,Bool>> Xor = {
            return b1 => b2 => b1.m_Bool ? (b2.m_Bool ? False : True) : (b2.m_Bool ? True : False);
        }

        public static Func<Bool,Func<Bool,Bool>> Nand = {
            return b1 => b2 => b1.m_Bool ? (b2.m_Bool ? False : True) : True;
        }

        public static Func<Bool,Bool> Not = {
            return b => b.m_Bool ? False : True;
        }
    }
}
