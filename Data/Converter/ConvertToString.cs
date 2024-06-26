using System.Reflection;

namespace Scheduler
{
    internal class ConvertToString : IConvertType
    {

        //Converts data of Note to string
        public string Convert(AbstractNote note)
        {
            PropertyInfo[] properties = note.GetType().GetProperties();
            string result = $"\n<{note.GetType().Name}>";
            
            foreach (PropertyInfo p in properties)
            {
                result += $"\n{p.Name}>{p.GetValue(note)}<";
            }

            result += $"\n<\\{note.GetType().Name}>";
            return result;
        }
    }
}
