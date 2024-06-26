
using Scheduler;

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
            string line = "";
            try
            {
                using (StreamReader reader = new StreamReader(this.document.path))
                {
                    while (reader.Read() > 0)
                    {
                        string obj = "";

                        while ((line = reader.ReadLine()) != $"<\\{key}>")
                        {
                            if (!(line.Contains(key)))
                            {
                                obj += line + '\r';
                            }
                        }

                        result.Add(obj);
                    }
                }
            }
            catch
            {

            }
            
        }
    }
}
