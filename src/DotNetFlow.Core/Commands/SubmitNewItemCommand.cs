using System;
using DotNetFlow.Core.Infrastructure.Commanding;

namespace DotNetFlow.Core.Commands
{
    public sealed class SubmitNewItemCommand : ICommand
    {
        public Guid ItemId { get; set; }

        // Submitting user's details
        public Guid? UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }

        // Submission details
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}