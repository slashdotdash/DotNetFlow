using DotNetFlow.Core.Commands.Executors;
using Ncqrs.Commanding.ServiceModel;
using StructureMap;

namespace DotNetFlow.Core.Infrastructure
{
    public static class Bootstrapper
    {
        public static void Configure()
        {
            ObjectFactory.Initialize(x =>
                x.For<ICommandService>().Use(InitializeCommandService));
        }

        private static ICommandService InitializeCommandService()
        {
            var service = new CommandService();
            service.RegisterExecutor(new SubmitNewItemExecutor());
            
            return service;
        }
    }
}