using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetFlow.Core.DomainModel;
using Ncqrs.Eventing;

namespace DotNetFlow.Core.Events
{
    public sealed class NewItemSubmittedEvent : Event
    {
        public ApprovalStatus Status { get; private set; }
    }
}