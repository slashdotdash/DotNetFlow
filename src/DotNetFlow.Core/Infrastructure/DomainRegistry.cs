using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Storage;
using Ncqrs.Eventing.Storage.SQL;
using StructureMap;
using StructureMap.Configuration.DSL;
using FluentValidation;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Commands.Executors;
using DotNetFlow.Core.Commands.Validators;

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
            For<IDbConnection>().Use(CreateReadModelDbConnection);

            ConfigureValidators();
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
            bus.RegisterAllHandlersInAssembly(typeof(DomainRegistry).Assembly, ObjectFactory.GetInstance);
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

        private static IDbConnection CreateReadModelDbConnection()
        {
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ReadModel"].ConnectionString);
            connection.Open();
            return connection;
        }

        private void ConfigureValidators()
        {
            For<IValidator<SubmitNewItemCommand>>().Singleton().Use<SubmitNewItemValidator>();
        }

    }
}