using System;
using DotNetFlow.Core.Infrastructure.Commanding;

namespace DotNetFlow.Core.Commands
{
    public sealed class SubmitNewItemCommand : ICommand
    {
        public Guid ItemId { get; set; }
        public string UsersName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}