namespace Scheduler
{
    internal interface IDataOperation
    {
        internal void Operate(string data) { }
        internal void Operate(string target,string replace) { }
        internal void Operate(ref List<string> result,string id) { }

    }
}
