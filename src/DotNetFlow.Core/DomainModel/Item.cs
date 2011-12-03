using System;
using CommonDomain.Core;
using DotNetFlow.Core.Events;
using MarkdownSharp;

namespace DotNetFlow.Core.DomainModel
{
    public sealed class Item : AggregateBase
    {
        private Guid _id;
        private Guid? _submittedByUserId;
        private string _submittedByUsername;
        private string _submittedByUserNamed;
        private DateTime _submittedAt;
        private DateTime _publishedAt;
        private string _title, _rawContent, _htmlContent;
        private ApprovalStatus _status;
        private Guid _approvedBy;
        private string _urlSlug;

        private static readonly Markdown Markdown = new Markdown(new MarkdownOptions
        {
            AutoHyperlink = true,
            AutoNewLines = true,
            EncodeProblemUrlCharacters = true,
            StrictBoldItalic = true,
            LinkEmails = true
        });

        private Item(Guid id)
        {            
            Id = id;
        }

        public Item(Guid id, Guid? userId, string username, string fullName, string title, string content) : this(id)
        {
            var htmlContent = Markdown.Transform(content);

            RaiseEvent(new NewItemSubmittedEvent
            {
                ItemId = id,
                SubmittedAt = DateTime.Now,
                UserId = userId,
                Username = username,
                FullName = fullName,
                Title = title,
                RawContent = content,
                HtmlContent = htmlContent,
                Status = ApprovalStatus.Pending
            });
        }

        public void Approve(Guid approvedBy, DateTime publishedAt, string urlSlug)
        {
            Guard.Against<InvalidOperationException>(_status == ApprovalStatus.Approved, "Item has already been approved");
            Guard.Against<ArgumentOutOfRangeException>(publishedAt > DateTime.UtcNow, "Publish date cannot be in the future");

            RaiseEvent(new ItemPublishedEvent
            {
                ItemId = _id,
                PublishedAt = publishedAt,
                ApprovedByUserId = approvedBy,
                Status = ApprovalStatus.Approved,
                Title = _title,
                RawContent = _rawContent,
                HtmlContent = _htmlContent,
                UrlSlug = urlSlug,
                SubmittedByUserId = _submittedByUserId,
                SubmittedByUsername = _submittedByUsername,
                SubmittedByFullName = _submittedByUserNamed,                
            });
        }

        private void Apply(NewItemSubmittedEvent @event)
        {
            _id = @event.ItemId;
            _submittedAt = @event.SubmittedAt;
            _submittedByUserId = @event.UserId;
            _submittedByUsername = @event.Username;
            _submittedByUserNamed = @event.FullName;
            _title = @event.Title;
            _rawContent = @event.RawContent;
            _htmlContent = @event.HtmlContent;
            _status = @event.Status;
        }

        private void Apply(ItemPublishedEvent @event)
        {
            _status = @event.Status;
            _publishedAt = @event.PublishedAt;
            _approvedBy = @event.ApprovedByUserId;
        }       
    }
}