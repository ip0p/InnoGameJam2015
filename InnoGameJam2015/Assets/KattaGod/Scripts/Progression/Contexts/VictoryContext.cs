namespace KattaGod.Progression.Contexts
{
    using Slash.Unity.DataBind.Core.Data;

    public class VictoryContext : Context
    {
        private readonly Property<Satisfaction> satisfactionProperty =
            new Property<Satisfaction>();

        public Satisfaction Satisfaction
        {
            get
            {
                return this.satisfactionProperty.Value;
            }
            set
            {
                this.satisfactionProperty.Value = value;
            }
        }    
    }
}