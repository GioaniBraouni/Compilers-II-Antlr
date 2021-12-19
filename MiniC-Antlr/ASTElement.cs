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

    public enum NodeType
    {
        NT_NA,
        NT_COMPILEUNIT,
        NT_ASSIGNMENT,
        NT_ADDITION,
        NT_SUBTRACTION,
        NT_MULTIPLICATION,
        NT_DIVISION,
        NT_NUMBER,
        NT_IDENTIFIER,
        NT_AND,
        NT_OR,
        NT_NOT,
        NT_GT,
        NT_GTE,
        NT_LT,
        NT_LTE,
        NT_EQUAL,
        NT_NEQUAL,
        NT_FUNCTIONDEFINITION,
        NT_IFSTATEMENT,
        NT_WHILESTATEMENT,
        NT_DOWHILESTATEMENT,
        NT_FORLOOPSTATEMENT,
        NT_EXPRPLUSPLUS,
        NT_PLUSPLUSEXPR,
        NT_EXPRMINUSMINUS,
        NT_MINUSMINUSEXPR,
        NT_RETURN_STATEMENT,
        NT_BREAK_STATEMENT,
        NT_CASEOPTIONS,
        NT_DEFAULTOPTION,
        NT_SWITCHCASE

    }

    public abstract class ASTElement :IEnumerable<ASTElement>
    {
        private List<ASTElement>[] m_children = null;
        private ASTElement m_parent;
        private int m_serial;
        private string m_name;
        private static int m_serialCounter = 0;
        private NodeType m_type;

        public NodeType Type
        {
            get => m_type;
        }

        public ASTElement MParent
        {
            get => m_parent;
            set => m_parent = value;
        }

        public virtual string MName => m_name;

        public IEnumerator<ASTElement> GetEnumerator()
        {
            return new ASTElementChildrenEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract T Accept<T>(ASTBaseVisitor<T> visitor);

        protected ASTElement(NodeType type,int context)
        {
            m_serial = m_serialCounter++;
            m_type = type;
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

        public IEnumerable<ASTElement> GetChildren(int context)
        {
            return m_children[context];
        }

        public int getContextChildrenNumber(int context)
        {
            if (m_children.Length > context && m_children != null) 
            {
                return m_children[context].Count;
            }
            else if (m_children != null && m_children.Length <= context)
            {
                throw new IndexOutOfRangeException("Out of range");
            }
            else
                return 0;
        }

        public int getContextNumber()
        {
            return m_children.Length;
        }

        public ASTElement GetChild(int context, int index)
        {
            return m_children[context][index];
        }

        public IEnumerable<ASTElement> GetContextChildren(int context)
        {
            foreach (ASTElement c in m_children[context])
            {
                yield return c;
            }
        }

        public virtual string GenerateNodeName()
        {
            return Enum.GetName(typeof(NodeType), Type) + "_" + m_serial;
        }
    }
}
