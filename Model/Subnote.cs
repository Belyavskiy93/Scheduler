namespace Scheduler
{
    public class Subnote:AbstractNote
    {
        private string id;
        private string description;

        public new string Id
        {
            get => this.id;
            set => this.id = value;
        }

        public new string Name
        {
            get => this.name;
            set
            {
                if(this.name != value)
                {
                    this.name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => this.description;
            set
            {
                if (this.description != value)
                {
                    this.description = value;
                    OnPropertyChanged();
                }
            }
        }

        internal Subnote() { }
        internal Subnote(string id,string name, string description = "") : base(name)
        {
            this.id = id;
            this.description = description;
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
