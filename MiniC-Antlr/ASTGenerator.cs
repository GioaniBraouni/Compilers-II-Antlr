using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Tree;

namespace MiniC_Antlr
{
    class ASTGenerator :GrammarBaseVisitor<int>
    {
        private GrammarASTElement m_root;

        public GrammarASTElement MRoot => m_root;

        private Stack<(GrammarASTElement, int)> m_contextData = new Stack<(GrammarASTElement, int)>();

        public override int VisitST_CompileUnit(GrammarParser.ST_CompileUnitContext context)
        {

            CCompileUnit node = new CCompileUnit();
            m_root = node;

            (GrammarASTElement, int) t = (m_root,CCompileUnit.CT_STATEMENTS);
            m_contextData.Push(t);
            foreach (var child in context.statement())
            {
                Visit(child);
            }
            m_contextData.Pop();

            t = (m_root, CCompileUnit.CT_FUNCTIONDEFINITION);
            m_contextData.Push(t);
            foreach (var child in context.functionDefinition())
            {
                Visit(child);
            }
            m_contextData.Pop();
            return base.VisitST_CompileUnit(context);
        }

        public override int VisitST_FunctionDefinition(GrammarParser.ST_FunctionDefinitionContext context)
        {
            CFunctionDefinition newnode = new CFunctionDefinition();

            (GrammarASTElement, int) parentData = m_contextData.Peek();
            parentData.Item1.AddChild(newnode,parentData.Item2);

            newnode.MParent = parentData.Item1;

            (GrammarASTElement, int) t;

            t = (m_root, CFunctionDefinition.CT_FNAME);
            m_contextData.Push(t);
            Visit(context.IDENTIFIER());
            m_contextData.Pop();

            t = (m_root, CFunctionDefinition.CT_FARGS);
            m_contextData.Push(t);
            Visit(context.fargs());
            m_contextData.Pop();

            t = (m_root, CFunctionDefinition.CT_COMPOUNDSTATEMENT);
            m_contextData.Push(t);
            Visit(context.compoundStatement());
            m_contextData.Pop();

            return base.VisitST_FunctionDefinition(context);
        }

        public override int VisitST_Return(GrammarParser.ST_ReturnContext context)
        {
            return base.VisitST_Return(context);
        }

        public override int VisitST_Break(GrammarParser.ST_BreakContext context)
        {

            return base.VisitST_Break(context);
        }

        public override int VisitST_If(GrammarParser.ST_IfContext context)
        {
            CIfStatement newnode = new CIfStatement();

            (GrammarASTElement, int) parentData = m_contextData.Peek();
            parentData.Item1.AddChild(newnode, parentData.Item2);

            newnode.MParent = parentData.Item1;

            (GrammarASTElement, int) t;

            t = (m_root, CIfStatement.CT_CONDITION);
            m_contextData.Push(t);
            foreach (var expressionContext in context.expression()) Visit(expressionContext);
            m_contextData.Pop();

            t = (m_root, CIfStatement.CT_COMPOUNDSTATEMENT);
            m_contextData.Push(t);
            foreach (GrammarParser.StatementContext statementContext in context.statement()) Visit(statementContext);
            m_contextData.Pop();

            t = (m_root, CIfStatement.CT_CONDITION2);
            m_contextData.Push(t);
            foreach (var expressionContext in context.expression()) Visit(expressionContext);
            m_contextData.Pop();

            t = (m_root, CIfStatement.CT_COMPOUNDSTATEMENT2);
            m_contextData.Push(t);
            foreach (GrammarParser.StatementContext statementContext in context.statement()) Visit(statementContext);
            m_contextData.Pop();

            t = (m_root, CIfStatement.CT_COMPOUNDSTATEMENT3);
            m_contextData.Push(t);
            foreach (GrammarParser.StatementContext statementContext in context.statement()) Visit(statementContext);
            m_contextData.Pop();

            return base.VisitST_If(context);
        }

