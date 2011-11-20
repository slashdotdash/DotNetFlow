using System;
using DotNetFlow.Core.DomainModel;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Infrastructure.Eventing;

namespace DotNetFlow.Core.Events
{
    public sealed class NewItemSubmittedEvent : IDomainEvent
    {
        public Guid ItemId { get; set; }
        public DateTime SubmittedAt;
        public string SubmissionUsersName { get; set; }
        public string Title { get; set; }
        public string RawContent { get; set; }
        public string HtmlContent { get; set; }
        public ApprovalStatus Status { get; set; }
    }
}