using System.Collections.ObjectModel;

namespace Scheduler
{
    public class Note: AbstractNote
    {
        internal Note() { }
        internal Note(string name) : base(name) { }
        
        internal override Note Convert(string data)
        {
            Note note = new Note();
            this.converter = new ConvertToNote(note);
            return (Note)this.converter.Convert(data);
        }


        internal override string Convert()
        {
            this.converter = new ConvertToString();
            return this.converter.Convert(this);
        }

    }
}
