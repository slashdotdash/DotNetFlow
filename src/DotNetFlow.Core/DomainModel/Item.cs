using System;
using DotNetFlow.Core.Events;
using MarkdownSharp;
using Ncqrs.Domain;

namespace DotNetFlow.Core.DomainModel
{
    public sealed class Item : AggregateRootMappedByConvention
    {
        private Guid _id;
        private string _submittedByUserName;
        private DateTime _submittedAt;
        private DateTime _publishedAt;
        private string _title, _rawContent, _htmlContent;
        private ApprovalStatus _status;
        private Guid _approvedBy;

        private static readonly Markdown Markdown = new Markdown(new MarkdownOptions
        {
            AutoHyperlink = true,
            AutoNewlines = true,
            EncodeProblemUrlCharacters = true,
            StrictBoldItalic = true,
            LinkEmails = true
        });

        private Item()
        {
            // Private ctor for NCQRS
        }

        public Item(Guid id, string submittedByUser, string title, string content) : base(id)
        {
            var htmlContent = Markdown.Transform(content);
            
            ApplyEvent(new NewItemSubmittedEvent
            {
                ItemId = id,
                SubmittedAt = DateTime.Now,
                SubmissionUsersName = submittedByUser,
                Title = title,
                RawContent = content,
                HtmlContent = htmlContent,
                Status = ApprovalStatus.Pending
            });
        }

        public void Approve(Guid approvedBy)
        {
            if (_status == ApprovalStatus.Approved)
                throw new InvalidOperationException("Item has already been approved");

            ApplyEvent(new ItemPublishedEvent
            {
                ItemId = _id,
                PublishedAt = DateTime.Now,
                ApprovedBy = approvedBy,
                Status = ApprovalStatus.Approved,
                Title = _title,
                RawContent = _rawContent,
                HtmlContent = _htmlContent,
                SubmittedByUser = _submittedByUserName,                
            });
        }

        private void OnNewItemSubmitted(NewItemSubmittedEvent @event)
        {
            _id = @event.ItemId;
            _submittedAt = @event.SubmittedAt;
            _submittedByUserName = @event.SubmissionUsersName;
            _title = @event.Title;
            _rawContent = @event.RawContent;
            _htmlContent = @event.HtmlContent;
            _status = @event.Status;
        }

        private void OnItemPublished(ItemPublishedEvent @event)
        {
            _status = @event.Status;
            _publishedAt = @event.PublishedAt;
            _approvedBy = @event.ApprovedBy;
        }
    }
}