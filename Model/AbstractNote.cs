using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Scheduler
{
    public abstract class AbstractNote:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        internal IConvertType? converter { get; set; }
        protected string id { get; set; }
        protected string? name { get; set; }
        protected string? description { get; set; }
        protected  string time { get; set; }

        public string Id
        {
            get => this.id;
            set => this.id = value;
        }

        public string Name
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

        public string Time
        {
            get => this.time;
            set => this.time = value;
        }


        internal AbstractNote() { }
        internal AbstractNote(string name,string description = "")
        {
            this.id = Guid.NewGuid().ToString();
            this.name = name;
            this.description = description;
            time = DateTime.Now.ToString();
        }

        internal AbstractNote(string id,string name,string description="",string time="")
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.time = DateTime.Now.ToString();
        }


        static internal void CopyAbstractNote(AbstractNote note_1,AbstractNote note_2)
        {
            PropertyInfo[] properties = note_1.GetType().GetProperties();

            foreach (PropertyInfo p in properties)
            {
                p.SetValue(note_2, p.GetValue(note_1));
            }
        }

        internal abstract string Convert();
        internal abstract AbstractNote Convert(string data);

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
