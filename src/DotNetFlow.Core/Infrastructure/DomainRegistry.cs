using DotNetFlow.Core.Commands.Executors;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Storage;
using Ncqrs.Eventing.Storage.SQL;
using StructureMap.Configuration.DSL;

namespace DotNetFlow.Core.Infrastructure
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            For<IEventStore>().Singleton().Use(InitializeEventStore);
            For<IEventBus>().Singleton().Use(InitializeEventBus);
            For<ICommandService>().Singleton().Use(InitializeCommandService);
        }

        private static ICommandService InitializeCommandService()
        {
            var service = new CommandService();
            service.RegisterExecutor(new SubmitNewItemExecutor());

            return service;
        }

        //public static void BootUp()
        //{
        //    NcqrsEnvironment.SetDefault(InitializeCommandService());
        //    NcqrsEnvironment.SetDefault(InitializeEventStore());
        //    NcqrsEnvironment.SetDefault<IEventBus>(InitializeEventBus());
        //}

        private static IEventBus InitializeEventBus()
        {
            var bus = new InProcessEventBus();
            //bus.RegisterHandler(new TweetListItemDenormalizer());

            return bus;
        }

        private static IEventStore InitializeEventStore()
        {
            return new MsSqlServerEventStore("Data Source=.\\SQLEXPRESS;Integrated Security=SSPI;User Instance=True;AttachDbFilename=|DataDirectory|\\EventStore.mdf;");
        }
    }
}