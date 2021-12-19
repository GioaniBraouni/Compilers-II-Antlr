using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniC_Antlr
{
    //Αναπαριστα το περασμα στο AST,χωρις να γνωριζει την γραμματικη
    public class ASTBaseVisitor <T>
    {
        public virtual T Visit(ASTElement node)
        {
            return node.Accept(this);   
        }

        public virtual T VisitChildren(ASTElement node)
        {
            for (int i = 0; i < node.getContextNumber(); i++)
            {
                foreach (ASTElement child in node.GetChildren(i))
                {
                    Visit(child);
                }
            }
            return default(T);
        }
    }

    public class GrammarBaseASTVisitor<T> : ASTBaseVisitor<T>
    {
        public virtual T VisitCCompileUnit(CCompileUnit node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCSwitch(CSwitch node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCCaseOptions(CCaseOptions node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCDefaultOption(CDefaultOption node)
        {
            return VisitChildren(node);
        }


        public virtual T VisitCAssignment(CAssignment node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCAddition(CAddition node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCSubtraction(CSubtraction node)
        {
            return VisitChildren(node);
        }


        public virtual T VisitCMultiplication(CMultiplication node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCDivision(CDivision node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCNUMBER(CNUMBER node)
        {
            return default(T);
        }

        public virtual T VisitCIDENTIFIER(CIDENTIFIER node)
        {
            return default(T);
        }

        public virtual T VisitCAnd(CAnd node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCOr(COr node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCNot(CNot node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCGreaterThan(CGreaterThan node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCGreaterThanEqual(CGreaterThanEqual node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCLessThan(CLessThan node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCLessThanEqual(CLessThanEqual node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCEqual(CEqual node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCNotEqual(CNotEqual node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCFunctionDefinition(CFunctionDefinition node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCIfStatement(CIfStatement node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCWhileStatement(CWhileStatement node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCDoWhileStatement(CDoWhileStatement node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCForWhileStatement(CForWhileStatement node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCReturnStatement(CReturnStatement node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCBreakStatement(CBreakStatement node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCExprPlusPlus(CExprPlusPlus node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCPlusPlusExpr(CPlusPlusExpression node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCExprMinusMinus(CExpressionMinusMInus node)
        {
            return VisitChildren(node);
        }

        public virtual T VisitCMinusMinusExpr(CMinusMInusExpression node)
        {
            return VisitChildren(node);
        }



    }
}
