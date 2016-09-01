namespace SimpleRuleEngine.Models
{
    public class NumericConstant : NumericEntity
    {
        public decimal value { get; set; }
        public NumericConstant(string type) : base(type)
        {
        }

        public NumericConstant(decimal value) : base("const")
        {
            this.value = value;
        }



        //public override bool Equals(object obj)
        //{
        //    if (this == obj) return true;
        //    if (obj == null || this.GetType() != obj.GetType()) return false;

        //    NumericConstant other = (NumericConstant)obj;
        //    if (value != null ? !value.Equals(other.value) : other.value != null) return false;

        //    return true;
        //}


        //public override int GetHashCode()
        //{

        //    return value != null ? value.GetHashCode() : 0;
        //}
    }
}
