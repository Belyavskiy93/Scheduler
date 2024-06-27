using System.Reflection;

namespace Scheduler
{
    internal class ConvertToNote:IConvertType
    {
        internal AbstractNote note;

        internal ConvertToNote() { }

        internal ConvertToNote(AbstractNote note)
        {
            this.note = note;
        }

        // converts data received from a document into Note
        public AbstractNote Convert(string data)
        {
            PropertyInfo[] properties = this.note.GetType().GetProperties();
            string[] data_note = data.Split('<');

            for (int i = 0; i < data_note.Length; ++i)
            {
                string[] property = data_note[i].Split('>');

                foreach (PropertyInfo p in properties)
                {
                    if (property[0].Contains(p.Name))
                    {
                        p.SetValue(note, property[1]);
                    }
                }
            }

            return this.note;
        }
    }
}
