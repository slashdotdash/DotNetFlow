using System;
using CommonDomain.Persistence;
using DotNetFlow.Core.DomainModel;
using DotNetFlow.Core.Infrastructure.Commanding;

namespace DotNetFlow.Core.Commands.Executors
{
    public class SubmitNewItemExecutor : CommandExecutorBase<SubmitNewItemCommand>
    {
        protected override void ExecuteInContext(IRepository context, SubmitNewItemCommand command)
        {
            var item = new Item(command.ItemId, command.UserId, command.Username, command.FullName, command.Title, command.Content);
            context.Save(item, Guid.NewGuid());
        }
    }
}