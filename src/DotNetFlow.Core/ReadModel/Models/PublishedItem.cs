using System;

namespace DotNetFlow.Core.ReadModel.Models
{
    public sealed class PublishedItem
    {
        public Guid ItemId { get; set; }
        public DateTime PublishedAt;

        public string SubmittedByUser { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
        public string UrlSlug { get; set; }
    }
}