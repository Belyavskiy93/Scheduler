
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
        public void Operate(ref List<string> result)
        {
            try
            {
                string file = "";

                using (StreamReader reader = new StreamReader(this.document.path))
                {
                    file = reader.ReadToEnd();
                }

                List<Match> matches = Regex.Matches(file, @$"<{key}>([\s\S]+?)<\\{key}>").ToList();

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
