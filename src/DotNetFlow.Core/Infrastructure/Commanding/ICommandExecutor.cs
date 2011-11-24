namespace DotNetFlow.Core.Infrastructure.Commanding
{
    /// <summary>
    /// Executes a command. This means that the handles 
    /// executes the correct action based on the command.
    /// </summary>
    public interface ICommandExecutor<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="command">The command to execute. This should not be null.</param>
        void Execute(TCommand command);
    }
}
