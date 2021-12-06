using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniC_Antlr
{
    public class ASTElement
    {
        private List<ASTElement>[] m_children = null;
        private ASTElement m_parent;
        private int m_serial;
        private string m_name;
        private static int m_serialCounter = 0;

        public ASTElement MParent
        {
            get => m_parent;
            set => m_parent = value;
        }

        public string MName => m_name;

        protected ASTElement(int context)
        {
            m_serial = m_serialCounter++;
            m_name = GenerateNodeName();
            if (context != 0)
            {
                m_children = new List<ASTElement>[context];
                for (int i = 0; i < context; i++)
                {
                    m_children[i] = new List<ASTElement>();
                }
            }
        }

        public void AddChild(ASTElement child, int contextIndex)
        {
            m_children[contextIndex].Add(child);
        }

        public ASTElement GetChild(int context, int index)
        {
            return m_children[context][index];
        }

        public virtual string GenerateNodeName()
        {
            return "_" + m_serial;
        }
    }
}
