using System;
using CommonDomain.Persistence;
using StructureMap;

namespace DotNetFlow.Core.Infrastructure.Commanding
{
    /// <summary>
    /// Represents a command executor.
    /// </summary>
    /// <typeparam name="TCommand">The type of the commands to execute.</typeparam>
    public abstract class CommandExecutorBase<TCommand> : ICommandExecutor<TCommand> where TCommand : ICommand
    {       
        private readonly IRepository _context;

        protected CommandExecutorBase()
            : this(ObjectFactory.GetInstance<IRepository>())
        {
        }

        protected CommandExecutorBase(IRepository context)
        {
            if (context == null) throw new ArgumentNullException("context");
            _context = context;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="command">The command to execute. This should not be null.</param>
        /// <exception cref="ArgumentNullException">Occurs when <i>command</i> is null.</exception>
        public void Execute(TCommand command)
        {            
            ExecuteInContext(_context, command);
        }

        /// <summary>
        /// Executes the command withing an unit of work context.
        /// </summary>       
        /// <param name="context">The event store repository context.</param>
        /// <param name="command">The command to execute.</param>
        protected abstract void ExecuteInContext(IRepository context, TCommand command);
    }
}
