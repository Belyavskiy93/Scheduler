using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Scheduler
{
    internal class DisplaySubnotes : AbstractDisplay
    {
        private AbstractNote route_note { get; set; }
        public DisplaySubnotes() { }
        public DisplaySubnotes(IDataOperation writer,IDataOperation reader,IDataOperation editor, AbstractNote note) 
            : base(writer, reader,editor)
        {
            route_note = note;
            GetDataFromDocument();
        }

        internal new void Add(AbstractNote note)
        {
            base.Add(note);
        }

        internal new void Edit(AbstractNote old_note,AbstractNote update_note)
        {
            base.Edit(old_note,update_note);
        }

        internal new void Delete(AbstractNote note)
        {
            base.Delete(note);
        }

        // Received all subnotes from document,converts to Subnotes and adds to list
        protected override void GetDataFromDocument()
        {
            List<string> data = new List<string>();
            this.reader.Operate(ref data,this.route_note.Id);

            foreach (string s in data)
            {
                this.selected_note = new Subnote();
                this.Notes.Add(this.selected_note.Convert(s));
            }
        }
    }
}
