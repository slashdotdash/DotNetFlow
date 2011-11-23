using DotNetFlow.Core.Infrastructure.Commanding;

namespace DotNetFlow.Specifications.Infrastructure
{
    public abstract class CommandTestFixture<TCommand> : DomainTestFixture<TCommand> where TCommand : ICommand
    {
        protected ICommandExecutor<TCommand> CommandExecutor { get; private set; }

        protected override void SetupDependencies()
        {
            base.SetupDependencies();

            CommandExecutor = BuildCommandExecutor();
        }

        protected override void Execute(TCommand command)
        {
            CommandExecutor.Execute(command);
        }

        protected abstract ICommandExecutor<TCommand> BuildCommandExecutor();
    }
}
