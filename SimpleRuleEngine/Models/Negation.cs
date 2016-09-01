namespace SimpleRuleEngine.Models
{
    public class Negation : IArithmeticExpression
    {
        public IArithmeticExpression expression { get; set; }

        public Negation(IArithmeticExpression expression)
        {
            this.expression = expression;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || this.GetType() != obj.GetType()) return false;

            Negation other = (Negation)obj;
            if (expression != null ? !expression.Equals(other.expression) : other.expression != null) return false;

            return true;
        }


        public override int GetHashCode()
        {

            return expression != null ? expression.GetHashCode() : 0;
        }
    }
}
