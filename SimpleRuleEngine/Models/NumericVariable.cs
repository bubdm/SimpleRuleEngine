namespace SimpleRuleEngine.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public class NumericVariable : NumericEntity
    {

        public string variableName { get; set; }
        public NumericVariable(string type) : base("var")
        {
            this.variableName = type;
        }


        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || this.GetType() != obj.GetType()) return false;

            NumericVariable other = (NumericVariable)obj;
            if (variableName != null ? !variableName.Equals(other.variableName) : other.variableName != null) return false;

            return true;
        }


        public override int GetHashCode()
        {

            return variableName != null ? variableName.GetHashCode() : 0;
        }


    }
}
