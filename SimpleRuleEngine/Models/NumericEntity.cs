namespace SimpleRuleEngine.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class NumericEntity : IArithmeticExpression, IComparisonOperand
    {
        public string type { get; set; }

        public NumericEntity(string type)
        {
            this.type = type;
        }
    }
}
