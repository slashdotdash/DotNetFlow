using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetFlow.Core.DomainModel;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Domain;

namespace DotNetFlow.Core.Commands.Executors
{
    public class SubmitNewItemExecutor : CommandExecutorBase<SubmitNewItemCommand>
    {
        protected override void ExecuteInContext(IUnitOfWorkContext context, SubmitNewItemCommand command)
        {
            var item = new Item(Guid.NewGuid(), command.UsersName, command.Title, command.Content);
            context.Accept();
        }
    }
}
