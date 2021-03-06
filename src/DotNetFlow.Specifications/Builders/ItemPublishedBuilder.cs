﻿using System;
using DotNetFlow.Core.Events;

namespace DotNetFlow.Specifications.Builders
{
    internal sealed class ItemPublishedBuilder
    {
        private Guid _itemId = Guid.NewGuid();
        private Guid _submittedByUserId;
        private string _submittedByUsername;
        private string _submittedByFullName = "Anonymous";
        private string _title = "Announcing release of ASP.NET MVC 3, IIS Express, SQL CE 4, Web Farm Framework, Orchard, WebMatrix";
        private string _rawContent = "Scott Gu has [announced the release of ASP.NET MVC 3](http://weblogs.asp.net/scottgu/archive/2011/01/13/announcing-release-of-asp-net-mvc-3-iis-express-sql-ce-4-web-farm-framework-orchard-webmatrix.aspx\">announced)";
        private string _htmlContent = "Scott Gu has <a href=\"http://weblogs.asp.net/scottgu/archive/2011/01/13/announcing-release-of-asp-net-mvc-3-iis-express-sql-ce-4-web-farm-framework-orchard-webmatrix.aspx\">announced the release of ASP.NET MVC 3</a>";
        private string _approversName = "Approver";
        private Guid _approvedById;
        private DateTime _publishedAt = DateTime.Now;

        public ItemPublishedBuilder ForItem(Guid id)
        {
            _itemId = id;
            return this;
        }

        public ItemPublishedBuilder Title(string title)
        {
            _title = title;
            return this;
        }

        public ItemPublishedBuilder Content(string raw, string html)
        {
            _rawContent = raw;
            _htmlContent = html;
            return this;
        }

        public ItemPublishedBuilder SubmittedByAnonymousUser(string userName)
        {
            _submittedByFullName = userName;
            return this;
        }

        public ItemPublishedBuilder SubmittedByRegisteredUser(Guid userId, string username, string fullName)
        {
            _submittedByUserId = userId;
            _submittedByUsername = username;
            _submittedByFullName = fullName;
            return this;
        }

        public ItemPublishedBuilder ApprovedBy(string name)
        {
            _approversName = name;
            _approvedById = Guid.NewGuid();
            return this;
        }

        public ItemPublishedEvent Build()
        {
            return new ItemPublishedEvent
            {
                ItemId = _itemId,
                SubmittedByUserId = _submittedByUserId,
                SubmittedByUsername = _submittedByUsername,
                SubmittedByFullName = _submittedByFullName,
                Title = _title,
                HtmlContent = _htmlContent,
                RawContent = _rawContent,
                ApprovedByUser = _approversName,
                ApprovedByUserId = _approvedById,
                PublishedAt = _publishedAt
            };
        }
    }
}