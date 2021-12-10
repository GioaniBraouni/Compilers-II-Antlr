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
        NT_FUNCTION ,NT_RETURN, NT_IF, NT_SWITCH, NT_CASEOPTIONS, 
        NT_DEFAULTOPTION, NT_WHILE, NT_DOWHILE, NT_FORLOOP, NT_BREAK, NT_ADDITION, NT_SUBTRACTION, NT_DIVISION, NT_MULTIPLICATION, 
        NT_OR, NT_AND, NT_NOT, NT_EQUAL, NT_NEQUAL, NT_GT, NT_LT, NT_GTE, NT_LTE, NT_QM,NT_COMPOUND,NT_STLIST,NT_ST,
        NT_LP, NT_RP, NT_LB, NT_RB, NT_COMMA, NT_ASSIGNMENT, NT_COLON, NT_IDENTIFIER, NT_NUMBER,NT_EXPRPLUSPLUS,NT_EXPRMINUSMINUS,
        NT_NA, NT_COMPILEUNIT,NT_PLUSPLUSEXPR, NT_MINUSMINUSEXPR,
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

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCCompileUnit(this);
        }

        public CCompileUnit() : base(2, GrammarType.NodeType.NT_COMPILEUNIT)
        {

        }

    }

    public class CCaseOptions : GrammarASTElement
    {
        public const int CT_CASECONDITION = 0, CT_STATEMENT = 1;
        public CCaseOptions() : base(2, GrammarType.NodeType.NT_CASEOPTIONS)
        {

        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCCaseOptions(this);
        }
    }

    public class CAssignment : GrammarASTElement
    {
        public const int CT_LEFT = 0,CT_RIGHT=1;
        public CAssignment() : base(2, GrammarType.NodeType.NT_ASSIGNMENT)
        {
            
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCAssignment(this);
        }
    }

    public class CAddition : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '+', right=what appears right from the '+'

        public CAddition() : base(2, GrammarType.NodeType.NT_ADDITION)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCAddition(this);
        }
    }

    public class CSubtraction : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '-', right=what appears right from the '-'

        public CSubtraction() : base(2, GrammarType.NodeType.NT_SUBTRACTION)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCSubtraction(this);
        }
    }

    public class CSwitch : GrammarASTElement
    {
        public const int CT_CONDITION = 0, CT_CASE = 1,CT_DEFAULT=2;   //left=what appears left from the '-', right=what appears right from the '-'

        public CSwitch() : base(3, GrammarType.NodeType.NT_SWITCH)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCSwitch(this);
        }
    }


    public class CMultiplication : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '*', right=what appears right from the '*'

        public CMultiplication() : base(2, GrammarType.NodeType.NT_MULTIPLICATION)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCMultiplication(this);
        }
    }

    public class CDivision : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '/', right=what appears right from the '/'

        public CDivision() : base(2, GrammarType.NodeType.NT_DIVISION)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCDivision(this);
        }
    }

    public class CDefaultOption : GrammarASTElement
    {
        public const int CT_STATEMENT = 0;

        public CDefaultOption() : base(1, GrammarType.NodeType.NT_DEFAULTOPTION)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCDefaultOption(this);
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

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCNUMBER(this);
        }
    }

    public class CIDENTIFIER : GrammarASTElement
    {
        private string m_name;

        public string MName1 => m_name;

        public CIDENTIFIER(string name) : base(0, GrammarType.NodeType.NT_IDENTIFIER)
        {
            m_name = name;
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCIDENTIFIER(this);
        }
    }

    public class CAnd : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '&&', right=what appears right from the '&&'

        public CAnd() : base(2, GrammarType.NodeType.NT_AND)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCAnd(this);
        }
    }

    public class COr : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'

        public COr() : base(2, GrammarType.NodeType.NT_OR)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCOr(this);
        }
    }

    public class CNot : GrammarASTElement
    {
        public const int CT_RIGHT = 0;


        public CNot() : base(1, GrammarType.NodeType.NT_NOT)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCNot(this);
        }
    }

    public class CGreaterThan : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'

        public CGreaterThan() : base(2, GrammarType.NodeType.NT_GT)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCGreaterThan(this);
        }
    }

    public class CGreaterThanEqual : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'

        public CGreaterThanEqual() : base(2, GrammarType.NodeType.NT_GTE)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCGreaterThanEqual(this);
        }
    }

    public class CLessThan : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'

        public CLessThan() : base(2, GrammarType.NodeType.NT_LT)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCLessThan(this);
        }
    }

    public class CLessThanEqual : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'

        public CLessThanEqual() : base(2, GrammarType.NodeType.NT_LTE)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCLessThanEqual(this);
        }
    }

    public class CEqual : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'

        public CEqual() : base(2, GrammarType.NodeType.NT_EQUAL)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCEqual(this);
        }
    }

    public class CNotEqual : GrammarASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'

        public CNotEqual() : base(2, GrammarType.NodeType.NT_NEQUAL)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCNotEqual(this);
        }
    }


    public class CFunctionDefinition : GrammarASTElement
    {
        public const int CT_FNAME = 0, CT_FARGS = 1, CT_COMPOUNDSTATEMENT = 2;

        public CFunctionDefinition() : base(3, GrammarType.NodeType.NT_FUNCTION)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCFunctionDefinition(this);
        }
    }

    public class CIfStatement : GrammarASTElement
    {
        public const int CT_CONDITION = 0,
            CT_STATEMENT = 1,
            CT_CONDITION2=2,
            CT_STATEMENT2 = 3,
            CT_STATEMENT3 = 4;
        public CIfStatement() : base(5, GrammarType.NodeType.NT_IF)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCIfStatement(this);
        }
    }

   
    public class CWhileStatement : GrammarASTElement
    {
        public const int CT_CONDITION = 0, CT_STATEMENT = 1;

        public CWhileStatement() : base(2, GrammarType.NodeType.NT_WHILE)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCWhileStatement(this);
        }
    }

    public class CDoWhileStatement : GrammarASTElement
    {
        public const int CT_STATEMENT = 0, CT_CONDITION = 1;

        public CDoWhileStatement() : base(2, GrammarType.NodeType.NT_DOWHILE)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCDoWhileStatement(this);
        }
    }

    public class CForWhileStatement : GrammarASTElement
    {
        public const int CT_EXPRESSION = 0, CT_EXPRESSION2 = 1 , CT_EXPRESSION3=2 , CT_STATEMENT=3;

        public CForWhileStatement() : base(4, GrammarType.NodeType.NT_FORLOOP)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCForWhileStatement(this);
        }
    }

    public class CCompoundStatement : GrammarASTElement
    {
        public const int CT_STATEMENTLIST = 0;

        public CCompoundStatement() : base(1, GrammarType.NodeType.NT_COMPOUND)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCCompoundStatement(this);
        }
    }

    public class CExprPlusPlus : GrammarASTElement
    {
        public const int CT_LEFT = 0;

        public CExprPlusPlus() : base(1, GrammarType.NodeType.NT_EXPRPLUSPLUS)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCExprPlusPlus(this);
        }
    }

    public class CPlusPlusExpression : GrammarASTElement
    {
        public const int CT_RIGHT = 0;

        public CPlusPlusExpression() : base(1, GrammarType.NodeType.NT_PLUSPLUSEXPR)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCPlusPlusExpr(this);
        }
    }

    public class CExpressionMinusMInus : GrammarASTElement
    {
        public const int CT_LEFT = 0;

        public CExpressionMinusMInus() : base(1, GrammarType.NodeType.NT_EXPRMINUSMINUS)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCExprMinusMinus(this);
        }
    }

    public class CMinusMInusExpression : GrammarASTElement
    {
        public const int CT_RIGHT = 0;

        public CMinusMInusExpression() : base(1, GrammarType.NodeType.NT_MINUSMINUSEXPR)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCMinusMinusExpr(this);
        }
    }

    public class CReturnStatement : GrammarASTElement
    {
        public const int CT_RETURNVALUE = 0;
        public CReturnStatement() : base(1, GrammarType.NodeType.NT_RETURN)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCReturnStatement(this);
        }
    }

    public class CBreakStatement : GrammarASTElement
    {

        public CBreakStatement() : base(0, GrammarType.NodeType.NT_BREAK)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCBreakStatement(this);
        }
    }

}
