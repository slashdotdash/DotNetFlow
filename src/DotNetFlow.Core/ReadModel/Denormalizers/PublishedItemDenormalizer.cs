﻿using Dapper;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.Infrastructure.Eventing;

namespace DotNetFlow.Core.ReadModel.Denormalizers
{
    public sealed class PublishedItemDenormalizer : IEventHandler<ItemPublishedEvent>
    {
        private readonly IUnitOfWork _context;

        public PublishedItemDenormalizer(IUnitOfWork unitOfWork)
        {
            _context = unitOfWork;
        }

        public void Handle(ItemPublishedEvent evnt)
        {
            _context.Connection.Execute(
                "insert into Items (ItemId, PublishedAt, SubmittedByUserId, SubmittedByUsername, SubmittedByFullName, Title, HtmlContent) values (@ItemId, @PublishedAt, @SubmittedByUserId, @SubmittedByUsername, @SubmittedByFullName, @Title, @HtmlContent)",
                new { evnt.ItemId, evnt.PublishedAt, evnt.SubmittedByUserId, evnt.SubmittedByUsername, evnt.SubmittedByFullName, evnt.Title, evnt.HtmlContent },
                _context.Transaction);
        }
    }
}