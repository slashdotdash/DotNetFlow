using System;
using DotNetFlow.Core.DomainModel;
using Ncqrs.Eventing.Sourcing;

namespace DotNetFlow.Core.Events
{
    public sealed class ItemPublishedEvent : SourcedEvent
    {
        public Guid ItemId { get; set; }
        public DateTime PublishedAt;

        // Submission details
        public string SubmissionUsersName { get; set; }
        public string Title { get; set; }
        public string RawContent { get; set; }
        public string HtmlContent { get; set; }

        // Approval metadata
        public Guid ApprovedBy { get; set; }
        public string ApproversName { get; set; }
        public ApprovalStatus Status { get; set; }
    }
}