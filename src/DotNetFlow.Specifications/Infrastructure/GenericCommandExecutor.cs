using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution;

namespace DotNetFlow.Specifications.Infrastructure
{
    internal class GenericCommandExecutor<TCommand> : ICommandExecutor<ICommand> where TCommand : ICommand
    {
        private readonly ICommandExecutor<TCommand> _executor;

        public GenericCommandExecutor(ICommandExecutor<TCommand> executor)
        {
            _executor = executor;
        }

        public void Execute(ICommand command)
        {
            _executor.Execute((TCommand)command);
        }
    }
}