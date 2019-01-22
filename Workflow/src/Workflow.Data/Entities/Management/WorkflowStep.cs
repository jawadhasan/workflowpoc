namespace Workflow.Data.Entities.Management
{
    public class WorkflowStep
    {
        public long WorkflowId { get; set; }
        public virtual Workflow Workflow { get; set; }

        public long StepId { get; set; }
        public virtual Step Step { get; set; }
    }
}
