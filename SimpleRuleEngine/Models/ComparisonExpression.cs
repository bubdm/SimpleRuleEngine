namespace SimpleRuleEngine.Models
{
    public class ComparisonExpression : LogicalExpression
    {
        public string op { get; }
        public IComparisonOperand left { get; }
        public IComparisonOperand right { get; }

        public ComparisonExpression(string type) : base(type)
        {
        }

        public ComparisonExpression(string op, IComparisonOperand left, IComparisonOperand right) : base("comp")
        {
            this.op = op;
            this.left = left;
            this.right = right;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || this.GetType() != obj.GetType()) return false;

            ComparisonExpression other = (ComparisonExpression)obj;

            if (left != null ? !left.Equals(other.left) : other.left != null) return false;
            if (op != null ? !op.Equals(other.op) : other.op != null) return false;
            if (right != null ? !right.Equals(other.right) : other.right != null) return false;

            return true;

        }


        public override int GetHashCode()
        {

            int result = op != null ? op.GetHashCode() : 0;
            result = 31 * result + (left != null ? left.GetHashCode() : 0);
            result = 31 * result + (right != null ? right.GetHashCode() : 0);
            return result;

        }
    }
}
