using System;
using DotNetFlow.Core.DomainModel;
using DotNetFlow.Core.Infrastructure.Eventing;

namespace DotNetFlow.Core.Events
{
    public sealed class NewItemSubmittedEvent : IDomainEvent
    {
        public Guid ItemId { get; set; }
        public DateTime SubmittedAt;

        // Submitting user's details
        public Guid? UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }  // Only this "submitted by" value will be  populated for submissions by Anonymous users

        // Submission details
        public string Title { get; set; }
        public string RawContent { get; set; }
        public string HtmlContent { get; set; }
        
        public ApprovalStatus Status { get; set; }
    }
}