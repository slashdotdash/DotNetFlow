using System.Configuration;
using DotNetFlow.Core.Commands.Executors;
using DotNetFlow.Core.Commands.Validators;
using FluentValidation;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Storage;
using Ncqrs.Eventing.Storage.SQL;
using StructureMap.Configuration.DSL;
using DotNetFlow.Core.Commands;

namespace DotNetFlow.Core.Infrastructure
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            For<IEventStore>().Use(InitializeEventStore);
            For<IEventBus>().Use(InitializeEventBus);
            For<ICommandService>().Use(InitializeCommandService);
            For<IUniqueIdentifierGenerator>().Use(InitializeIdGenerator);
            ConfigureValidators();
        }

        private void ConfigureValidators()
        {
            For<IValidator<SubmitNewItemCommand>>().Singleton().Use<SubmitNewItemValidator>();
        }

        private static ICommandService InitializeCommandService()
        {
            var service = new CommandService();
            service.RegisterExecutor(new SubmitNewItemExecutor());

            return service;
        }

        private static IEventBus InitializeEventBus()
        {
            var bus = new InProcessEventBus();
            //bus.RegisterHandler(new TweetListItemDenormalizer());

            return bus;
        }

        private static IEventStore InitializeEventStore()
        {
            return new MsSqlServerEventStore(ConfigurationManager.ConnectionStrings["EventStore"].ConnectionString);
        }

        /// <summary>
        /// The comb algorithm is designed to make the use of GUIDs as Primary Keys, Foreign Keys, and Indexes nearly as efficient as ints.
        /// </summary>
        private static IUniqueIdentifierGenerator InitializeIdGenerator()
        {
            return new GuidCombGenerator();
        }
    }
}