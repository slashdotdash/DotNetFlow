using System;

namespace DotNetFlow.Core.ReadModel.Models
{
    public sealed class Submission
    {
        public Guid ItemId { get; set; }
        public DateTime SubmittedAt { get; set; }
        public Guid? UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        //public string Status { get; set; }

        public bool SubmittedByAnonymousUser
        {
            get { return !UserId.HasValue; }
        }
    }
}