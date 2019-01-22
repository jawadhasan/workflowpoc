using System;

namespace Workflow.Core.Events
{
    public interface IDomainEvent
    {
        DateTime DateTimeEventOccured { get; }
    }
}
