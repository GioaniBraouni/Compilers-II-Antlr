using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniC_Antlr
{
     public abstract class CNodeType<T>
    {
        public T MNodeType => m_nodeType;

        protected CNodeType(T mNodeType)
        {
            m_nodeType = mNodeType;
        }



        private T m_nodeType;

        public abstract T Default();

        public abstract T NA();

        public abstract T Map(int type);

        public abstract int Map(T type);
    }
}
