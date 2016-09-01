namespace SimpleRuleEngine.Models
{
    public class LogicalExpression : IRuleSetModel
    {
        public string Type { get;}

        protected LogicalExpression(string type)
        {
            this.Type = type;
        }

    }
}
