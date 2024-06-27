
using Scheduler;

namespace Scheduler
{
    internal class TextDocumentWriter:IDataOperation
    {
        private AbstractDocument document;
        internal TextDocumentWriter(AbstractDocument document)
        {
            this.document = document;
        }

        // Writes data in a document
        public void  Operate(string data)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(this.document.path, true))
                {
                    writer.Write(data);
                }
            }
            catch
            {

            }
            
        }
    }
}
