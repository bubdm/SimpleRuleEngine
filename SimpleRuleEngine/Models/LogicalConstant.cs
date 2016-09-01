namespace SimpleRuleEngine.Models
{
    public class LogicalConstant : LogicalExpression
    {
        public bool value { get; set; }
        public LogicalConstant(string type) : base(type)
        {
        }

        public LogicalConstant(bool value) : base("const")
        {
            this.value = value;
        }

        public static LogicalConstant GetTrue()
        {
            return new LogicalConstant(true);

        }

        public static LogicalConstant GetFalse()
        {
            return new LogicalConstant(false);

        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || this.GetType() != obj.GetType()) return false;

            LogicalConstant other = (LogicalConstant)obj;

            return value == other.value;
        }


        public override int GetHashCode()
        {

            return (value ? 1 : 0);
        }

    }
}
