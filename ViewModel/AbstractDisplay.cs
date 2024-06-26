using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Scheduler
{
    abstract class AbstractDisplay:INotifyPropertyChanged
    {
        protected AbstractNote selected_note;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected IDataOperation writer { get; set; }
        protected IDataOperation reader { get; set; }
        protected IDataOperation editor { get; set; }
        public ObservableCollection<AbstractNote> Notes { get; set; } = new ObservableCollection<AbstractNote>();

        public AbstractDisplay() { }
        public AbstractDisplay(IDataOperation writer, IDataOperation reader,IDataOperation editor)
        {
            this.writer = writer;
            this.reader = reader;
            this.editor = editor;
        }

        internal IDataOperation Writer
        {
            get => writer;
        }
        public AbstractNote Selected_note
        {
            get => this.selected_note;
            set
            {
                if (this.selected_note != value)
                {
                    this.selected_note = value;
                    OnPropertyChanged();
                }
            }

        }

        internal void Add(AbstractNote note)
        {
            this.Notes.Add(note);
            string data = note.Convert();
            this.writer.Operate(data, true);
        }
        internal void Delete(AbstractNote note)
        {
            this.Notes.Remove(note);
            string removed_note = note.Convert();
            this.editor.Operate(removed_note, "");
        }
        internal void Edit(AbstractNote old_note,AbstractNote update_note)
        {
            string old = old_note.Convert();
            string update = update_note.Convert();
            editor.Operate(old, update);
        }
        abstract internal void GetDataFromDocument();

        // Converts all data to writes into a document
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
