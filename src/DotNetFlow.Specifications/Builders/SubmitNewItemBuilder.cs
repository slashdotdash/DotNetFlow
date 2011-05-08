using DotNetFlow.Core.Commands;

namespace DotNetFlow.Specifications.Builders
{
    internal sealed class SubmitNewItemBuilder
    {
        private string _submittingUsersName = "Anonymous";
        private string _title = "Announcing release of ASP.NET MVC 3, IIS Express, SQL CE 4, Web Farm Framework, Orchard, WebMatrix";
        private string _content = "Scott Gu has <a href=\"http://weblogs.asp.net/scottgu/archive/2011/01/13/announcing-release-of-asp-net-mvc-3-iis-express-sql-ce-4-web-farm-framework-orchard-webmatrix.aspx\">announced the release of ASP.NET MVC 3</a>";

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

        public SubmitNewItemBuilder SubmittedBy(string userName)
        {
            _submittingUsersName = userName;
            return this;
        }

        public SubmitNewItemCommand Build()
        {
            return new SubmitNewItemCommand(_submittingUsersName, _title, _content);
        }
    }
}