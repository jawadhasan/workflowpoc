using System;
using System.Linq;
using System.Threading.Tasks;
using Workflow.Core;
using Workflow.Core.Data;
using Workflow.Core.Factories;
using Workflow.Core.Workflows;
using Workflow.Core.WorkflowSteps;

namespace Workflow.Api
{
    /// <summary>
    /// All this functionality is setup in controller (WorkflowController), better to use that one
    /// This is just for reference, but should not be used, hence the whole api project can be removed.
    /// </summary>
    
    public class WorkflowManager : IWorkflowManager
    {
        private readonly IWorkflowDataService _workflowDataService;
        public BaseWorkflow BaseWorkflow { get; private set; } //may be we should not have this here, coz not being updated here.

        public WorkflowManager(IWorkflowDataService workflowDataService)
        {
            _workflowDataService = workflowDataService;
        }

        public string GetFirstInvalidStepName => BaseWorkflow.GetFirstInvalidStep();

        public async Task CreateNew(string requestId, string workflowName, string sourceEmailAddress, int expiresInDays)
        {
            var workFlow = WorkflowFactory.CreateNewWorkflow(workflowName, sourceEmailAddress, requestId, expiresInDays);
           await _workflowDataService.SaveWorkflowAsync(workFlow);
        }
        
        
        //Load
        public async Task LoadUserWorkflowByRequestId(string requestId)
        {
            BaseWorkflow = await _workflowDataService.GetWorkflowByRequestId(requestId);
        }

        public async Task<BaseWorkflow> GetWorkflowByRequestIdAsync(string requestId)
        {
            BaseWorkflow = await _workflowDataService.GetWorkflowByRequestId(requestId);
            return BaseWorkflow;
        }


        //Persistence
        public virtual void SetStepData<T>(Steps step, T data) //where T : IStepData
        {
            switch (step)
            {
                case Steps.Personal:
                    SetPersonal(data as Personal);
                    break;
                case Steps.Work:
                    SetWork(data as Work);
                    break;
                case Steps.Address:
                    SetAddress(data as Address);
                    break;
                case Steps.Result:
                    SetResultAndSaveWorkflow(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(step), step, null);
            }
        }

      
        private void SetPersonal(Personal personal)
        {
            if (personal.IsValid())
            {
                var step = (BaseWorkflowStep<Personal>) BaseWorkflow.Steps.FirstOrDefault(s => s.Step == Steps.Personal.ToString());
                if (step != null)
                {
                    step.SetStepData(personal);
                    //BaseWorkflow.WorkflowData.SetPersonal(personal.FirstName, personal.LastName, personal.Email); //removed, was not working
                   _workflowDataService.SaveWorkflowAsync(BaseWorkflow as Core.Workflows.BaseWorkflow);
                }
            }
        }

        private void SetWork(Work work)
        {
            if (work.IsValid())
            {
                var step = (BaseWorkflowStep<Work>)BaseWorkflow.Steps.FirstOrDefault(s => s.Step == Steps.Work.ToString());
                if (step != null)
                {
                    step.SetStepData(work);
                    BaseWorkflow.WorkflowData.Work = work.WorkType;
                    _workflowDataService.SaveWorkflowAsync(BaseWorkflow as Core.Workflows.BaseWorkflow);
                }
            }
        }

        private void SetAddress(Address address)
        {
            if (address.IsValid())
            {
                var step = (BaseWorkflowStep<Address>)BaseWorkflow.Steps.FirstOrDefault(s => s.Step == Steps.Address.ToString());
                if (step != null)
                {
                    step.SetStepData(address);
                    BaseWorkflow.WorkflowData.Street = address.Street;
                    BaseWorkflow.WorkflowData.City = address.City;
                    BaseWorkflow.WorkflowData.Zip = address.Zip;
                    BaseWorkflow.WorkflowData.State = address.State;
                    _workflowDataService.SaveWorkflowAsync(BaseWorkflow as Core.Workflows.BaseWorkflow);
                }
            }
        }

        private void SetResultAndSaveWorkflow(bool result)
        {
            var step = (BaseWorkflowStep<bool>)BaseWorkflow.Steps.FirstOrDefault(s => s.Step == Steps.Result.ToString());
            step?.SetStepData(result);

            if (BaseWorkflow.IsValid())
            {
                _workflowDataService.SaveWorkflowAsync(BaseWorkflow as Core.Workflows.BaseWorkflow);
            }
        }
    }
}
