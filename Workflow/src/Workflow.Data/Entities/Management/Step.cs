using System.Collections.Generic;
using Workflow.Core;

namespace Workflow.Data.Entities.Management
{
    public class Step
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Title { get; private set; }
        public List<WorkflowStep> WorkflowSteps { get; set; }

        protected Step()
        {
            WorkflowSteps = new List<WorkflowStep>();
        }
        public Step(string name, string title):this()
        {
            Guard.ForNullOrEmpty(name, nameof(name));
            Guard.ForNullOrEmpty(title, nameof(title));

            Name = name;
            Title = title;
        }
    }
}
