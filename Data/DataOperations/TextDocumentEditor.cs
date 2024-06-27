using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scheduler
{
    class TextDocumentEditor:IDataOperation
    {
        AbstractDocument document;

        internal TextDocumentEditor(AbstractDocument document)
        {
            this.document = document;
        }

        // Seeks the target string and replaces it.
        // If It needs to delete object,the replacement variable is equal empty string.
        // If It needs to edit object,the replacement variable has a value.
        public void Operate(string target,string replace)
        {
            StringBuilder data = new StringBuilder();

            using(StreamReader reader = new StreamReader(this.document.path))
            {
                data.Append(reader.ReadToEnd());
            }

            data.Replace(target, replace);

            using (StreamWriter writer = new StreamWriter(this.document.path,false))
            {
                writer.Write(data);
            }
        }
    }
}
