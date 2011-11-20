using System;
using DotNetFlow.Core.DomainModel;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Infrastructure.Eventing;

namespace DotNetFlow.Core.Events
{
    public sealed class ItemPublishedEvent : IDomainEvent
    {
        public Guid ItemId { get; set; }
        public DateTime PublishedAt;

        // Submission details
        public string SubmittedByUser { get; set; }
        public string Title { get; set; }
        public string RawContent { get; set; }
        public string HtmlContent { get; set; }

        // Approval metadata
        public Guid ApprovedBy { get; set; }
        public string ApprovedByUser { get; set; }
        public ApprovalStatus Status { get; set; }
    }
}