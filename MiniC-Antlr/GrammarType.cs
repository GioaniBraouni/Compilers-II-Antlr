using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniC_Antlr
{
    public class GrammarType : CNodeType<GrammarType.NodeType>
    {
        public GrammarType(NodeType nodeType) : base(nodeType)
        {
        }

        public enum NodeType
        {
        NT_FUNCTION ,NT_RETURN, NT_IF, NT_ELSEIF, NT_ELSE, NT_SWITCH, NT_CASE, 
        NT_DEFAULT, NT_WHILE, NT_DOWHILE, NT_FORLOOP, NT_BREAK, NT_ADDITION, NT_SUBTRACTION, NT_DIVISION, NT_MULTIPLICATION, 
        NT_OR, NT_AND, NT_NOT, NT_EQUAL, NT_NEQUAL, NT_GT, NT_LT, NT_GTE, NT_LTE, NT_QM,NT_COMPOUND,NT_STLIST,NT_ST,
        NT_LP, NT_RP, NT_LB, NT_RB, NT_COMMA, NT_ASSIGNMENT, NT_COLON, NT_IDENTIFIER, NT_NUMBER,
        NT_NA,NT_COMPILEUNIT
        }

        public override NodeType Default()
        {

            return NodeType.NT_NA;
        }

        public override NodeType NA()
        {
            return NodeType.NT_NA;
        }

        public override NodeType Map(int type)
        {
            return (NodeType) type;
        }

        public override int Map(NodeType type)
        {
            return (int) type;
        }
    }

    public abstract class GrammarASTElement : ASTElement
    {
        private GrammarType m_nodeType;

        protected GrammarASTElement(int context , GrammarType.NodeType Type) : base(context)
        {
            m_nodeType = new GrammarType(Type);  
        }
    }
    /*
    NT_FUNCTION ,NT_RETURN, NT_IF, NT_ELSEIF, NT_ELSE, NT_SWITCH, NT_CASE, 
    NT_DEFAULT, NT_WHILE, NT_DOWHILE, NT_FORLOOP, NT_BREAK, NT_ADDITION, NT_SUBTRACTION, NT_DIVISION, NT_MULTIPLICATION, 
    NT_OR, NT_AND, NT_NOT, NT_EQUAL, NT_NEQUAL, NT_GT, NT_LT, NT_GTE, NT_LTE, NT_QM, 
    NT_LP, NT_RP, NT_LB, NT_RB, NT_COMMA, NT_ASSIGNMENT, NT_COLON, NT_IDENTIFIER, NT_NUMBER,
    */

    public class CCompileUnit : GrammarASTElement
    {
        public const int CT_STATEMENTS = 0, CT_FUNCTIONDEFINITION = 1;
        public CCompileUnit() : base(2, GrammarType.NodeType.NT_COMPILEUNIT)
        {

        }
    }

    public class CAssignment : GrammarASTElement
    {
        public const int CT_LEFT = 0,CT_RIGHT=1;
        public CAssignment() : base(2, GrammarType.NodeType.NT_ASSIGNMENT)
        {

        }
    }

    public class CAddition : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '+', right=what appears right from the '+'

        public CAddition() : base(2, GrammarType.NodeType.NT_ADDITION)
        {
        }
    }

    public class CSubtraction : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '-', right=what appears right from the '-'

        public CSubtraction() : base(2, GrammarType.NodeType.NT_SUBTRACTION)
        {
        }
    }

    public class CSwitch : GrammarASTElement
    {
        public const int CT_CONDITION = 0, CT_CASE = 1,CT_DEFAULT=2;   //left=what appears left from the '-', right=what appears right from the '-'

        public CSwitch() : base(3, GrammarType.NodeType.NT_SWITCH)
        {
        }
    }


    public class CMultiplication : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '*', right=what appears right from the '*'

        public CMultiplication() : base(2, GrammarType.NodeType.NT_MULTIPLICATION)
        {
        }
    }

    public class CDivision : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '/', right=what appears right from the '/'

        public CDivision() : base(2, GrammarType.NodeType.NT_DIVISION)
        {
        }
    }

    public class CNUMBER : GrammarASTElement
    {
        private string m_text;
        private int m_value;

        public string MName1 => m_text;

        public CNUMBER(string name) : base(0, GrammarType.NodeType.NT_NUMBER)
        {
            m_text = name;
            m_value = int.Parse(name);
        }
    }

    public class CIdentifier : GrammarASTElement
    {
        private string m_name;

        public string MName1 => m_name;

        public CIdentifier(string name) : base(0, GrammarType.NodeType.NT_IDENTIFIER)
        {
            m_name = name;
        }
    }

    public class CAnd : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '&&', right=what appears right from the '&&'

        public CAnd() : base(2, GrammarType.NodeType.NT_AND)
        {
        }
    }

    public class COr : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'

        public COr() : base(2, GrammarType.NodeType.NT_OR)
        {
        }
    }

    public class CNot : GrammarASTElement
    {
        public const int CT_BODY = 0;


        public CNot() : base(1, GrammarType.NodeType.NT_NOT)
        {
        }
    }

    public class CGreaterThan : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'

        public CGreaterThan() : base(2, GrammarType.NodeType.NT_GT)
        {
        }
    }

    public class CGreaterThanEqual : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'

        public CGreaterThanEqual() : base(2, GrammarType.NodeType.NT_GTE)
        {
        }
    }

    public class CLessThan : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'

        public CLessThan() : base(2, GrammarType.NodeType.NT_LT)
        {
        }
    }

    public class CLessThanEqual : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'

        public CLessThanEqual() : base(2, GrammarType.NodeType.NT_LTE)
        {
        }
    }

    public class CEqual : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'

        public CEqual() : base(2, GrammarType.NodeType.NT_EQUAL)
        {
        }
    }

    public class CNotEqual : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'

        public CNotEqual() : base(2, GrammarType.NodeType.NT_NEQUAL)
        {
        }
    }


    public class CFunctionDefinition : GrammarASTElement
    {
        public const int CT_FNAME = 0, CT_FARGS = 1, CT_COMPOUNDSTATEMENT = 2;

        public CFunctionDefinition() : base(3, GrammarType.NodeType.NT_FUNCTION)
        {
        }
    }

    public class CIfStatement : GrammarASTElement
    {
        public const int CT_CONDITION = 0,
            CT_COMPOUNDSTATEMENT = 1,
            CT_CONDITION2 = 2,
            CT_COMPOUNDSTATEMENT2 = 3,
            CT_COMPOUNDSTATEMENT3 = 4;
        public CIfStatement() : base(4, GrammarType.NodeType.NT_IF)
        {
        }
    }

    public class CWhileStatement : GrammarASTElement
    {
        public const int CT_CONDITION = 0, CT_COMPOUNDSTATEMENT = 1;

        public CWhileStatement() : base(2, GrammarType.NodeType.NT_WHILE)
        {
        }
    }

    public class CDoWhileStatement : GrammarASTElement
    {
        public const int CT_CONDITION = 1, CT_COMPOUNDSTATEMENT = 0;

        public CDoWhileStatement() : base(2, GrammarType.NodeType.NT_WHILE)
        {
        }
    }



    public class CCompoundStatement : GrammarASTElement
    {
        public const int CT_STATEMENTLIST = 0;

        public CCompoundStatement() : base(1, GrammarType.NodeType.NT_COMPOUND)
        {
        }
    }

    public class CReturnStatement : GrammarASTElement
    {

        public CReturnStatement() : base(0, GrammarType.NodeType.NT_RETURN)
        {
        }
    }

    public class CBreakStatement : GrammarASTElement
    {

        public CBreakStatement() : base(0, GrammarType.NodeType.NT_BREAK)
        {
        }
    }

}
