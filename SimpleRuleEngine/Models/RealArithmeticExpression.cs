namespace SimpleRuleEngine.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class RealArithmeticExpression : IArithmeticExpression
    {
        public string op { get; }
        public IArithmeticExpression left { get; }
        public IArithmeticExpression right { get; }

        public RealArithmeticExpression(string op, IArithmeticExpression left, IArithmeticExpression right)
        {
            this.op = op;
            this.left = left;
            this.right = right;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || this.GetType() != obj.GetType()) return false;

            RealArithmeticExpression other = (RealArithmeticExpression)obj;

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
