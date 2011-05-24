using DotNetFlow.Core.DomainModel;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Domain;

namespace DotNetFlow.Core.Commands.Executors
{
    public class SubmitNewItemExecutor : CommandExecutorBase<SubmitNewItemCommand>
    {
        protected override void ExecuteInContext(IUnitOfWorkContext context, SubmitNewItemCommand command)
        {
            new Item(command.ItemId, command.UsersName, command.Title, command.Content);
            context.Accept();
        }
    }
}