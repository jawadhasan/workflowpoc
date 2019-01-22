using System.Collections.Generic;

namespace Workflow.WebUi.Models
{
    public class WorkflowDefinition
    {
        public long WorkflowId { get; set; }
        public string WorkflowName { get; set; }
        public List<Step> Steps { get; set; }

        public WorkflowDefinition(long workflowId, string workflowName)
        {
            WorkflowId = workflowId;
            WorkflowName = workflowName;
            Steps = new List<Step>();
        }

        public void AddStep(Step step)
        {
            Steps.Add(step);
        }
    }
}
