using System;
using CommonDomain.Persistence;
using DotNetFlow.Core.DomainModel;
using DotNetFlow.Core.Infrastructure.Commanding;

namespace DotNetFlow.Core.Commands.Executors
{
    public class RegisterUserAccountExecutor : CommandExecutorBase<RegisterUserAccountCommand>
    {
        protected override void ExecuteInContext(IRepository context, RegisterUserAccountCommand command)
        {
            var user = new UserAccount(command.UserId, command.FullName, command.Username, command.Email, command.Password, command.Website, command.Twitter);
            context.Save(user, Guid.NewGuid());
        }
    }
}