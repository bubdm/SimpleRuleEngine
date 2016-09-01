using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.Collections.Generic;
using SimpleRuleEngine.Models;
using SimpleRuleEngine.Compiler;


namespace SimpleRuleEngine.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRule()
        {
            List<Tuple<bool, string>> tests = new List<Tuple<bool, string>>() {

                Tuple.Create<bool,string>(true,"if true then conclusion;"),
                Tuple.Create<bool,string>(true, "if false then conclusion;"),
                Tuple.Create<bool, string>(true, "if (true) then conclusion;"),
                Tuple.Create<bool, string>(true, "if (false) then conclusion;"),


                Tuple.Create<bool,string>(true, "if a then conclusion;"),
                Tuple.Create<bool, string>(true, "if (a) then conclusion;"),
                Tuple.Create<bool, string>(true, "if a or b then conclusion;"),
               Tuple.Create<bool, string>(true, "if a and b then conclusion;"),
                Tuple.Create<bool, string>(true, "if (a or b) and c then conclusion;"),
                Tuple.Create<bool, string>(true, "if a or b and c then conclusion;"),

                Tuple.Create<bool, string>(true, "if a_1 or b_2 and c_3_aaa then conclusion;"),

                Tuple.Create<bool, string>(true, "if a >= b then conclusion;"),
                Tuple.Create<bool, string>(true, "if a > b then conclusion;"),
                Tuple.Create<bool, string>(true, "if a = b then conclusion;"),
                Tuple.Create<bool, string>(true, "if a < b then conclusion;"),
                Tuple.Create<bool, string>(true, "if a <= b then conclusion;"),
                Tuple.Create<bool, string>(true, "if a > 0.1 then conclusion;"),
                Tuple.Create<bool, string>(true, "if 1.12 <= b then conclusion;"),
                Tuple.Create<bool, string>(true, "if 0.1 = 4 then conclusion;"),
                Tuple.Create<bool, string>(true, "if 5 = 5.0 then conclusion;"),
                Tuple.Create<bool, string>(true, "if a + 5 * b <= c / 12.0 - 1 then conclusion;"),
                Tuple.Create<bool, string>(true, "if (a) > (1.23) * (1 + 4) then conclusion;"),
                Tuple.Create<bool, string>(true, "if 1 < 4 and true then conclusion;"),
                Tuple.Create<bool, string>(true, "if -(a * b) < 12 and true then conclusion;"),

                Tuple.Create<bool, string>(true, ""), // empty rule file
                Tuple.Create<bool, string>(true, "//  comment in a rule file"),
                Tuple.Create<bool, string>(true, "//  comment with new line\n"),


                /* Invalid rules. */
                 Tuple.Create<bool, string>(false, "true"),
                 Tuple.Create<bool, string>(false, "false"),

                 Tuple.Create<bool, string>(false, "if true then conclusion"), // No semicolon

                Tuple.Create<bool, string>(false, "if then conclusion;"),
                Tuple.Create<bool, string>(false, "if true conclusion;"),
                Tuple.Create<bool, string>(false, "if then;"),
                Tuple.Create<bool, string>(false, "if;"),
                Tuple.Create<bool, string>(false, "if"),
                Tuple.Create<bool, string>(false, "then;"),
                Tuple.Create<bool, string>(false, "then"),

                Tuple.Create<bool, string>(false, "a + b"),

                Tuple.Create<bool, string>(false, "if a ++ b then conclusion;"),
                Tuple.Create<bool, string>(false, "if a + (+b) then conclusion;"),

                Tuple.Create<bool, string>(false, "true and false;"),

                Tuple.Create<bool, string>(false, "if abc $ 123 then conclusion;"),
                Tuple.Create<bool, string>(false, "if abc @ 123 then conclusion;"),

                Tuple.Create<bool, string>(false, "if aa( == 123 then conclusion;"),
                Tuple.Create<bool, string>(false, "if bb) == 123 then conclusion;"),

                Tuple.Create<bool, string>(false, "if true then conclusion1 conclusion2;"),

                // Considered invalid for now
                Tuple.Create<bool, string>(false, "if function(a, b, c) then conclusion;"),

                Tuple.Create<bool, string>(false, "if logical_function() then conclusion;"),
                Tuple.Create<bool, string>(false, "if logical_function() and (true) then conclusion;"),

                Tuple.Create<bool, string>(false, "if numeric_function() == 123 then conclusion;"),
                Tuple.Create<bool, string>(false, "if numeric_function() >= 0.12 then conclusion;"),

            };


            foreach (var test in tests)
            {
                var desiredResult = test.Item1;
                var testString = test.Item2;

                AntlrInputStream input = new AntlrInputStream(testString);
                RuleSetGrammarLexer lexer = new RuleSetGrammarLexer(input);
                ITokenStream tokens = new CommonTokenStream(lexer);

                RuleSetGrammarParser parser = new RuleSetGrammarParser(tokens);

                parser.RemoveErrorListeners();
                parser.AddErrorListener(new ThrowExceptionErrorListener());

                if (desiredResult)
                {
                    ParserRuleContext ruleContext = parser.rule_set();
                    string t = ruleContext.GetText();
                    Assert.IsNull(ruleContext.exception);
                }
                else
                {
                    try
                    {
                        ParserRuleContext ruleContext = parser.rule_set();
                        // fail("Failed on \"" + testString + "\"");
                        Assert.IsNotNull(ruleContext);
                    }
                    catch (ArgumentException e)
                    {
                        Console.Write(e.ToString());
                        // deliberately do nothing
                    }
                }

            }



        }

        [TestMethod]
        public void TestCompiler()
        {
            var inputString = "if -(A + 2) > 0.5 then be_careful;";

            SimpleRuleEngine.Compiler.Compiler compiler = new SimpleRuleEngine.Compiler.Compiler();
            RuleSet gotRuleSet = compiler.compile(inputString);

            Assert.IsNull(gotRuleSet);

        }

        [TestMethod]
        public void TestCalculator()
        {
            var testString = @"a=1+2*3; print a;";

            CalculatorLexer lexer = new CalculatorLexer(new AntlrInputStream(testString));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CalculatorParser p = new CalculatorParser(tokens);
            p.BuildParseTree = true;
            p.AddParseListener(new CalculatorListener());
            ParserRuleContext t = p.program();

        }
    }
}
