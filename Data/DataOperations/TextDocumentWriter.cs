
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
        // If option = false - rewrites document with updated data
        // If option = true - adds new data to the document
        public void  Operate(string data,bool option)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(this.document.path, option))
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
