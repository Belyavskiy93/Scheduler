
using Scheduler;
using System.Text.RegularExpressions;

namespace Scheduler
{
    internal class TextDocumentReader:IDataOperation
    {
        private AbstractDocument document;
        private string key { get; set; }
        internal TextDocumentReader(AbstractDocument document,string key) 
        {
            this.document = document;
            this.key = key;
        }

        
        // Reads data from a document and groups object data into separates rows
        public void Operate(ref List<string> result,string id)
        {
            try
            {
                List<Match> matches = new List<Match>();
                string file = "";

                using (StreamReader reader = new StreamReader(this.document.path))
                {
                    file = reader.ReadToEnd();
                }

                if(id!= string.Empty)
                {
                    matches = Regex.Matches(file, @$"<{key}>([\S\s]+?)<\\{key}>").Where(m => m.Value.Contains(id)).ToList();
                }
                else
                {
                    matches = Regex.Matches(file, @$"<{key}>([\S\s]+?)<\\{key}>").ToList();
                }

                foreach (Match m in matches)
                {
                    result.Add(m.Groups[1].Value);
                }
            }
            catch
            {

            }
            
        }
    }
}
