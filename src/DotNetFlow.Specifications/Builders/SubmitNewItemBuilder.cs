using System;
using DotNetFlow.Core.Commands;

namespace DotNetFlow.Specifications.Builders
{
    internal sealed class SubmitNewItemBuilder
    {
        private Guid _id = Guid.NewGuid();
        private Guid? _userId;
        private string _username;
        private string _fullName = "Anonymous";
        private string _title = "Announcing release of ASP.NET MVC 3, IIS Express, SQL CE 4, Web Farm Framework, Orchard, WebMatrix";
        private string _content = "Scott Gu has <a href=\"http://weblogs.asp.net/scottgu/archive/2011/01/13/announcing-release-of-asp-net-mvc-3-iis-express-sql-ce-4-web-farm-framework-orchard-webmatrix.aspx\">announced the release of ASP.NET MVC 3</a>";

        public SubmitNewItemBuilder Id(Guid id)
        {
            _id = id;
            return this;
        }

        public SubmitNewItemBuilder Title(string title)
        {
            _title = title;
            return this;
        }

        public SubmitNewItemBuilder Content(string content)
        {
            _content = content;
            return this;
        }

        public SubmitNewItemBuilder SubmittedByAnonymousUser(string userName)
        {
            _fullName = userName;
            return this;
        }

        public SubmitNewItemBuilder SubmittedByRegisteredUser(Guid userId, string username, string fullName)
        {
            _userId = userId;
            _username = username;
            _fullName = fullName;
            return this;
        }

        public SubmitNewItemCommand Build()
        {
            return new SubmitNewItemCommand
            {
                ItemId = _id,
                UserId = _userId,
                Username = _username,
                FullName = _fullName,
                Title = _title,
                Content = _content
            };
        }
    }
}