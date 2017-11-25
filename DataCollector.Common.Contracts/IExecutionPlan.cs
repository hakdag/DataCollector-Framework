namespace DataCollector.Common.Contracts
{
    public interface IExecutionPlan<T>
    {
        void CreatePlan();
        T ExecutePlan();
    }
}
