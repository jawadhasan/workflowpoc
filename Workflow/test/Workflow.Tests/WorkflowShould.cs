using System;
using Workflow.Core;
using Workflow.Core.Workflows;
using Workflow.Data.Adapter;
using Workflow.Tests.Mocks;
using Xunit;

namespace Workflow.Tests
{
    public class WorkflowShould
    {
        [Fact]
        public void NotValid_When_New_Created()
        {
            var basicWorkflow = BasicWorkflow.Create("jawadhasan80@gmail.com", Guid.NewGuid().ToString("N"), 10);

            Assert.Equal(SupportedWorkflows.Basic.ToString(), basicWorkflow.Name);
            Assert.False(basicWorkflow.IsValid());
        }


        [Fact]
        public void Indicate_Workflow_Is_Expired_Correctly()
        {
            //Arrange
            var workflowFromDb = InMemoryWorkflowDataService.GetOnboardingWorkflow(10, new DateTime(2018, 12, 20));

            //Act
            var applicationWorkflow = new ApplicationWorkflow(workflowFromDb);

            //Assert
            Assert.True(applicationWorkflow.IsExpired());
        }


        [Theory]
        [InlineData(WorkflowStatus.Created)]
        [InlineData(WorkflowStatus.Started)]
        [InlineData(WorkflowStatus.Completed)]
        public void Indicate_If_Workflow_Can_Continue_Correctly(WorkflowStatus status)
        {
            //Arrange
            var workflowFromDb = InMemoryWorkflowDataService.GetOnboardingWorkflow(10, new DateTime(2018, 12, 20));
            workflowFromDb.UpdateStatus(status);

            //Act
            var applicationWorkflow = new ApplicationWorkflow(workflowFromDb);

            //Assert
            Assert.True(applicationWorkflow.CanContinue());
        }

        [Theory]
        [InlineData(WorkflowStatus.Cancelled)]
        [InlineData(WorkflowStatus.Expired)]
        [InlineData(WorkflowStatus.Failed)]
        [InlineData(WorkflowStatus.Posted)]
        [InlineData(WorkflowStatus.Success)]
        public void Indicate_If_Workflow_Can_Not_Continue_Correctly(WorkflowStatus status)
        {
            //Arrange
            var workflowFromDb = InMemoryWorkflowDataService.GetOnboardingWorkflow(10, new DateTime(2018, 12, 20));
            workflowFromDb.UpdateStatus(status);

            //Act
            var applicationWorkflow = new ApplicationWorkflow(workflowFromDb);

            //Assert
            Assert.False(applicationWorkflow.CanContinue());
        }
    }
}
