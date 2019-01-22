using System;

namespace Workflow.Core.Events
{
    public class WorkflowStartedEvent : IDomainEvent
    {
        public Guid Id { get; private set; }
        public DateTime DateTimeEventOccured { get; }

        public WorkflowStartedEvent()
        {
            Id = Guid.NewGuid();
            DateTimeEventOccured = DateTime.UtcNow;
        }
    }
}