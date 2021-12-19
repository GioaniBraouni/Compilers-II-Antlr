using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniC_Antlr
{
    class ASTPrinterVisitor :GrammarBaseASTVisitor<int>
    {
        private StreamWriter m_dotFile;
        private ASTElement m_root;
        private String m_dotFileName;
        private static int ms_clusterSerial = 0;
        private Stack<ASTElement> m_parentStack = new Stack<ASTElement>();

        public ASTPrinterVisitor(string mDotFileName)
        {
            m_dotFileName = mDotFileName;
            m_root = null;
            m_dotFile = null;
        }

        //Dont print edges , only the context(cluster)
        public void ExtractSubgraphs(ASTElement element , int context,string[] contextNames)
        {
            m_dotFile.WriteLine($"subgraph cluster{ms_clusterSerial++} {{" );
            m_dotFile.WriteLine("node [style=filled,color=white];");
            m_dotFile.WriteLine("style=filled;");
            m_dotFile.WriteLine("color=lightgrey;");

            foreach (ASTElement c in element.GetChildren(context))
            {
                m_dotFile.WriteLine($"{c.MName};"); 
            }

            m_dotFile.WriteLine($"label= \"{contextNames[context]}\";");
            m_dotFile.WriteLine("}");
        }

        public override int VisitCCompileUnit(CCompileUnit node)
        {
            //Open dotFile
            m_dotFile = new StreamWriter(m_dotFileName);

            m_dotFile.WriteLine("digraph {");
            //Generate edge with parent (ommited here)

            //Generate contexts
            ExtractSubgraphs(node, CCompileUnit.CT_STATEMENTLIST, CCompileUnit.msc_contextNames);
            ExtractSubgraphs(node, CCompileUnit.CT_FUNCTIONDEFINITION, CCompileUnit.msc_contextNames);
            //Visit contexts

            base.VisitCCompileUnit(node);

            //Close dotFile

            m_dotFile.WriteLine("}");
            m_dotFile.Close();
            //Call graphviz to print tree
            ProcessStartInfo start = new ProcessStartInfo();
            //Enter, in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = "-Tgif " +
                              Path.GetFileName("ast.dot") + " -o " +
                              Path.GetFileNameWithoutExtension("ast") + ".gif";
            //Enter the executable to run , including the complete path
            start.FileName = "dot";
            //Do you want to show the console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            using (Process proc = Process.Start(start))
            {
                proc.WaitForExit();

                //Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }

            return 0;
        }

        public override int VisitCSwitch(CSwitch node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CSwitch.CT_CONDITION, CSwitch.msc_contextNames);
            ExtractSubgraphs(node, CSwitch.CT_CASE, CSwitch.msc_contextNames);
            ExtractSubgraphs(node, CSwitch.CT_DEFAULT, CSwitch.msc_contextNames);


            base.VisitCSwitch(node);
            return 0;
        }

        public override int VisitCCaseOptions(CCaseOptions node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CCaseOptions.CT_CASECONDITION, CCaseOptions.msc_contextNames);
            ExtractSubgraphs(node, CCaseOptions.CT_STATEMENT, CCaseOptions.msc_contextNames);
            
            base.VisitCCaseOptions(node);
            return 0;
        }

        public override int VisitCDefaultOption(CDefaultOption node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CDefaultOption.CT_STATEMENT, CDefaultOption.msc_contextNames);
            
            base.VisitCDefaultOption(node);
            return 0;
        }

        public override int VisitCAssignment(CAssignment node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CAssignment.CT_LEFT, CAssignment.msc_contextNames);
            ExtractSubgraphs(node, CAssignment.CT_RIGHT, CAssignment.msc_contextNames);


            base.VisitCAssignment(node);
            return 0;
        }

        public override int VisitCAddition(CAddition node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CAddition.CT_LEFT, CAddition.msc_contextNames);
            ExtractSubgraphs(node, CAddition.CT_RIGHT, CAddition.msc_contextNames);


            base.VisitCAddition(node);
            return 0;
        }

        public override int VisitCSubtraction(CSubtraction node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CSubtraction.CT_LEFT, CSubtraction.msc_contextNames);
            ExtractSubgraphs(node, CSubtraction.CT_RIGHT, CSubtraction.msc_contextNames);


            base.VisitCSubtraction(node);
            return 0;
        }

        public override int VisitCMultiplication(CMultiplication node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CMultiplication.CT_LEFT, CMultiplication.msc_contextNames);
            ExtractSubgraphs(node, CMultiplication.CT_RIGHT, CMultiplication.msc_contextNames);


            base.VisitCMultiplication(node);
            return 0;
        }

        public override int VisitCDivision(CDivision node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CDivision.CT_LEFT, CDivision.msc_contextNames);
            ExtractSubgraphs(node, CDivision.CT_RIGHT, CDivision.msc_contextNames);


            base.VisitCDivision(node);
            return 0;
        }

        public override int VisitCNUMBER(CNUMBER node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            return 0;
        }

        public override int VisitCIDENTIFIER(CIDENTIFIER node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            return 0;
        }

        public override int VisitCAnd(CAnd node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CAnd.CT_LEFT, CAnd.msc_contextNames);
            ExtractSubgraphs(node, CAnd.CT_RIGHT, CAnd.msc_contextNames);


            base.VisitCAnd(node);
            return 0;
        }

        public override int VisitCOr(COr node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, COr.CT_LEFT, COr.msc_contextNames);
            ExtractSubgraphs(node, COr.CT_RIGHT, COr.msc_contextNames);


            base.VisitCOr(node);
            return 0;
        }

        public override int VisitCNot(CNot node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CNot.CT_RIGHT, CNot.msc_contextNames);


            base.VisitCNot(node);
            return 0;
        }

        public override int VisitCGreaterThan(CGreaterThan node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CGreaterThan.CT_LEFT, CGreaterThan.msc_contextNames);
            ExtractSubgraphs(node, CGreaterThan.CT_RIGHT, CGreaterThan.msc_contextNames);


            base.VisitCGreaterThan(node);
            return 0;
        }

        public override int VisitCGreaterThanEqual(CGreaterThanEqual node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CGreaterThanEqual.CT_LEFT, CGreaterThanEqual.msc_contextNames);
            ExtractSubgraphs(node, CGreaterThanEqual.CT_RIGHT, CGreaterThanEqual.msc_contextNames);


            base.VisitCGreaterThanEqual(node);
            return 0;
        }

        public override int VisitCLessThan(CLessThan node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CLessThan.CT_LEFT, CLessThan.msc_contextNames);
            ExtractSubgraphs(node, CLessThan.CT_RIGHT, CLessThan.msc_contextNames);


            base.VisitCLessThan(node);
            return 0;
        }

        public override int VisitCLessThanEqual(CLessThanEqual node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CLessThanEqual.CT_LEFT, CLessThanEqual.msc_contextNames);
            ExtractSubgraphs(node, CLessThanEqual.CT_RIGHT, CLessThanEqual.msc_contextNames);


            base.VisitCLessThanEqual(node);
            return 0;
        }

        public override int VisitCEqual(CEqual node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CEqual.CT_LEFT, CEqual.msc_contextNames);
            ExtractSubgraphs(node, CEqual.CT_RIGHT, CEqual.msc_contextNames);


            base.VisitCEqual(node);
            return 0;
        }

        public override int VisitCNotEqual(CNotEqual node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CNotEqual.CT_LEFT, CNotEqual.msc_contextNames);
            ExtractSubgraphs(node, CNotEqual.CT_RIGHT, CNotEqual.msc_contextNames);


            base.VisitCNotEqual(node);
            return 0;
        }


        public override int VisitCFunctionDefinition(CFunctionDefinition node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CFunctionDefinition.CT_FNAME, CFunctionDefinition.msc_contextNames);
            ExtractSubgraphs(node, CFunctionDefinition.CT_FARGS, CFunctionDefinition.msc_contextNames);
            
            base.VisitCFunctionDefinition(node);
            return 0;
        }

        public override int VisitCIfStatement(CIfStatement node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CIfStatement.CT_CONDITION, CIfStatement.msc_contextNames);
            ExtractSubgraphs(node, CIfStatement.CT_STATEMENT, CIfStatement.msc_contextNames);
            ExtractSubgraphs(node, CIfStatement.CT_CONDITION2, CIfStatement.msc_contextNames);
            ExtractSubgraphs(node, CIfStatement.CT_CONDITION2, CIfStatement.msc_contextNames);
            ExtractSubgraphs(node, CIfStatement.CT_STATEMENT3, CIfStatement.msc_contextNames);

            base.VisitCIfStatement(node);
            return 0;
        }

        public override int VisitCWhileStatement(CWhileStatement node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CWhileStatement.CT_CONDITION, CWhileStatement.msc_contextNames);
            ExtractSubgraphs(node, CWhileStatement.CT_STATEMENTS, CWhileStatement.msc_contextNames);
            
            base.VisitCWhileStatement(node);
            return 0;
        }

        public override int VisitCDoWhileStatement(CDoWhileStatement node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CDoWhileStatement.CT_STATEMENT, CDoWhileStatement.msc_contextNames);
            ExtractSubgraphs(node, CDoWhileStatement.CT_CONDITION, CDoWhileStatement.msc_contextNames);
            
            base.VisitCDoWhileStatement(node);
            return 0;
        }

        public override int VisitCForWhileStatement(CForWhileStatement node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CForWhileStatement.CT_EXPRESSION, CForWhileStatement.msc_contextNames);
            ExtractSubgraphs(node, CForWhileStatement.CT_EXPRESSION2, CForWhileStatement.msc_contextNames);
            ExtractSubgraphs(node, CForWhileStatement.CT_EXPRESSION3, CForWhileStatement.msc_contextNames);
            ExtractSubgraphs(node, CForWhileStatement.CT_STATEMENT, CForWhileStatement.msc_contextNames);

            base.VisitCForWhileStatement(node);
            return 0;
        }

        public override int VisitCReturnStatement(CReturnStatement node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            return 0;
        }

        public override int VisitCBreakStatement(CBreakStatement node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            return 0;
        }

        public override int VisitCExprPlusPlus(CExprPlusPlus node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CExprPlusPlus.CT_LEFT, CExprPlusPlus.msc_contextNames);
            
            base.VisitCExprPlusPlus(node);
            return 0;
        }

        public override int VisitCPlusPlusExpr(CPlusPlusExpression node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CPlusPlusExpression.CT_RIGHT, CPlusPlusExpression.msc_contextNames);

            base.VisitCPlusPlusExpr(node);
            return 0;
        }

        public override int VisitCExprMinusMinus(CExpressionMinusMInus node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CExpressionMinusMInus.CT_LEFT, CExpressionMinusMInus.msc_contextNames);

            base.VisitCExprMinusMinus(node);
            return 0;
        }

        public override int VisitCMinusMinusExpr(CMinusMInusExpression node)
        {
            //Generate edge with parent (ommited here)
            m_dotFile.WriteLine($"{node.MParent.MName}->{node.MName};");
            //Generate contexts
            ExtractSubgraphs(node, CMinusMInusExpression.CT_RIGHT, CMinusMInusExpression.msc_contextNames);

            base.VisitCMinusMinusExpr(node);
            return 0;
        }
    }
}
