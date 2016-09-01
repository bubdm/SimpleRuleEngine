namespace SimpleRuleEngine.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class RuleSet : IRuleSetModel
    {
        public List<Rule> rules { get; set; }

        public RuleSet()
        {
            this.rules = new List<Rule>();
        }

        public RuleSet(List<Rule> rules)
        {
            this.rules = rules;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || this.GetType() != obj.GetType()) return false;

            RuleSet other = (RuleSet)obj;

            return this.rules.Equals(other.rules);

        }


        public override int GetHashCode()
        {

            return rules != null ? rules.GetHashCode() : 0;
        }
    }
}
