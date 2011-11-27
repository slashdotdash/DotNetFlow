using System;
using DotNetFlow.Core.Events;

namespace DotNetFlow.Specifications.Builders
{
    internal sealed class NewItemSubmittedBuilder
    {
        private Guid _id = Guid.NewGuid();
        private DateTime _submittedAt = DateTime.Now;
        private Guid? _userId;
        private string _username;
        private string _fullName = "Anonymous";
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

        public NewItemSubmittedBuilder SubmittedByAnonymousUser(string fullName)
        {
            _fullName = fullName;
            _userId = null;
            _username = null;
            return this;
        }

        public NewItemSubmittedBuilder SubmittedByRegisteredUser(Guid userId, string username, string fullName)
        {
            _userId = userId;
            _username = username;
            _fullName = fullName;
            return this;
        }

        public NewItemSubmittedBuilder SubmittedAt(DateTime time)
        {
            _submittedAt = time;
            return this;
        }

        public NewItemSubmittedEvent Build()
        {
            return new NewItemSubmittedEvent
            {
                ItemId = _id,
                SubmittedAt = _submittedAt,
                UserId = _userId,
                Username = _username,
                FullName = _fullName,
                Title = _title,
                RawContent = _rawContent,
                HtmlContent = _htmlContent
            };
        }
    }
}