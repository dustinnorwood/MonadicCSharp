using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prelude
{
    public partial class Ordering
    {
        private enum Ord {LT, EQ, GT}

        public static Ordering LT = new Ordering(Ord.LT);
        public static Ordering EQ = new Ordering(Ord.EQ);
        public static Ordering GT = new Ordering(Ord.GT);

        private readonly Ord m_Ordering;

        private Ordering(Ord o)
        {
            m_Ordering = o;
        }
    }
}
