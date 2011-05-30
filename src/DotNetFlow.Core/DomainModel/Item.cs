using System;
using DotNetFlow.Core.Events;
using MarkdownSharp;
using Ncqrs.Domain;

namespace DotNetFlow.Core.DomainModel
{
    public sealed class Item : AggregateRootMappedByConvention
    {
        private Guid _id;
        private string _usersName;
        private DateTime _submittedAt;
        private string _title, _rawContent, _htmlContent;
        private ApprovalStatus _status;

        private static readonly Markdown Markdown = new Markdown(new MarkdownOptions
        {
            AutoHyperlink = true,
            AutoNewlines = true,
            EncodeProblemUrlCharacters = true,
            StrictBoldItalic = true,
            LinkEmails = true
        });

        public Item(Guid id, string usersName, string title, string content)
        {
            var htmlContent = Markdown.Transform(content);

            ApplyEvent(new NewItemSubmittedEvent
            {
                ItemId = id,
                SubmittedAt = DateTime.Now,
                SubmissionUsersName = usersName,
                Title = title,
                RawContent = content,
                HtmlContent = htmlContent,
                Status = ApprovalStatus.Pending
            });
        }

        private void OnNewItemSubmitted(NewItemSubmittedEvent @event)
        {
            _id = @event.ItemId;
            _submittedAt = @event.SubmittedAt;
            _usersName = @event.SubmissionUsersName;
            _title = @event.Title;
            _rawContent = @event.RawContent;
            _htmlContent = @event.HtmlContent;
            _status = @event.Status;
        }
    }
}