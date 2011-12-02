using System;
using CommonDomain.Persistence;
using DotNetFlow.Core.DomainModel;
using DotNetFlow.Core.Infrastructure.Commanding;

namespace DotNetFlow.Core.Commands.Executors
{
    public class PublishItemExecutor : CommandExecutorBase<PublishItemCommand>
    {       
        protected override void ExecuteInContext(IRepository context, PublishItemCommand command)
        {
            var submission = context.GetById<Item>(command.ItemId);
            submission.Approve(command.ApprovedBy, command.PublishedAt);

            context.Save(submission, Guid.NewGuid());
        }
    }
}