using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MonadicCSharp
{
    public partial class Either<A,B>
    {
        private readonly bool m_IsRight;
        private readonly A m_Left;
        private readonly B m_Right;

        public Either(A value)
        {
            m_Left = value;
            m_IsRight = false;
        }

        public Either(B value)
        {
            m_Right = value;
            m_IsRight = true;
        }

        public static Either<A,B> Left(A value)
        {
            return new Either<A,B>(value);
        }

        public static Either<A,B> Right(B value)
        {
            return new Either<A,B>(value);
        }

        public Either<A,B> OnLeft(Action<A> action)
        {
            if (!m_IsRight)
                action(m_Left);
            return this;
        }

        public Either<A,B> OnRight(Action<B> action)
        {
            if (m_IsRight)
                action(m_Right);=
            return this;
        }

        public override string ToString()
        {
            if (m_IsRight)
                return "Right " + m_Right.ToString();
            else return "Left " + m_Left.ToString();
        }
    }
}
