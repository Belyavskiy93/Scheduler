using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Scheduler
{
    internal class DisplaySubnotes : AbstractDisplay
    {
        private ObservableCollection<AbstractNote> Current_list = new ObservableCollection<AbstractNote>();
        private AbstractNote route_note;
        public DisplaySubnotes() { }
        public DisplaySubnotes(IDataOperation writer,IDataOperation reader,IDataOperation editor, AbstractNote note) 
            : base(writer, reader,editor)
        {
            GetDataFromDocument();
            route_note = note;
        }

        public ObservableCollection<AbstractNote> current_list
        {
            get
            {
                foreach(AbstractNote n in this.Notes)
                {
                    if(n.Id == route_note.Id && !this.Current_list.Contains(n))
                    {
                        this.Current_list.Add(n);
                    }
                }
                return this.Current_list;
            }
        }

        internal new void Add(AbstractNote note)
        {
            this.current_list.Add(note);
            base.Add(note);
        }

        internal new void Edit(AbstractNote old_note,AbstractNote update_note)
        {
            base.Edit(old_note,update_note);
        }

        internal new void Delete(AbstractNote note)
        {
            this.current_list.Remove(note);
            base.Delete(note);
        }

        // Received all subnotes from document,converts to Subnotes and adds to list
        internal override void GetDataFromDocument()
        {
            List<string> data = new List<string>();
            this.reader.Operate(ref data);

            foreach (string s in data)
            {
                this.selected_note = new Subnote();
                this.Notes.Add(this.selected_note.Convert(s));
            }
        }
    }
}
