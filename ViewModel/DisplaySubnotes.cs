using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Scheduler
{
    internal class DisplaySubnotes : AbstractDisplay
    {
        private AbstractNote route_note { get; set; }
        private DisplayNotes display_notes { get; set; }
        public DisplaySubnotes() { }
        public DisplaySubnotes(IDataOperation writer, IDataOperation reader, IDataOperation editor,DisplayNotes display_notes)
            : base(writer, reader, editor)
        {
            this.display_notes = display_notes;
            this.route_note = this.display_notes.Selected_note;
            GetDataFromDocument();
        }

        internal new void Add(AbstractNote note)
        {
            UpdateTimeRouteNote();
            base.Add(note);
        }

        internal new void Edit(AbstractNote old_note,AbstractNote update_note)
        {
            UpdateTimeRouteNote();
            base.Edit(old_note,update_note);
        }

        internal new void Delete(AbstractNote note)
        {

            base.Delete(note);
        }

        private void UpdateTimeRouteNote()
        {
            Note old_route_note = new Note();
            AbstractNote.CopyAbstractNote(this.route_note, old_route_note);
            this.display_notes.Edit(old_route_note, this.route_note);
        }

        // Received all subnotes from document,converts to Subnotes and adds to list
        protected override void GetDataFromDocument()
        {
            List<string> data = new List<string>();
            this.reader.Operate(ref data,this.display_notes.Selected_note.Id);

            foreach (string s in data)
            {
                this.selected_note = new Subnote();
                this.Notes.Add(this.selected_note.Convert(s));
            }
        }
    }
}
