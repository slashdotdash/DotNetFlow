using System;
using DotNetFlow.Core.DomainModel;
using Ncqrs.Eventing.Sourcing;

namespace DotNetFlow.Core.Events
{
    public sealed class ItemPublishedEvent : SourcedEvent
    {
        public Guid ApprovedBy { get; set; }
        public DateTime ApprovedAt { get; set; }
        public ApprovalStatus Status { get; set; }
    }
}