using System.Reflection;

namespace Scheduler
{
    internal class ConvertToString : IConvertType
    {

        //Converts data of Note to string
        public string Convert(AbstractNote note)
        {
            PropertyInfo[] properties = note.GetType().GetProperties();
            string result = $"<{note.GetType().Name}>\r";
            
            foreach (PropertyInfo p in properties)
            {
                result += $"{p.Name}>{p.GetValue(note)}<\r";
            }

            result += $"<\\{note.GetType().Name}>\r";
            return result;
        }
    }
}
