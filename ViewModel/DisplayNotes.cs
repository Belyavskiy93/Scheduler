
using Scheduler;

namespace Scheduler
{
    internal class DisplayNotes : AbstractDisplay
    {
        
        public DisplayNotes() { }
        public DisplayNotes(IDataOperation writer,IDataOperation reader,IDataOperation editor) : base(writer, reader,editor) 
        { 
            GetDataFromDocument(); 
        }

        internal  new void Add(AbstractNote note)
        {
            base.Add(note);
        }

        internal new void Delete(AbstractNote note)
        {
            base.Delete(note);
        }

        internal new void Edit(AbstractNote old_note,AbstractNote update_note)
        {
            base.Edit(old_note, update_note);
        }

        internal override void GetDataFromDocument()
        {
            List<string> data = new List<string>();
            this.reader.Operate(ref data);

            foreach (string s in data)
            {
                this.selected_note = new Note();
                this.Notes.Add(this.selected_note.Convert(s));
            }
        }
    }
}