        public override int VisitST_Switch(GrammarParser.ST_SwitchContext context)
        {
            CSwitch newnode = new CSwitch();

            (GrammarASTElement, int) parentData = m_contextData.Peek();
            parentData.Item1.AddChild(newnode, parentData.Item2);

            newnode.MParent = parentData.Item1;

            (GrammarASTElement, int) t;

            t = (m_root, CSwitch.CT_CONDITION);
            m_contextData.Push(t);
            Visit(context.expression());
            m_contextData.Pop();
            return base.VisitST_Switch(context);
        }

        public override int VisitST_CaseOptions(GrammarParser.ST_CaseOptionsContext context)
        {
            return base.VisitST_CaseOptions(context);
        }

        public override int VisitST_DefaultOptions(GrammarParser.ST_DefaultOptionsContext context)
        {
            return base.VisitST_DefaultOptions(context);
        }

        public override int VisitST_While(GrammarParser.ST_WhileContext context)
        {
            return base.VisitST_While(context);
        }

        public override int VisitST_DoWhile(GrammarParser.ST_DoWhileContext context)
        {
            return base.VisitST_DoWhile(context);
        }

        public override int VisitST_For(GrammarParser.ST_ForContext context)
        {
            return base.VisitST_For(context);
        }

        public override int VisitExprDIVMULT(GrammarParser.ExprDIVMULTContext context)
        {
            return base.VisitExprDIVMULT(context);
        }

        public override int VisitExprPLUSMINUS(GrammarParser.ExprPLUSMINUSContext context)
        {
            return base.VisitExprPLUSMINUS(context);
        }

        public override int VisitExprPLUSPLUS(GrammarParser.ExprPLUSPLUSContext context)
        {
            return base.VisitExprPLUSPLUS(context);
        }

        public override int VisitPLUSPLUSExpr(GrammarParser.PLUSPLUSExprContext context)
        {
            return base.VisitPLUSPLUSExpr(context);
        }

        public override int VisitMINUSMINUSExpr(GrammarParser.MINUSMINUSExprContext context)
        {
            return base.VisitMINUSMINUSExpr(context);
        }

        public override int VisitExprMINUSMINUS(GrammarParser.ExprMINUSMINUSContext context)
        {
            return base.VisitExprMINUSMINUS(context);
        }

        public override int VisitExprASSIGN(GrammarParser.ExprASSIGNContext context)
        {
            return base.VisitExprASSIGN(context);
        }

        public override int VisitExprNOT(GrammarParser.ExprNOTContext context)
        {
            return base.VisitExprNOT(context);
        }

        public override int VisitExprAND(GrammarParser.ExprANDContext context)
        {
            return base.VisitExprAND(context);
        }

        public override int VisitExprOR(GrammarParser.ExprORContext context)
        {
            return base.VisitExprOR(context);
        }

        public override int VisitExprGT(GrammarParser.ExprGTContext context)
        {
            return base.VisitExprGT(context);
        }

        public override int VisitExprGTE(GrammarParser.ExprGTEContext context)
        {
            return base.VisitExprGTE(context);
        }

        public override int VisitExprLT(GrammarParser.ExprLTContext context)
        {
            return base.VisitExprLT(context);
        }

        public override int VisitExprLTE(GrammarParser.ExprLTEContext context)
        {
            return base.VisitExprLTE(context);
        }

        public override int VisitExprEQUAL(GrammarParser.ExprEQUALContext context)
        {
            return base.VisitExprEQUAL(context);
        }

        public override int VisitExprNEQUAL(GrammarParser.ExprNEQUALContext context)
        {
            return base.VisitExprNEQUAL(context);
        }

        public override int VisitST_Arguments(GrammarParser.ST_ArgumentsContext context)
        {
            return base.VisitST_Arguments(context);
        }

        public override int VisitST_FunctionArguments(GrammarParser.ST_FunctionArgumentsContext context)
        {
            return base.VisitST_FunctionArguments(context);
        }

        public override int VisitTerminal(ITerminalNode node)
        {
            return base.VisitTerminal(node);
        }
    }
}
