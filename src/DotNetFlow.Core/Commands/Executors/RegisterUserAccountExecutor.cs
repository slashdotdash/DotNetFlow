using DotNetFlow.Core.DomainModel;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Domain;

namespace DotNetFlow.Core.Commands.Executors
{
    public sealed class RegisterUserAccountExecutor : CommandExecutorBase<RegisterUserAccountCommand>
    {
        protected override void ExecuteInContext(IUnitOfWorkContext context, RegisterUserAccountCommand command)
        {
            new UserAccount(command.UserId, command.FullName, command.Username, command.Email, command.Password, command.Website, command.Twitter);
            context.Accept();
        }
    }
}