namespace SimpleRuleEngine.Compiler
{
    using Antlr4.Runtime;
    using System;


    public class ThrowExceptionErrorListener : BaseErrorListener, IAntlrErrorListener<string>
    {

        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new ArgumentException("Invalid Expression: {0}", msg, e);
            //base.SyntaxError(recognizer, offendingSymbol, line, charPositionInLine, msg, e);
        }
        public void SyntaxError(IRecognizer recognizer, string offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new ArgumentException("Invalid Expression: {0}", msg, e);
        }
    }
}
