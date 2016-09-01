namespace SimpleRuleEngine.Models
{
    public class Rule : IRuleSetModel
    {
        public LogicalExpression condition { get; set; }

        public Conclusion conclusion { get; set; }

        public Rule()
        {

        }
        public Rule(LogicalExpression condition, Conclusion conclusion)
        {
            this.condition = condition;
            this.conclusion = conclusion;
        }

         public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || this.GetType() != obj.GetType()) return false;

            Rule other = (Rule)obj;

            if (conclusion != null ? !conclusion.Equals(other.conclusion) : other.conclusion != null) return false;
            if (condition != null ? !condition.Equals(other.condition) : other.condition != null) return false;

            return true;

        }


        public override int GetHashCode()
        {

            int result = condition != null ? condition.GetHashCode() : 0;
            result = 31 * result + (conclusion != null ? conclusion.GetHashCode() : 0);

            return result;
        }
    }
}
