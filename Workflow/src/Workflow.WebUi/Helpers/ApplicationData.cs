using System.Collections.Generic;
using Workflow.Core;
using Workflow.Core.WorkflowSteps;
using Workflow.Data.Entities.Management;

namespace Workflow.WebUi.Helpers
{
    public static class ApplicationData
    {
        public static List<Data.Entities.Management.Workflow> GetSupportedWorkflows()
        {
            return new List<Data.Entities.Management.Workflow>
            {
                new Data.Entities.Management.Workflow(SupportedWorkflows.Basic.ToString()),
                new Data.Entities.Management.Workflow(SupportedWorkflows.Simple.ToString()),
                new Data.Entities.Management.Workflow(SupportedWorkflows.OnBoarding.ToString())
            };
        }

        public static List<Step> GetSupportedSteps()
        {
            return new List<Step>
            {
                new Step(Steps.Personal.ToString(),"Please tell us about yourself."),
                new Step(Steps.Work.ToString(),"What do you do?."),
                new Step(Steps.Address.ToString(),"Where do you live?"),
                new Step(Steps.Result.ToString(),"Thanks for staying tuned!")
            };
        }

        public static List<WorkflowStep> GetSupportedWorkflowStepMappings()
        {
            return new List<WorkflowStep>
            {
                //Basic
                new WorkflowStep{WorkflowId = 1, StepId = 1},
                new WorkflowStep{WorkflowId = 1, StepId = 4},

                //Simple
                new WorkflowStep{WorkflowId = 2, StepId = 1},
                new WorkflowStep{WorkflowId = 2, StepId = 2},
                new WorkflowStep{WorkflowId = 2, StepId = 4},

                //OnBoarding
                new WorkflowStep{WorkflowId = 3, StepId = 1},
                new WorkflowStep{WorkflowId = 3, StepId = 2},
                new WorkflowStep{WorkflowId = 3, StepId = 3},
                new WorkflowStep{WorkflowId = 3, StepId = 4}
            };
        }
    }
}
