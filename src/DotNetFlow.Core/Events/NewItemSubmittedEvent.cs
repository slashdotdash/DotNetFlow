using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetFlow.Core.DomainModel;
using Ncqrs.Eventing.Sourcing;

namespace DotNetFlow.Core.Events
{
    public sealed class NewItemSubmittedEvent : SourcedEvent
    {
        public Guid ItemId { get; set; }
        public string SubmissionUsersName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ApprovalStatus Status { get; set; }
    }
}