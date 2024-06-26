namespace Scheduler
{
    public class Subnote:AbstractNote
    {
        internal Subnote() { }
        internal Subnote(string id, string name, string description = "") : base(name,description)
        {
            this.id = id;
        }

        internal override Subnote Convert(string data)
        {
            Subnote subnote = new Subnote();
            this.converter = new ConvertToNote(subnote);
            return (Subnote)this.converter.Convert(data);
        }

        internal override string Convert()
        {
            this.converter = new ConvertToString();
            return this.converter.Convert(this);
        }

    }
}
