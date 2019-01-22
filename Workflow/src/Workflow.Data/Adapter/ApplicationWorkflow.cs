using System;
using System.Collections.Generic;
using System.Linq;
using Workflow.Core;
using Workflow.Core.Data;
using Workflow.Core.Factories;
using Workflow.Core.Workflows;
using BaseWorkflow = Workflow.Core.Workflows.BaseWorkflow;

namespace Workflow.Data.Adapter
{
    public class ApplicationWorkflow : BaseWorkflow
    {
        private readonly Entities.UserWorkflow _dataUserWorkflow;
        public ApplicationWorkflow(Entities.UserWorkflow dataUserWorkflow)
        {
            _dataUserWorkflow = dataUserWorkflow;
        }

        public override long Id => _dataUserWorkflow.Id;
        public override string Name => _dataUserWorkflow.WorkflowType;
        public override string SourceEmailAddress => _dataUserWorkflow.SourceEmailAddress;
        public override string RequestId => _dataUserWorkflow.RequestId;
        public override int ExpiresIn => _dataUserWorkflow.ExpiresIn;
        public override DateTime CreatedOn => _dataUserWorkflow.CreatedOn;
        public override WorkflowStatus Status => _dataUserWorkflow.Status;

        public override FormData WorkflowData => new FormData(
            _dataUserWorkflow.FirstName,
            _dataUserWorkflow.LastName,
            _dataUserWorkflow.Email,
            _dataUserWorkflow.Work,
            _dataUserWorkflow.Street,
            _dataUserWorkflow.City,
            _dataUserWorkflow.State,
            _dataUserWorkflow.Zip,
            _dataUserWorkflow.Steps.All(s=> s.IsCompleted)
            );

        public override List<IWorkflowStep> Steps => BuildStepsFromDb(WorkflowData, _dataUserWorkflow.Steps.ToList());


        private static List<IWorkflowStep> BuildStepsFromDb(FormData formData, List<Entities.UserWorkflowStep> steps)
        {
            var result = new List<IWorkflowStep>();
            foreach (var dbStep in steps)
            {
                result.Add(WorkflowStepFactory.Create(dbStep.Step, dbStep.Step, formData));
            }
            return result;
        }
    }
}
