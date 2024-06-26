
namespace Scheduler
{
    public abstract class AbstractDocument
    {
        internal string path = "";

        internal AbstractDocument() { }
        internal AbstractDocument(string path)
        {
            this.path = path;
        }
    }
}
