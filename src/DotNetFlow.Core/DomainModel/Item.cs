using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetFlow.Core.Events;
using Ncqrs.Domain;

namespace DotNetFlow.Core.DomainModel
{
    public sealed class Item : AggregateRootMappedByConvention
    {
        private string _usersName;
        private string _title, _content;
        private ApprovalStatus _status;

        private Item()
        {
            // Required default ctor for Ncqrs
        }

        public Item(Guid itemId, string usersName, string title, string content)
        {
            ApplyEvent(new NewItemSubmittedEvent
            {
                ItemId = itemId,
                SubmissionUsersName = usersName,
                Title = title,
                Content = content,
                Status = ApprovalStatus.Pending
            });
        }

        private void OnNewItemSubmitted(NewItemSubmittedEvent @event)
        {
            _usersName = @event.SubmissionUsersName;
            _title = @event.Title;
            _content = @event.Content;
            _status = @event.Status;
        }
    }
}
