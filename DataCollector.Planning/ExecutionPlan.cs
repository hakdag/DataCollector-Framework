using DataCollector.Common.Contracts;

namespace DataCollector.Planning
{
    public abstract class ExecutionPlan<T> : IExecutionPlan<T>
    {
        public abstract void CreatePlan();
        public abstract void LoadPlan(string filename);
        public abstract void SavePlan();
        public abstract T ExecutePlan();
    }
}
