using System;

namespace DataCollector.Planning
{
    public class Executor<T>
    {
        public ExecutionResult Execute(ExecutionPlan<T> plan)
        {
            try
            {
                T collectorSet = plan.ExecutePlan();
                if (collectorSet == null)
                    return new ExecutionResult { Sucess = false, Message = "Null result." };
                return new ExecutionResult { Sucess = true, Message = "" };
            }
            catch (Exception exc)
            {
                return new ExecutionResult { Sucess = false, Message = exc.Message };
            }
        }
    }
}
