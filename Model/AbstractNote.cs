using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Scheduler
{
    public abstract class AbstractNote:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        internal IConvertType? converter { get; set; }
        protected string? id { get; set; }
        protected string? name { get; set; }
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


        public string Time
        {
            get => this.time;
            set
            {
                if(this.time != value)
                {
                    this.time = value;
                    OnPropertyChanged();
                }
            }
        }


        internal AbstractNote() { }
        internal AbstractNote(string name)
        {
            this.id = Guid.NewGuid().ToString();
            this.name = name;
            time = DateOnly.FromDateTime(DateTime.Now).ToString();
        }


        static internal void CopyAbstractNote(AbstractNote source,AbstractNote receiver)
        {
            PropertyInfo[] properties = source.GetType().GetProperties();

            foreach (PropertyInfo p in properties)
            {
                p.SetValue(receiver, p.GetValue(source));
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
