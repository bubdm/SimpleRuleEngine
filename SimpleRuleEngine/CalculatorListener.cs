namespace SimpleRuleEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Antlr4.Runtime.Misc;

    public class CalculatorListener : CalculatorBaseListener
    {
        public Stack<int> stack = new Stack<int>();
        public Dictionary<string, int> sym = new Dictionary<string, int>();

        public override void ExitPrint([NotNull] CalculatorParser.PrintContext context)
        {
            foreach (var argCtx in context.arg())
            {
                if (argCtx.ID() != null)
                {
                    Console.Write(sym[argCtx.ID().GetText()]);

                }
                else
                {
                    Console.Write(argCtx.GetText().Replace("^\"|\"$", ""));
                }
            }
            Console.WriteLine();
            //base.ExitPrint(context);
        }
        public override void ExitAssignment([NotNull] CalculatorParser.AssignmentContext context)
        {
            sym.Add(context.ID().GetText(), stack.Pop());
            //base.ExitAssignment(context);
        }

        public override void ExitInteger([NotNull] CalculatorParser.IntegerContext context)
        {
            stack.Push(int.Parse(context.INT().GetText()));
            //base.ExitInteger(context);
        }

        public override void ExitAddOrSubtract([NotNull] CalculatorParser.AddOrSubtractContext context)
        {
            int op1 = stack.Pop();
            int op2 = stack.Pop();
            if (context.GetChild(1).GetText().Equals("-"))
            {
                stack.Push(op2 - op1);
            }
            else
            {
                stack.Push(op1 + op2);
            }
            //base.ExitAddOrSubtract(context);
        }

        public override void ExitMultOrDiv([NotNull] CalculatorParser.MultOrDivContext context)
        {
            int op1 = stack.Pop();
            int op2 = stack.Pop();
            if (context.GetChild(1).GetText().Equals("/"))
            {
                stack.Push(op2 / op1);
            }
            else
            {
                stack.Push(op2 * op1);
            }
            //base.ExitMultOrDiv(context);
        }
    }
}
