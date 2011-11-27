using System;
using DotNetFlow.Core.DomainModel;
using DotNetFlow.Core.Infrastructure.Eventing;

namespace DotNetFlow.Core.Events
{
    public sealed class ItemPublishedEvent : IDomainEvent
    {
        public Guid ItemId { get; set; }
        public DateTime PublishedAt;

        // Submitting user's details
        public Guid? SubmittedByUserId { get; set; }
        public string SubmittedByUsername { get; set; }
        public string SubmittedByFullName { get; set; }  // Only this "submitted by" value will be  populated for submissions by Anonymous users

        // Submission details
        public string Title { get; set; }
        public string RawContent { get; set; }
        public string HtmlContent { get; set; }

        // Approval metadata
        public Guid ApprovedByUserId { get; set; }
        public string ApprovedByUser { get; set; }
        public ApprovalStatus Status { get; set; }
    }
}