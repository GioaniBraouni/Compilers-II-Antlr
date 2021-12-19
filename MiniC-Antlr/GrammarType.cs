using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniC_Antlr
{
    
    /*
    NT_FUNCTION ,NT_RETURN, NT_IF, NT_ELSEIF, NT_ELSE, NT_SWITCH, NT_CASE, 
    NT_DEFAULT, NT_WHILE, NT_DOWHILE, NT_FORLOOP, NT_BREAK, NT_ADDITION, NT_SUBTRACTION, NT_DIVISION, NT_MULTIPLICATION, 
    NT_OR, NT_AND, NT_NOT, NT_EQUAL, NT_NEQUAL, NT_GT, NT_LT, NT_GTE, NT_LTE, NT_QM, 
    NT_LP, NT_RP, NT_LB, NT_RB, NT_COMMA, NT_ASSIGNMENT, NT_COLON, NT_IDENTIFIER, NT_NUMBER,
    */

    public class CCompileUnit : ASTElement
    {
        public const int CT_STATEMENTLIST = 0, CT_FUNCTIONDEFINITION = 1;

        public static readonly string[] msc_contextNames = {"StatementList", "FunctionDefinition"}; 

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCCompileUnit(this);
        }

        public CCompileUnit() : base(NodeType.NT_COMPILEUNIT,2)
        {

        }

    }

    public class CCaseOptions : ASTElement
    {
        public const int CT_CASECONDITION = 0, CT_STATEMENT = 1;
        public static readonly string[] msc_contextNames = { "CaseCondition", "StatementContext" };
        public CCaseOptions() : base(NodeType.NT_CASEOPTIONS,2)
        {

        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCCaseOptions(this);
        }
    }

    public class CAssignment : ASTElement
    {
        public const int CT_LEFT = 0,CT_RIGHT=1;
        public static readonly string[] msc_contextNames = { "Left", "Right" };
        public CAssignment() : base(NodeType.NT_ASSIGNMENT,2)
        {
            
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCAssignment(this);
        }
    }

    public class CAddition : ASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '+', right=what appears right from the '+'
        public static readonly string[] msc_contextNames = { "Left", "Right" };
        public CAddition() : base(NodeType.NT_ADDITION,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCAddition(this);
        }
    }

    public class CSubtraction : ASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '-', right=what appears right from the '-'
        public static readonly string[] msc_contextNames = { "Left", "Right" };
        public CSubtraction() : base(NodeType.NT_SUBTRACTION,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCSubtraction(this);
        }
    }

    public class CSwitch : ASTElement
    {
        public const int CT_CONDITION = 0, CT_CASE = 1,CT_DEFAULT=2;   //left=what appears left from the '-', right=what appears right from the '-'
        public static readonly string[] msc_contextNames = { "SwitchCondition", "CaseBody","DefaultBody" };
        public CSwitch() : base(NodeType.NT_SWITCHCASE,3)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCSwitch(this);
        }
    }


    public class CMultiplication : ASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '*', right=what appears right from the '*'
        public static readonly string[] msc_contextNames = { "Left", "Right" };
        public CMultiplication() : base(NodeType.NT_MULTIPLICATION,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCMultiplication(this);
        }
    }

    public class CDivision : ASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '/', right=what appears right from the '/'
        public static readonly string[] msc_contextNames = { "Left", "Right" };
        public CDivision() : base( NodeType.NT_DIVISION,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCDivision(this);
        }
    }

    public class CDefaultOption : ASTElement
    {
        public const int CT_STATEMENT = 0;
        public static readonly string[] msc_contextNames = { "StatementContext" };
        public CDefaultOption() : base(NodeType.NT_DEFAULTOPTION,1)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCDefaultOption(this);
        }
    }

    public class CNUMBER : ASTElement
    {
        private string m_text;
        private int m_value;

        public string MName1 => m_text;

        public CNUMBER(string name) : base(NodeType.NT_NUMBER,0)
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

    public class CIDENTIFIER : ASTElement
    {
        private string m_name;

        public string MName1 => m_name;

        public CIDENTIFIER(string name) : base(NodeType.NT_IDENTIFIER,0)
        {
            m_name = name;
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCIDENTIFIER(this);
        }
    }

    public class CAnd : ASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '&&', right=what appears right from the '&&'
        public static readonly string[] msc_contextNames = { "Left", "Right" };
        public CAnd() : base(NodeType.NT_AND,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCAnd(this);
        }
    }

    public class COr : ASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'
        public static readonly string[] msc_contextNames = { "Left", "Right" };
        public COr() : base(NodeType.NT_OR,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCOr(this);
        }
    }

    public class CNot : ASTElement
    {
        public const int CT_RIGHT = 0;

        public static readonly string[] msc_contextNames = { "Right" };
        public CNot() : base(NodeType.NT_NOT,1)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCNot(this);
        }
    }

    public class CGreaterThan : ASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'
        public static readonly string[] msc_contextNames = { "Left", "Right" };
        public CGreaterThan() : base(NodeType.NT_GT,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCGreaterThan(this);
        }
    }

    public class CGreaterThanEqual : ASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'
        public static readonly string[] msc_contextNames = { "Left", "Right" };
        public CGreaterThanEqual() : base(NodeType.NT_GTE,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCGreaterThanEqual(this);
        }
    }

    public class CLessThan : ASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'
        public static readonly string[] msc_contextNames = { "Left", "Right" };
        public CLessThan() : base(NodeType.NT_LT,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCLessThan(this);
        }
    }

    public class CLessThanEqual : ASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'
        public static readonly string[] msc_contextNames = { "Left", "Right" };
        public CLessThanEqual() : base(NodeType.NT_LTE,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCLessThanEqual(this);
        }
    }

    public class CEqual : ASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'
        public static readonly string[] msc_contextNames = { "Left", "Right" };
        public CEqual() : base(NodeType.NT_EQUAL,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCEqual(this);
        }
    }

    public class CNotEqual : ASTElement
    {
        public const int CT_LEFT = 0, CT_RIGHT = 1;   //left=what appears left from the '||', right=what appears right from the '||'
        public static readonly string[] msc_contextNames = { "Left", "Right" };
        public CNotEqual() : base(NodeType.NT_NEQUAL,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCNotEqual(this);
        }
    }


    public class CFunctionDefinition : ASTElement
    {
        public const int CT_FNAME = 0, CT_FARGS = 1;
        public static readonly string[] msc_contextNames = { "FunctionName", "FunctionArgs"};
        public CFunctionDefinition() : base(NodeType.NT_FUNCTIONDEFINITION,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCFunctionDefinition(this);
        }
    }

    public class CIfStatement : ASTElement
    {
        public const int CT_CONDITION = 0,
            CT_STATEMENT = 1,
            CT_CONDITION2=2,
            CT_STATEMENT2 = 3,
            CT_STATEMENT3 = 4;
        public static readonly string[] msc_contextNames = { "IfCondition", "IfBody","ElseIfCondition","ElseIfBody","ElseBody" };
        public CIfStatement() : base(NodeType.NT_IFSTATEMENT,5)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCIfStatement(this);
        }
    }

   
    public class CWhileStatement : ASTElement
    {
        public const int CT_CONDITION = 0, CT_STATEMENTS = 1;
        public static readonly string[] msc_contextNames = { "WhileCondition", "WhileBody" };
        public CWhileStatement() : base(NodeType.NT_WHILESTATEMENT,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCWhileStatement(this);
        }
    }

    public class CDoWhileStatement : ASTElement
    {
        public const int CT_COMPOUNDSTATEMENT = 0, CT_CONDITION = 1;
        public static readonly string[] msc_contextNames = { "DoWhileBody", "DoWhileCondition" };
        public CDoWhileStatement() : base(NodeType.NT_DOWHILESTATEMENT,2)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCDoWhileStatement(this);
        }
    }

    public class CForWhileStatement : ASTElement
    {
        public const int CT_EXPRESSION = 0, CT_EXPRESSION2 = 1 , CT_EXPRESSION3=2 , CT_COMPOUNDSTATEMENT=3;
        public static readonly string[] msc_contextNames = { "ForExpr1", "ForExpr2", "ForExpr3", "ForBody" };
        public CForWhileStatement() : base(NodeType.NT_FORLOOPSTATEMENT,4)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCForWhileStatement(this);
        }
    }

    
    public class CExprPlusPlus : ASTElement
    {
        public const int CT_LEFT = 0;
        public static readonly string[] msc_contextNames = { "Left" };
        public CExprPlusPlus() : base (NodeType.NT_EXPRMINUSMINUS,1)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCExprPlusPlus(this);
        }
    }

    public class CPlusPlusExpression : ASTElement
    {
        public const int CT_RIGHT = 0;
        public static readonly string[] msc_contextNames = { "Right" };
        public CPlusPlusExpression() : base(NodeType.NT_PLUSPLUSEXPR,1)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCPlusPlusExpr(this);
        }
    }

    public class CExpressionMinusMInus : ASTElement
    {
        public const int CT_LEFT = 0;
        public static readonly string[] msc_contextNames = { "Left" };
        public CExpressionMinusMInus() : base(NodeType.NT_EXPRMINUSMINUS,1)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCExprMinusMinus(this);
        }
    }

    public class CMinusMInusExpression : ASTElement
    {
        public const int CT_RIGHT = 0;
        public static readonly string[] msc_contextNames = { "Right" };
        public CMinusMInusExpression() : base(NodeType.NT_MINUSMINUSEXPR,1)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCMinusMinusExpr(this);
        }
    }

    public class CReturnStatement : ASTElement
    {
        public const int CT_RETURNVALUE = 0;
        public static readonly string[] msc_contextNames = { "Return" };
        public CReturnStatement() : base(NodeType.NT_RETURN_STATEMENT,1)
        {
        }

        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCReturnStatement(this);
        }
    }

    public class CBreakStatement : ASTElement
    {

        public CBreakStatement() : base(NodeType.NT_BREAK_STATEMENT,0)
        {
        }
        public static readonly string[] msc_contextNames = { "Break" };
        public override T Accept<T>(ASTBaseVisitor<T> visitor)
        {
            GrammarBaseASTVisitor<T> grammarVisitor = visitor as GrammarBaseASTVisitor<T>;
            return grammarVisitor.VisitCBreakStatement(this);
        }
    }

}
