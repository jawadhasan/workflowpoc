using System;

namespace Workflow.Core.Events
{
    public class WorkflowCompletedEvent : IDomainEvent
    {
        public Guid Id { get; private set; }
        public DateTime DateTimeEventOccured { get; }

        public WorkflowCompletedEvent()
        {
            Id = Guid.NewGuid();
            DateTimeEventOccured = DateTime.UtcNow;
        }
    }
}
