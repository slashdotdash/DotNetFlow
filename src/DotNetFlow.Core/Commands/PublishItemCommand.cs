using System;
using DotNetFlow.Core.Infrastructure.Commanding;

namespace DotNetFlow.Core.Commands
{
    public sealed class PublishItemCommand : ICommand
    {
        public Guid ItemId { get; set; }
        public Guid ApprovedBy { get; set; }
    }
}