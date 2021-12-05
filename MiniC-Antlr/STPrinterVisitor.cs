using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Tree;

namespace MiniC_Antlr
{
    class STPrinterVisitor : GrammarBaseVisitor<int>
    {
        StreamWriter m_STSpecFile = new StreamWriter("ST.dot");
        Stack<string> m_parentsLabel = new Stack<string>();
        static int ms_serialCounter = 0;

        public override int VisitST_CompileUnit(GrammarParser.ST_CompileUnitContext context)
        {
            string label = "CompileUnit" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("digraph G{");  /*we write the prologue of the tree for graphviz*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitCompileUnit(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            m_STSpecFile.WriteLine("}");  /*we write the epilogue of the tree for graphviz*/

            m_STSpecFile.Close();

            //Prepare the process dot to run
            ProcessStartInfo start = new ProcessStartInfo();
            //Enter, in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-Tgif " +
                              Path.GetFileName("ST.dot") + " -o " +
                              Path.GetFileNameWithoutExtension("ST") + ".gif";
            //Enter the executable to run , including the complete path
            start.FileName = "dot";
            //Do you want to show the console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            //Run the external process and wait for it to finish
            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                //Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }



            return 0;
        }

        public override int VisitPLUSPLUSExpr(GrammarParser.PLUSPLUSExprContext context)
        {
            string label = "PLUSPLUSExpr" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitPLUSPLUSExpr(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitMINUSMINUSExpr(GrammarParser.MINUSMINUSExprContext context)
        {
            string label = "MINUSMINUSExpr" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitMINUSMINUSExpr(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitStatement(GrammarParser.StatementContext context)
        {
            string label = "Statement" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitStatement(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_FunctionDefinition(GrammarParser.ST_FunctionDefinitionContext context)
        {
            string label = "FunctionDefinition" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_FunctionDefinition(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_Expression(GrammarParser.ST_ExpressionContext context)
        {
            string label = "Expression" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_Expression(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_selection(GrammarParser.ST_selectionContext context)
        {
            string label = "Selection" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_selection(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_Iteration(GrammarParser.ST_IterationContext context)
        {
            string label = "Iteration" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_Iteration(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_Compound(GrammarParser.ST_CompoundContext context)
        {
            string label = "Compound" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_Compound(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_Return(GrammarParser.ST_ReturnContext context)
        {
            string label = "Return" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_Return(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_Break(GrammarParser.ST_BreakContext context)
        {
            string label = "Break" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_Break(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_If(GrammarParser.ST_IfContext context)
        {
            string label = "If" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_If(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_Switch(GrammarParser.ST_SwitchContext context)
        {
            string label = "Switch" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_Switch(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_CaseOptions(GrammarParser.ST_CaseOptionsContext context)
        {
            string label = "CaseOptions" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_CaseOptions(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_DefaultOptions(GrammarParser.ST_DefaultOptionsContext context)
        {
            string label = "DefaultOptions" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_DefaultOptions(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_While(GrammarParser.ST_WhileContext context)
        {
            string label = "While" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_While(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_DoWhile(GrammarParser.ST_DoWhileContext context)
        {
            string label = "DoWhile" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_DoWhile(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_For(GrammarParser.ST_ForContext context)
        {
            string label = "ForLoop" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_For(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_List(GrammarParser.ST_ListContext context)
        {
            string label = "StatementsList" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_List(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_Arguments(GrammarParser.ST_ArgumentsContext context)
        {
            string label = "Arguments" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_Arguments(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitST_FunctionArguments(GrammarParser.ST_FunctionArgumentsContext context)
        {
            string label = "FunctionArguments" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitST_FunctionArguments(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitExprDIVMULT(GrammarParser.ExprDIVMULTContext context)
        {
            string label = "";

            switch (context.op.Type)
            {
                case GrammarLexer.MULT:
                    label = "Multiplication" + "_" + ms_serialCounter++;
                    break;
                case GrammarLexer.DIV:
                    label = "Division" + "_" + ms_serialCounter++;
                    break;
            }

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprDIVMULT(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0; ;
        }

        public override int VisitExprPLUSMINUS(GrammarParser.ExprPLUSMINUSContext context)
        {
            string label = "";

            switch (context.op.Type)
            {
                case GrammarLexer.PLUS:
                    label = "Addition" + "_" + ms_serialCounter++;
                    break;
                case GrammarLexer.MINUS:
                    label = "Subtraction" + "_" + ms_serialCounter++;
                    break;
            }

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprPLUSMINUS(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0; ;
        }

        public override int VisitExprPLUSPLUS(GrammarParser.ExprPLUSPLUSContext context)
        {
            string label = "ExpressionPlusPlus" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprPLUSPLUS(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitExprMINUSMINUS(GrammarParser.ExprMINUSMINUSContext context)
        {
            string label = "ExpressionMinusMinus" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprMINUSMINUS(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitExprPARENTHESIS(GrammarParser.ExprPARENTHESISContext context)
        {
            string label = "ExpressionParenthesis" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprPARENTHESIS(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitExprASSIGN(GrammarParser.ExprASSIGNContext context)
        {
            string label = "ExpressionAssign" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprASSIGN(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitExprNOT(GrammarParser.ExprNOTContext context)
        {
            string label = "ExpressionNot" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprNOT(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitExprAND(GrammarParser.ExprANDContext context)
        {
            string label = "ExpressionAnd" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprAND(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitExprOR(GrammarParser.ExprORContext context)
        {
            string label = "ExpressionOr" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprOR(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitExprGT(GrammarParser.ExprGTContext context)
        {
            string label = "ExpressionGreaterThan" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprGT(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitExprGTE(GrammarParser.ExprGTEContext context)
        {
            string label = "ExpressionGreaterThanEqual" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprGTE(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitExprLT(GrammarParser.ExprLTContext context)
        {
            string label = "ExpressionLessThan" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprLT(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitExprLTE(GrammarParser.ExprLTEContext context)
        {
            string label = "ExpressionLessThanEqual" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprLTE(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitExprEQUAL(GrammarParser.ExprEQUALContext context)
        {
            string label = "ExpressionEqual" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprEQUAL(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitExprNEQUAL(GrammarParser.ExprNEQUALContext context)
        {
            string label = "ExpressionNotEqual" + "_" + ms_serialCounter++;

            m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  /*graphviz: start node -> destination node*/

            m_parentsLabel.Push(label);  /*we push this node's label*/
            base.VisitExprNEQUAL(context);  /*we visit this node's children*/
            m_parentsLabel.Pop();  /*we pop this node's label*/

            return 0;
        }

        public override int VisitTerminal(ITerminalNode node)
        {
            string label = "";
            switch (node.Symbol.Type)
            {
                case GrammarLexer.NUMBER:
                    label = node.Symbol.Text + "_" + ms_serialCounter++;

                    m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  //graphviz: start node -> destination node
                    break;

                case GrammarLexer.IDENTIFIER:
                    label = node.Symbol.Text + "_" + ms_serialCounter++;

                    m_STSpecFile.WriteLine("\"{0}\"->\"{1}\";", m_parentsLabel.Peek(), label);  //graphviz: start node -> destination node
                    break;
            }


            return base.VisitTerminal(node);
        }


    }
}
