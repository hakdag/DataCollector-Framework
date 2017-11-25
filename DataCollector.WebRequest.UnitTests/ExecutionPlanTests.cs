using DataCollector.Planning;
using MannDBCollector.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace DataCollector.WebRequest.UnitTests
{
    [TestClass]
    public class ExecutionPlanTests
    {
        [TestMethod]
        public void PlanExecutor_EmptyPlan_ExecutionSuccess()
        {
            //Arrange
            Executor<List<VehicleTypeCollectorSet>> executor = new Executor<List<VehicleTypeCollectorSet>>();
            Mock<ExecutionPlan<List<VehicleTypeCollectorSet>>> mockExecutionPlan = new Mock<ExecutionPlan<List<VehicleTypeCollectorSet>>>();

            //Act
            List<VehicleTypeCollectorSet> resultSet = new List<VehicleTypeCollectorSet>();
            mockExecutionPlan.Setup(ep => ep.ExecutePlan()).Returns(resultSet);
            ExecutionResult result = executor.Execute(mockExecutionPlan.Object);

            //Assert
            Assert.IsTrue(result.Sucess);
        }
    }
}
