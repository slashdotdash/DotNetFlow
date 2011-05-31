using System;
using Ncqrs.Commanding;

namespace DotNetFlow.Core.Commands
{
    public sealed class PublishItemCommand : CommandBase
    {
        public Guid ItemId { get; set; }
        public Guid ApprovedBy { get; set; }
    }
}