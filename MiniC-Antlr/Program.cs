using System;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace MiniC_Antlr
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader aStreamReader = new StreamReader(args[0]);

            AntlrInputStream antlrInputStream = new AntlrInputStream(aStreamReader);

            GrammarLexer lexer = new GrammarLexer(antlrInputStream);

            CommonTokenStream tokens = new CommonTokenStream(lexer);

            GrammarParser parser = new GrammarParser(tokens);

            IParseTree tree = parser.compileUnit();

            Console.WriteLine(tree.ToStringTree());

            //STPrinterVisitor stPrinter = new STPrinterVisitor(); 
            //stPrinter.Visit(tree);   
        }
    }
}
