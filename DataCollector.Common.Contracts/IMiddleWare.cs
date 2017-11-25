namespace DataCollector.Common.Contracts
{
    public interface IMiddleWare<T>
    {
        string Name { get; set; }
        void AddGroup(string groupName);
        void Add(T item);
        void Serialize();
        void Deserialize();
    }
}
