namespace SimpleRuleEngine.Models
{
    public class Conclusion : IRuleSetModel
    {
        public string Name { get; set; }

        public Conclusion(string name)
        {
            this.Name = name;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if(obj == null || this.GetType() != obj.GetType()) return false;

            Conclusion other = (Conclusion)obj;

            if (Name != null ? !Name.Equals(other.Name) : other.Name != null) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }
    }
}
