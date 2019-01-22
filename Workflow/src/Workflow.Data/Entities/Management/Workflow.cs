using System.Collections.Generic;
using Workflow.Core;

namespace Workflow.Data.Entities.Management
{
    public class Workflow
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<WorkflowStep> WorkflowSteps { get; set; }

        protected Workflow()
        {
            WorkflowSteps = new List<WorkflowStep>();
        }

        public Workflow(string name, bool isActive = true) :this()
        {
            Guard.ForNullOrEmpty(name, nameof(name));

            Name = name;
            IsActive = isActive;
        }
    }
}
