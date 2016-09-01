namespace SimpleRuleEngine.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Antlr4.Runtime.Misc;
    using Models;
    using System.Globalization;

    public class TreeBuilder : RuleSetGrammarBaseListener
    {

        public RuleSet ruleSet { get; set; }

        private Rule rule = null;

        private Stack<LogicalExpression> logicalExpressions = new Stack<LogicalExpression>();
        private Stack<IComparisonOperand> comparisonOperands = new Stack<IComparisonOperand>();
        private Stack<IArithmeticExpression> arithmeticExpressions = new Stack<IArithmeticExpression>();

        public override void EnterRule_set([NotNull] RuleSetGrammarParser.Rule_setContext context)
        {
            rule = null;
            logicalExpressions.Clear();
            comparisonOperands.Clear();
            arithmeticExpressions.Clear();

            this.ruleSet = new RuleSet();
        }

        public override void EnterSingle_rule([NotNull] RuleSetGrammarParser.Single_ruleContext context)
        {
            this.rule = new Rule();
        }

        public override void ExitConclusion([NotNull] RuleSetGrammarParser.ConclusionContext context)
        {
            this.rule.conclusion = new Conclusion(context.GetText());
        }


        public override void ExitSingle_rule([NotNull] RuleSetGrammarParser.Single_ruleContext context)
        {
            this.rule.condition = this.logicalExpressions.Pop();
            this.ruleSet.rules.Add(this.rule);
            this.rule = null;
        }

        public override void ExitNumericVariable([NotNull] RuleSetGrammarParser.NumericVariableContext context)
        {
            this.arithmeticExpressions.Push(new NumericVariable(context.GetText()));
        }

        public override void ExitNumericConst([NotNull] RuleSetGrammarParser.NumericConstContext context)
        {
            //TODO: implement formating
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            decimal value;

            try
            {
                value = Decimal.Parse(context.GetText());
            }
            catch (Exception)
            {

                throw;
            }

            this.arithmeticExpressions.Push(new NumericConstant(value));

        }

        public override void EnterNumericConst([NotNull] RuleSetGrammarParser.NumericConstContext context)
        {
            base.EnterNumericConst(context);
        }

        public override void ExitArithmeticExpressionMult([NotNull] RuleSetGrammarParser.ArithmeticExpressionMultContext context)
        {
            exitRealArithmeticExpression("*");
        }

        public override void ExitArithmeticExpressionDiv([NotNull] RuleSetGrammarParser.ArithmeticExpressionDivContext context)
        {
            exitRealArithmeticExpression("/");

        }

        public override void ExitArithmeticExpressionPlus([NotNull] RuleSetGrammarParser.ArithmeticExpressionPlusContext context)
        {
            exitRealArithmeticExpression("+");

        }

        public override void ExitArithmeticExpressionMinus([NotNull] RuleSetGrammarParser.ArithmeticExpressionMinusContext context)
        {
            exitRealArithmeticExpression("-");
        }

        protected void exitRealArithmeticExpression(string op)
        {
            // popping order matters
            IArithmeticExpression right = this.arithmeticExpressions.Pop();
            IArithmeticExpression left = this.arithmeticExpressions.Pop();
            RealArithmeticExpression expr = new RealArithmeticExpression(op, left, right);
            this.arithmeticExpressions.Push(expr);
        }

        public override void ExitArithmeticExpressionNegation([NotNull] RuleSetGrammarParser.ArithmeticExpressionNegationContext context)
        {
            Negation negation = new Negation(this.arithmeticExpressions.Pop());
            this.arithmeticExpressions.Push(negation);
        }

        public override void ExitComparison_operand([NotNull] RuleSetGrammarParser.Comparison_operandContext context)
        {
            IArithmeticExpression expr = this.arithmeticExpressions.Pop();
            this.comparisonOperands.Push(expr);
        }

        public override void ExitComparisonExpressionWithOperator([NotNull] RuleSetGrammarParser.ComparisonExpressionWithOperatorContext context)
        {
            // popping order matters
            IComparisonOperand right = this.comparisonOperands.Pop();
            IComparisonOperand left = this.comparisonOperands.Pop();
            string op = context.GetChild(1).GetText();
            ComparisonExpression expr = new ComparisonExpression(op, left, right);
            this.logicalExpressions.Push(expr);
        }

        public override void ExitLogicalConst([NotNull] RuleSetGrammarParser.LogicalConstContext context)
        {
            switch (context.GetText().ToUpper())
            {
                case "TRUE":
                    this.logicalExpressions.Push(LogicalConstant.GetTrue());
                    break;
                case "FALSE":
                    this.logicalExpressions.Push(LogicalConstant.GetFalse());
                    break;
                default:
                    throw new Exception("Unknown logical constant: " + context.GetText());
            }
        }

        public override void ExitLogicalVariable([NotNull] RuleSetGrammarParser.LogicalVariableContext context)
        {
            LogicalVariable variable = new LogicalVariable(context.GetText());
            this.logicalExpressions.Push(variable);
        }

        public override void ExitLogicalExpressionOr([NotNull] RuleSetGrammarParser.LogicalExpressionOrContext context)
        {
            // popping order matters
            LogicalExpression right = logicalExpressions.Pop();
            LogicalExpression left = logicalExpressions.Pop();
            OrExpression expr = new OrExpression(left, right);
            this.logicalExpressions.Push(expr);
        }

        public override void ExitLogicalExpressionAnd([NotNull] RuleSetGrammarParser.LogicalExpressionAndContext context)
        {
            // popping order matters
            LogicalExpression right = logicalExpressions.Pop();
            LogicalExpression left = logicalExpressions.Pop();
            AndExpression expr = new AndExpression(left, right);
            this.logicalExpressions.Push(expr);
        }
    }
}
