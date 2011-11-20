using System.Configuration;
using CommonDomain;
using CommonDomain.Core;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using DotNetFlow.Core.Infrastructure.Aggregates;
using DotNetFlow.Core.Infrastructure.Commanding;
using DotNetFlow.Core.Infrastructure.Eventing;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Queries;
using DotNetFlow.Core.ReadModel.Repositories;
using DotNetFlow.Core.Services;
using EventStore;
using EventStore.Dispatcher;
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
            ConfigureCqrsInfrastructure();            
            ConfigureUnitOfWorkPerHttpRequest();
            ConfigureCommandValidators();
            ConfigureReadModel();
            ConfigureServices();            
        }

        private void ConfigureCqrsInfrastructure()
        {
            For<IStoreEvents>().Use(ConfigureEventStore);
            For<IUniqueIdentifierGenerator>().Use(InitializeIdGenerator);
            For<ICommandService>().Use(InitializeCommandService);
            For<IConstructAggregates>().Use<SimpleAggregateCreationStrategy>();
            For<IDetectConflicts>().Use<ConflictDetector>();
            For<IRepository>().Use<EventStoreRepository>();            
            For<IUniqueIdentifierGenerator>().Use(InitializeIdGenerator);
        }

        private static IStoreEvents ConfigureEventStore()
        {
            return Wireup.Init()
                .UsingSqlPersistence("EventStore")
                .InitializeStorageEngine()
                .UsingServiceStackJsonSerialization()
                .UsingSynchronousDispatchScheduler().DispatchTo(new EventDispatcher())
                .Build();
        }

        /// <summary>
        /// Share the same unit-of-work instance within a single HTTP request
        /// </summary>
        private void ConfigureUnitOfWorkPerHttpRequest()
        {
            For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use(InitializeUnitOfWork);
        }

        private static IUnitOfWork InitializeUnitOfWork()
        {
            return new UnitOfWork(ConfigurationManager.ConnectionStrings["ReadModel"].ConnectionString);
        }

        private static ICommandService InitializeCommandService()
        {
            var service = new CommandService();

            service.RegisterExecutor(new SubmitNewItemExecutor());
            service.RegisterExecutor(new RegisterUserAccountExecutor());
            service.RegisterExecutor(new PublishItemExecutor());

            return service;
        }

        //private static IEventBus InitializeEventBus()
        //{
        //    var bus = new InProcessEventBus();
        //    bus.RegisterAllHandlersInAssembly(typeof(DomainRegistry).Assembly, ObjectFactory.GetInstance);
        //    return bus;
        //}

        //private static IEventStore InitializeEventStore()
        //{
        //    return new MsSqlServerEventStore(ConfigurationManager.ConnectionStrings["EventStore"].ConnectionString);
        //}

        /// <summary>
        /// The comb algorithm is designed to make the use of GUIDs as Primary Keys, Foreign Keys, and Indexes nearly as efficient as ints.
        /// </summary>
        private static IUniqueIdentifierGenerator InitializeIdGenerator()
        {
            return new GuidCombGenerator();
        }

        private void ConfigureCommandValidators()
        {
            For<IValidator<SubmitNewItemCommand>>().Use<SubmitNewItemValidator>();
            For<IValidator<RegisterUserAccountCommand>>().Use(c => new RegisterUserAccountValidator(c.GetInstance<IFindExistingUsername>().Exists, c.GetInstance<IFindExistingEmailAddress>().Exists));
            For<IValidator<LoginUserCommand>>().Use<LoginUserValidator>();
        }

        private void ConfigureServices()
        {
            For<IHashPasswords>().Use(c => new BCryptPasswordHashing());
            For<IAuthenticationService>().Use<AuthenticationService>();            
        }

        private void ConfigureReadModel()
        {
            For<IReadModelRepository<Submission>>().Use<SubmissionReadModelRepository>();
            For<IUserReadModelRepository>().Use<UserReadModelRepository>();
            For<IFindExistingUsername>().Use<FindExistingUsernameQuery>();
            For<IFindExistingEmailAddress>().Use<FindExistingEmailAddressQuery>();
            For<IQueryModel<PublishedItem>>().Use<LatestPublishedItemsQuery>();
        }
    }
}