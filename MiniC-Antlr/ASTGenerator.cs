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
        private ASTElement m_root;

        public ASTElement MRoot => m_root;

        private Stack<(ASTElement, int)> m_contextData = new Stack<(ASTElement, int)>();

        public override int VisitST_CompileUnit(GrammarParser.ST_CompileUnitContext context)
        {

            CCompileUnit node = new CCompileUnit();
            m_root = node;

            (ASTElement, int) t = (m_root,CCompileUnit.CT_STATEMENTLIST);

            m_contextData.Push(t);

            foreach (var child in context.statementList())
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

            return 0;
        }

        public override int VisitST_FunctionDefinition(GrammarParser.ST_FunctionDefinitionContext context)
        {
            CFunctionDefinition newnode = new CFunctionDefinition();

            (ASTElement, int) parentData = m_contextData.Peek();
            parentData.Item1.AddChild(newnode,parentData.Item2);

            newnode.MParent = parentData.Item1;

            (ASTElement, int) t;

            t = (newnode, CFunctionDefinition.CT_FNAME);
            m_contextData.Push(t);
            Visit(context.IDENTIFIER());
            m_contextData.Pop();

            t = (newnode, CFunctionDefinition.CT_FARGS);
            m_contextData.Push(t);
            Visit(context.fargs());
            m_contextData.Pop();

            return 0;
        }

        
        public override int VisitST_Return(GrammarParser.ST_ReturnContext context)
        {
            CReturnStatement newnode = new CReturnStatement();

            (ASTElement, int) parentData = m_contextData.Peek();
            parentData.Item1.AddChild(newnode, parentData.Item2);

            newnode.MParent = parentData.Item1;

            (ASTElement, int) t = (newnode, CReturnStatement.CT_RETURNVALUE);
            m_contextData.Push(t);
            Visit(context.expression());
            m_contextData.Pop();
            return base.VisitST_Return(context);
        }

        public override int VisitST_Break(GrammarParser.ST_BreakContext context)
        {
            CBreakStatement newnode = new CBreakStatement();

            (ASTElement, int) parentData = m_contextData.Peek();
            parentData.Item1.AddChild(newnode, parentData.Item2);

            newnode.MParent = parentData.Item1;
            return 0;
        }

        public override int VisitST_If(GrammarParser.ST_IfContext context)
        {
            CIfStatement newnode = new CIfStatement();

            (ASTElement, int) parentData = m_contextData.Peek();
            parentData.Item1.AddChild(newnode, parentData.Item2);

            newnode.MParent = parentData.Item1;

            (ASTElement, int) t;

            t = (newnode, CIfStatement.CT_CONDITION);
            m_contextData.Push(t);
            Visit(context.expression(0));
            m_contextData.Pop();

            t = (newnode, CIfStatement.CT_STATEMENT);
            m_contextData.Push(t);
            Visit(context.statement(0));
            m_contextData.Pop();

            t = (newnode, CIfStatement.CT_CONDITION2);
            m_contextData.Push(t);
            Visit(context.expression(1));
            m_contextData.Pop();

            t = (newnode, CIfStatement.CT_STATEMENT2);
            m_contextData.Push(t);
            Visit(context.statement(2));
            m_contextData.Pop();

            t = (newnode, CIfStatement.CT_STATEMENT3);
            m_contextData.Push(t);
            Visit(context.statement(3));
            m_contextData.Pop();

            return 0;
        }



        public override int VisitST_Switch(GrammarParser.ST_SwitchContext context)
        {
            CSwitch newnode = new CSwitch();

            (ASTElement, int) parentData = m_contextData.Peek();
            parentData.Item1.AddChild(newnode, parentData.Item2);

            newnode.MParent = parentData.Item1;

            (ASTElement, int) t;

            t = (newnode, CSwitch.CT_CONDITION);
            m_contextData.Push(t);
            Visit(context.expression());
            m_contextData.Pop();

            t = (newnode, CSwitch.CT_CASE);
            m_contextData.Push(t);
            foreach (GrammarParser.CaseOptionsContext child in context.caseOptions()) Visit(child);
            m_contextData.Pop();

            if (context.defaultOption() != null)
            {
                t = (newnode, CSwitch.CT_DEFAULT);
                m_contextData.Push(t);
                Visit(context.defaultOption());
                m_contextData.Pop();
            }

            return 0;
        }
        
        public override int VisitST_CaseOptions(GrammarParser.ST_CaseOptionsContext context)
        {
            CCaseOptions newnode = new CCaseOptions();

            (ASTElement, int) parentData = m_contextData.Peek();
            parentData.Item1.AddChild(newnode, parentData.Item2);

            newnode.MParent = parentData.Item1;

            (ASTElement, int) t;

            t = (newnode, CCaseOptions.CT_CASECONDITION);
            m_contextData.Push(t);
            Visit(context.expression());
            m_contextData.Pop();

            t = (newnode, CCaseOptions.CT_STATEMENT);
            m_contextData.Push(t);
            Visit(context.statement());
            m_contextData.Pop();

            return 0;
        }

        public override int VisitST_DefaultOptions(GrammarParser.ST_DefaultOptionsContext context)
        {
            CDefaultOption newnode = new CDefaultOption();

            (ASTElement, int) parentData = m_contextData.Peek();
            parentData.Item1.AddChild(newnode, parentData.Item2);

            newnode.MParent = parentData.Item1;

            (ASTElement, int) t;

            t = (newnode, CDefaultOption.CT_STATEMENT);
            m_contextData.Push(t);
            Visit(context.statement());
            m_contextData.Pop();

            return 0;
        }
        
        public override int VisitST_While(GrammarParser.ST_WhileContext context)
        {
            CWhileStatement newnode = new CWhileStatement();

            (ASTElement, int) parentData = m_contextData.Peek();
            parentData.Item1.AddChild(newnode, parentData.Item2);

            newnode.MParent = parentData.Item1;

            (ASTElement, int) t;

            t = (newnode, CWhileStatement.CT_CONDITION);
            m_contextData.Push(t);
            Visit(context.expression());
            m_contextData.Pop();

            t = (newnode, CWhileStatement.CT_STATEMENTS);
            m_contextData.Push(t);
            Visit(context.statement());
            m_contextData.Pop();
            return 0;
        }

        public override int VisitST_DoWhile(GrammarParser.ST_DoWhileContext context)
        {
            CDoWhileStatement newnode = new CDoWhileStatement();

            (ASTElement, int) parentData = m_contextData.Peek();
            parentData.Item1.AddChild(newnode, parentData.Item2);

            newnode.MParent = parentData.Item1;

            (ASTElement, int) t;

            t = (newnode, CDoWhileStatement.CT_COMPOUNDSTATEMENT);
            m_contextData.Push(t);
            Visit(context.compoundStatement());
            m_contextData.Pop();

            t = (newnode, CDoWhileStatement.CT_CONDITION);
            m_contextData.Push(t);
            Visit(context.expression());
            m_contextData.Pop();
            return 0;
        }

        public override int VisitST_For(GrammarParser.ST_ForContext context)
        {
            CForWhileStatement newnode = new CForWhileStatement();

            (ASTElement, int) parentData = m_contextData.Peek();
            parentData.Item1.AddChild(newnode, parentData.Item2);

            newnode.MParent = parentData.Item1;

            (ASTElement, int) t;

            if (context.expression(0) != null)
            {
                t = (newnode, CForWhileStatement.CT_EXPRESSION);
                m_contextData.Push(t);
                Visit(context.expression(0));
                m_contextData.Pop();
            }

            if (context.expression(1) != null)
            {
                t = (newnode, CForWhileStatement.CT_EXPRESSION2);
                m_contextData.Push(t);
                Visit(context.expression(1));
                m_contextData.Pop();
            }

            if (context.expression(2) != null)
            {
                t = (newnode, CForWhileStatement.CT_EXPRESSION3);
                m_contextData.Push(t);
                Visit(context.expression(2));
                m_contextData.Pop();
            }

            if (context.compoundStatement() != null)
            {
                t = (newnode, CForWhileStatement.CT_COMPOUNDSTATEMENT);
                m_contextData.Push(t);
                Visit(context.compoundStatement());
                m_contextData.Pop();
            }

            return 0;
        }

        public override int VisitTerminal(ITerminalNode node)
        {
            (ASTElement, int) parent_data;
            switch (node.Symbol.Type)
            {
                case GrammarLexer.NUMBER:
                    CNUMBER numNode = new CNUMBER(node.Symbol.Text);
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(numNode, parent_data.Item2);
                    numNode.MParent = parent_data.Item1;
                    break;
                case GrammarLexer.IDENTIFIER:
                    CIDENTIFIER identifierNode = new CIDENTIFIER(node.Symbol.Text);
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(identifierNode, parent_data.Item2);
                    identifierNode.MParent = parent_data.Item1;
                    break;
            }
            return 0;
        }


        public override int VisitExprDIVMULT(GrammarParser.ExprDIVMULTContext context)
        {
            (ASTElement, int) parent_data;
            switch (context.op.Type)
            {
                case GrammarLexer.MULT:
                    CMultiplication multNode = new CMultiplication();
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(multNode, parent_data.Item2);
                    multNode.MParent = parent_data.Item1;

                    m_contextData.Push((multNode, CMultiplication.CT_LEFT));
                    Visit(context.expression(0));
                    m_contextData.Pop();

                    m_contextData.Push((multNode, CMultiplication.CT_RIGHT));
                    Visit(context.expression(1));
                    m_contextData.Pop();
                    break;
                case GrammarLexer.DIV:
                    CDivision divNode = new CDivision();
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(divNode, parent_data.Item2);
                    divNode.MParent = parent_data.Item1;

                    m_contextData.Push((divNode, CDivision.CT_LEFT));
                    Visit(context.expression(0));
                    m_contextData.Pop();

                    m_contextData.Push((divNode, CDivision.CT_RIGHT));
                    Visit(context.expression(1));
                    m_contextData.Pop();

                    break;
            }
            return 0;
        }

        public override int VisitExprPLUSMINUS(GrammarParser.ExprPLUSMINUSContext context)
        {
            (ASTElement, int) parent_data;
            switch (context.op.Type)
            {
                case GrammarLexer.PLUS:
                    CAddition addNode = new CAddition();
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(addNode, parent_data.Item2);
                    addNode.MParent = parent_data.Item1;

                    m_contextData.Push((addNode, CAddition.CT_LEFT));
                    Visit(context.expression(0));
                    m_contextData.Pop();

                    m_contextData.Push((addNode, CAddition.CT_RIGHT));
                    Visit(context.expression(1));
                    m_contextData.Pop();
                    break;
                case GrammarLexer.MINUS:
                    CSubtraction subNode = new CSubtraction();
                    parent_data = m_contextData.Peek();
                    parent_data.Item1.AddChild(subNode, parent_data.Item2);
                    subNode.MParent = parent_data.Item1;

                    m_contextData.Push((subNode, CSubtraction.CT_LEFT));
                    Visit(context.expression(0));
                    m_contextData.Pop();

                    m_contextData.Push((subNode, CSubtraction.CT_RIGHT));
                    Visit(context.expression(1));
                    m_contextData.Pop();

                    break;
            }
            return 0;
        }

        public override int VisitExprPLUSPLUS(GrammarParser.ExprPLUSPLUSContext context)
        {
            CExprPlusPlus node = new CExprPlusPlus();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CExprPlusPlus.CT_LEFT));
            Visit(context.expression());
            m_contextData.Pop();
            return 0;
        }

        public override int VisitPLUSPLUSExpr(GrammarParser.PLUSPLUSExprContext context)
        {
            CPlusPlusExpression node = new CPlusPlusExpression();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CPlusPlusExpression.CT_RIGHT));
            Visit(context.expression());
            m_contextData.Pop();
            return 0;
        }

        public override int VisitExprMINUSMINUS(GrammarParser.ExprMINUSMINUSContext context)
        {
            CExpressionMinusMInus node = new CExpressionMinusMInus();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CExpressionMinusMInus.CT_LEFT));
            Visit(context.expression());
            m_contextData.Pop();
            return 0;
            
        }

        public override int VisitMINUSMINUSExpr(GrammarParser.MINUSMINUSExprContext context)
        {
            CMinusMInusExpression node = new CMinusMInusExpression();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CMinusMInusExpression.CT_RIGHT));
            Visit(context.expression());
            m_contextData.Pop();
            return 0;
        }

        public override int VisitExprASSIGN(GrammarParser.ExprASSIGNContext context)
        {
            CAssignment node = new CAssignment();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CAssignment.CT_LEFT));
            Visit(context.IDENTIFIER());
            m_contextData.Pop();

            m_contextData.Push((node, CAssignment.CT_RIGHT));
            Visit(context.expression());
            m_contextData.Pop();
            return 0;
        }

        public override int VisitExprNOT(GrammarParser.ExprNOTContext context)
        {
            CNot node = new CNot();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CNot.CT_RIGHT));
            Visit(context.expression());
            m_contextData.Pop();
            return 0;
        }

        public override int VisitExprAND(GrammarParser.ExprANDContext context)
        {
            CAnd node = new CAnd();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CAnd.CT_LEFT));
            Visit(context.expression(0));
            m_contextData.Pop();

            m_contextData.Push((node, CAnd.CT_RIGHT));
            Visit(context.expression(1));
            m_contextData.Pop();

            return 0;
        }

        public override int VisitExprOR(GrammarParser.ExprORContext context)
        {
            COr node = new COr();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, COr.CT_LEFT));
            Visit(context.expression(0));
            m_contextData.Pop();

            m_contextData.Push((node, COr.CT_RIGHT));
            Visit(context.expression(1));
            m_contextData.Pop();

            return 0;
        }

        public override int VisitExprGT(GrammarParser.ExprGTContext context)
        {
            CGreaterThan node = new CGreaterThan();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CGreaterThan.CT_LEFT));
            Visit(context.expression(0));
            m_contextData.Pop();

            m_contextData.Push((node, CGreaterThan.CT_RIGHT));
            Visit(context.expression(1));
            m_contextData.Pop();

            return 0;
        }

        public override int VisitExprGTE(GrammarParser.ExprGTEContext context)
        {
            CGreaterThanEqual node = new CGreaterThanEqual();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CGreaterThanEqual.CT_LEFT));
            Visit(context.expression(0));
            m_contextData.Pop();

            m_contextData.Push((node, CGreaterThanEqual.CT_RIGHT));
            Visit(context.expression(1));
            m_contextData.Pop();

            return 0;
        }

        public override int VisitExprLT(GrammarParser.ExprLTContext context)
        {
            CLessThan node = new CLessThan();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CLessThan.CT_LEFT));
            Visit(context.expression(0));
            m_contextData.Pop();

            m_contextData.Push((node, CLessThan.CT_RIGHT));
            Visit(context.expression(1));
            m_contextData.Pop();

            return 0;
        }

        public override int VisitExprLTE(GrammarParser.ExprLTEContext context)
        {
            CLessThanEqual node = new CLessThanEqual();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CLessThanEqual.CT_LEFT));
            Visit(context.expression(0));
            m_contextData.Pop();

            m_contextData.Push((node, CLessThanEqual.CT_RIGHT));
            Visit(context.expression(1));
            m_contextData.Pop();

            return 0;
        }

        public override int VisitExprEQUAL(GrammarParser.ExprEQUALContext context)
        {
            CEqual node = new CEqual();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CEqual.CT_LEFT));
            Visit(context.expression(0));
            m_contextData.Pop();

            m_contextData.Push((node, CEqual.CT_RIGHT));
            Visit(context.expression(1));
            m_contextData.Pop();

            return 0;
        }

        public override int VisitExprNEQUAL(GrammarParser.ExprNEQUALContext context)
        {
            CNotEqual node = new CNotEqual();
            (ASTElement, int) parent_data = m_contextData.Peek();
            parent_data.Item1.AddChild(node, parent_data.Item2);
            node.MParent = parent_data.Item1;

            m_contextData.Push((node, CNotEqual.CT_LEFT));
            Visit(context.expression(0));
            m_contextData.Pop();

            m_contextData.Push((node, CNotEqual.CT_RIGHT));
            Visit(context.expression(1));
            m_contextData.Pop();

            return 0;
        }
    }
}
