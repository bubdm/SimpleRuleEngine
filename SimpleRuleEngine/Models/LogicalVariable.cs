namespace SimpleRuleEngine.Models
{
    public class LogicalVariable : LogicalExpression
    {
        public string variableName { get; set; }
        public LogicalVariable(string type) : base("var")
        {
            this.variableName = type;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || this.GetType() != obj.GetType()) return false;

            LogicalVariable other = (LogicalVariable)obj;
            if (variableName != null ? !variableName.Equals(other.variableName) : other.variableName != null) return false;

            return true;
        }


        public override int GetHashCode()
        {

            return variableName != null ? variableName.GetHashCode() : 0;
        }
    }
}
