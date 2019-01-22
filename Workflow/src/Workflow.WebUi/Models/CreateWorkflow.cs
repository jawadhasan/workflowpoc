using System;
using System.ComponentModel.DataAnnotations;

namespace Workflow.WebUi.Models
{
    public class CreateWorkflow
    {
        public string RequestId { get; set; } 

        [Required]
        public string WorkflowName { get; set; }

        [Required]
        public string SourceEmailAddress { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public CreateWorkflow()
        {
            RequestId = Guid.NewGuid().ToString("N");
        }

    }
}
