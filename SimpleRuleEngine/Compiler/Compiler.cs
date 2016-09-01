namespace SimpleRuleEngine.Compiler
{
    using Antlr4.Runtime;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class Compiler
    {
        public RuleSet compile(String inputString)
        {
            AntlrInputStream input = new AntlrInputStream(inputString);
            RuleSetGrammarLexer lexer = new RuleSetGrammarLexer(input);
            ITokenStream tokens = new CommonTokenStream(lexer);
            RuleSetGrammarParser parser = new RuleSetGrammarParser(tokens);

            TreeBuilder treeBuilder = new TreeBuilder();
            parser.AddParseListener(treeBuilder);
            parser.AddErrorListener(new ThrowExceptionErrorListener());

            parser.rule_set();

            return treeBuilder.ruleSet;
        }
    }
}
