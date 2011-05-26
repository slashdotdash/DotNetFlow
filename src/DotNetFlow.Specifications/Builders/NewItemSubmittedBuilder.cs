using System;
using DotNetFlow.Core.Events;

namespace DotNetFlow.Specifications.Builders
{
    internal sealed class NewItemSubmittedBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _submissionUsersName = "Anonymous";
        private string _title = "Announcing release of ASP.NET MVC 3, IIS Express, SQL CE 4, Web Farm Framework, Orchard, WebMatrix";
        private string _rawContent = "Scott Gu has [announced the release of ASP.NET MVC 3](http://weblogs.asp.net/scottgu/archive/2011/01/13/announcing-release-of-asp-net-mvc-3-iis-express-sql-ce-4-web-farm-framework-orchard-webmatrix.aspx\">announced)";
        private string _htmlContent = "Scott Gu has <a href=\"http://weblogs.asp.net/scottgu/archive/2011/01/13/announcing-release-of-asp-net-mvc-3-iis-express-sql-ce-4-web-farm-framework-orchard-webmatrix.aspx\">announced the release of ASP.NET MVC 3</a>";

        public NewItemSubmittedBuilder Id(Guid id)
        {
            _id = id;
            return this;
        }

        public NewItemSubmittedBuilder Title(string title)
        {
            _title = title;
            return this;
        }

        public NewItemSubmittedBuilder Content(string raw, string html)
        {
            _rawContent = raw;
            _htmlContent = html;
            return this;
        }

        public NewItemSubmittedBuilder SubmittedBy(string userName)
        {
            _submissionUsersName = userName;
            return this;
        }

        public NewItemSubmittedEvent Build()
        {
            return new NewItemSubmittedEvent
            {
                ItemId = _id,
                SubmissionUsersName = _submissionUsersName,
                Title = _title,
                RawContent = _rawContent,
                HtmlContent = _htmlContent
            };
        }
    }
}