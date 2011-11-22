using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Infrastructure.Commanding;
using StructureMap;

namespace DotNetFlow.Features.Infrastructure
{
    internal sealed class CommandExecutor
    {
        private readonly ICommandService _commandService;
        private readonly IUnitOfWork _unitOfWork;

        public CommandExecutor(ICommandService commandService, IUnitOfWork unitOfWork)
        {
            _commandService = commandService;
            _unitOfWork = unitOfWork;
        }

        public CommandExecutor() : this(ObjectFactory.GetInstance<ICommandService>(), ObjectFactory.GetInstance<IUnitOfWork>())
        {
        }

        public void Execute(ICommand command)
        {
            _unitOfWork.Begin();
            _commandService.Execute(command);
            _unitOfWork.Commit();
        }
    }
}