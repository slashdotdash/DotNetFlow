namespace DotNetFlow.Core.Infrastructure.Commanding
{
    public interface ICommandService
    {
        void Execute(ICommand command);
    }
}