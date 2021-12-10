using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniC_Antlr
{
    public class ASTElementChildrenEnumerator : IEnumerator<ASTElement>
    {
        private int m_currentContext;
        private int m_currentChildIndex;
        private ASTElement m_currentChild;
        private ASTElement m_currentNode;

        public ASTElement Current => m_currentChild;

        public ASTElementChildrenEnumerator(ASTElement mCurrentNode)
        {
            m_currentNode = mCurrentNode;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            m_currentChildIndex++;
            //Last child
            if (m_currentChildIndex == m_currentNode.getContextChildrenNumber(m_currentContext))
            {
                //Last context
                if (m_currentContext == m_currentNode.getContextNumber())
                    return false;//The end of iteration
                else
                {
                    //Iteration continues
                    m_currentContext++;
                    while (m_currentNode.getContextChildrenNumber(m_currentContext) == 0 &&
                           m_currentContext < m_currentNode.getContextNumber())
                    {
                        m_currentChildIndex++;
                    }
                    //Last context
                    if (m_currentContext == m_currentNode.getContextNumber())
                        return false;//The end of iteration
                    else
                    {
                        m_currentChildIndex = 0;
                        m_currentChild = m_currentNode.GetChild(m_currentContext,m_currentChildIndex);
                        return true; //Move to next context
                    }

                }
            }
            else
            {
                m_currentChildIndex++;
                m_currentChild = m_currentNode.GetChild(m_currentContext, m_currentChildIndex);
                return true; //Move to next context
            }
        }

        public void Reset()
        {
            m_currentContext=-1;
            m_currentChildIndex=-1;
            m_currentNode=null;
    }

        object IEnumerator.Current => Current;
    }

    public abstract class ASTElement :IEnumerable<ASTElement>
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

        public IEnumerator<ASTElement> GetEnumerator()
        {
            return new ASTElementChildrenEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract T Accept<T>(ASTBaseVisitor<T> visitor);

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

        public int getContextChildrenNumber(int context)
        {
            if (m_children.Length > context)
            {
                return m_children[context].Count;
            }
            else
            {
                throw new IndexOutOfRangeException("Out of range");
            }
        }

        public int getContextNumber()
        {
            return m_children.Length;
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
