namespace SimpleRuleEngine.Models
{
    public class AndExpression : LogicalExpression
    {
        public LogicalExpression left { get;}
        public LogicalExpression right { get;}

        public AndExpression(string type) : base(type)
        {
        }

        public AndExpression(LogicalExpression left, LogicalExpression right) : base("and")
        {
            this.left = left;
            this.right = right;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || this.GetType() != obj.GetType()) return false;

            AndExpression other = (AndExpression)obj;

            if (left != null ? !left.Equals(other.left) : other.left != null) return false;
            if (right != null ? !right.Equals(other.right) : other.right != null) return false;

            return true;
        }

        public override int GetHashCode()
        {
            int result = left != null ? left.GetHashCode() : 0;
            result = 31 * result + (right != null ? right.GetHashCode() : 0);

            return result;
        }
    }
}
