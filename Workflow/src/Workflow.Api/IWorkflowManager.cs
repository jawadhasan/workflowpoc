using System.Threading.Tasks;
using Workflow.Core.Workflows;
using Workflow.Core.WorkflowSteps;

namespace Workflow.Api
{
    public interface IWorkflowManager
    {
        BaseWorkflow BaseWorkflow { get; }
        Task CreateNew(string requestId, string workflowName, string sourceEmailAddress, int expiresInDays);
        string GetFirstInvalidStepName { get; }
        Task LoadUserWorkflowByRequestId(string requestId);
        Task<BaseWorkflow> GetWorkflowByRequestIdAsync(string requestId);
        void SetStepData<T>(Steps step, T data); //where T : IStepData;
    }
}