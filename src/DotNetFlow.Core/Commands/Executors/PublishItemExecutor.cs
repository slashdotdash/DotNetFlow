using DotNetFlow.Core.DomainModel;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Domain;

namespace DotNetFlow.Core.Commands.Executors
{
    public class PublishItemExecutor : CommandExecutorBase<PublishItemCommand>
    {
        protected override void ExecuteInContext(IUnitOfWorkContext context, PublishItemCommand command)
        {
            var submission = context.GetById<Item>(command.ItemId);
            submission.Approve(command.ApprovedBy);
            context.Accept();
        }
    }
}