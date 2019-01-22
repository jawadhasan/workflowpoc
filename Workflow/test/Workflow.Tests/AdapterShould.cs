using System;
using Workflow.Data.Adapter;
using Workflow.Tests.Mocks;
using Xunit;

namespace Workflow.Tests
{
    public class AdapterShould
    {
        [Fact]
        public void Create_ApplicationWorkFlowFromOnBoardingWorkflow()
        {
            //Arrange
            var workflowFromDb = InMemoryWorkflowDataService.GetOnboardingWorkflow();

            //Act
            var applicationWorkflow = new ApplicationWorkflow(workflowFromDb);


            //Assert
            Assert.Equal(workflowFromDb.WorkflowType, applicationWorkflow.Name);
            Assert.Equal(workflowFromDb.SourceEmailAddress, applicationWorkflow.SourceEmailAddress);
            Assert.Equal(workflowFromDb.RequestId, applicationWorkflow.RequestId);
            Assert.Equal(workflowFromDb.CreatedOn, applicationWorkflow.CreatedOn);
            Assert.Equal(workflowFromDb.ExpiresIn, applicationWorkflow.ExpiresIn);
            Assert.Equal(workflowFromDb.Status, applicationWorkflow.Status);

            Assert.Equal(workflowFromDb.FirstName, applicationWorkflow.WorkflowData.FirstName);
            Assert.Equal(workflowFromDb.LastName, applicationWorkflow.WorkflowData.LastName);
            Assert.Equal(workflowFromDb.Email, applicationWorkflow.WorkflowData.Email);
            Assert.Equal(workflowFromDb.Work, applicationWorkflow.WorkflowData.Work);
            Assert.Equal(workflowFromDb.Street, applicationWorkflow.WorkflowData.Street);
            Assert.Equal(workflowFromDb.City, applicationWorkflow.WorkflowData.City);
            Assert.Equal(workflowFromDb.Zip, applicationWorkflow.WorkflowData.Zip);
            Assert.Equal(workflowFromDb.State, applicationWorkflow.WorkflowData.State);

            Assert.Equal(workflowFromDb.Steps.Count, applicationWorkflow.Steps.Count);

            //TODO: steps validation

        }


        //more unit tests for other cases and workflows


    }
}
