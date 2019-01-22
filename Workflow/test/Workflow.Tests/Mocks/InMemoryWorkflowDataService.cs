using System;
using Workflow.Core;
using Workflow.Core.Workflows;
using Workflow.Core.WorkflowSteps;
using Workflow.Data.Entities;

namespace Workflow.Tests.Mocks
{
    public static class InMemoryWorkflowDataService
    {

        /// <summary>
        /// Returns a completed UserWorkflow
        /// </summary>
        /// <returns></returns>
        public static Data.Entities.UserWorkflow GetOnboardingWorkflow(int expiresIn = 30, DateTime? createdDate = null)
        {
            var dbWorkflow = new Data.Entities.UserWorkflow(
                SupportedWorkflows.OnBoarding.ToString(),
                "jawadhasan80@gmail.com",
                Guid.NewGuid().ToString("N"),
                expiresIn, 
                createdDate ?? new DateTime(2018,12,20)
                );

            var personalStep = new UserWorkflowStep(Steps.Personal.ToString());
            personalStep.MarkIsStepComplete(true);
            dbWorkflow.AddWorkflowStep(personalStep);

            var workStep = new UserWorkflowStep(Steps.Work.ToString());
            workStep.MarkIsStepComplete(true);
            dbWorkflow.AddWorkflowStep(workStep);

            var addressStep = new UserWorkflowStep(Steps.Address.ToString());
            addressStep.MarkIsStepComplete(true);
            dbWorkflow.AddWorkflowStep(addressStep);

            var resultStep = new UserWorkflowStep(Steps.Result.ToString());
            resultStep.MarkIsStepComplete(true);
            dbWorkflow.AddWorkflowStep(resultStep);


            dbWorkflow.SetPersonal("Jawad", "Hasan", "j.shani@hds-systems.com");
            dbWorkflow.SetWork("Code");
            dbWorkflow.SetAddress("Willy-Brandt-Plataz", "Braunschweig", "38114", "SomeState");
            dbWorkflow.UpdateStatus(WorkflowStatus.Completed);

            return dbWorkflow;
        }



    }
}
