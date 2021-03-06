﻿using System.Configuration;
using EventStore;
using EventStore.Dispatcher;
using StructureMap.Configuration.DSL;
using FluentValidation;
using CommonDomain;
using CommonDomain.Core;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using DotNetFlow.Core.Infrastructure.Aggregates;
using DotNetFlow.Core.Infrastructure.Commanding;
using DotNetFlow.Core.Infrastructure.Eventing;
using DotNetFlow.Core.ReadModel.Denormalizers;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Queries;
using DotNetFlow.Core.ReadModel.Repositories;
using DotNetFlow.Core.Services;
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
            ConfigureEventHandlers();
            ConfigureReadModel();
            ConfigureServices();
        }

        private void ConfigureCqrsInfrastructure()
        {
            For<IStoreEvents>().Use(ConfigureEventStore);
            For<IRepository>().Use<EventStoreRepository>();
            For<IUniqueIdentifierGenerator>().Use(InitializeIdGenerator);
            For<ICommandService>().Use(InitializeCommandService);
            For<IConstructAggregates>().Use<SimpleAggregateCreationStrategy>();
            For<IDetectConflicts>().Use<ConflictDetector>();
        }

        private static IStoreEvents ConfigureEventStore()
        {
            return Wireup.Init()
                .UsingSqlPersistence("DotNetFlow")
                .InitializeStorageEngine()
                .UsingJsonSerialization()
                .UsingSynchronousDispatchScheduler().DispatchTo(InitializeEventDispatcher())
                .Build();
        }

        private static IDispatchCommits InitializeEventDispatcher()
        {
            var dispatcher = new EventDispatcher();

            var registration = new RegisterEventHandlersInAssembly(dispatcher);
            registration.IncludeAssembly(typeof(IEventHandler<>).Assembly);
            registration.RegisterHandlers();

            return dispatcher;
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
            return new UnitOfWork(ConfigurationManager.ConnectionStrings["DotNetFlow"].ConnectionString);
        }

        private static ICommandService InitializeCommandService()
        {
            var service = new CommandService();

            service.RegisterExecutor(new SubmitNewItemExecutor());
            service.RegisterExecutor(new RegisterUserAccountExecutor());
            service.RegisterExecutor(new PublishItemExecutor());

            return service;
        }

        public void ConfigureEventHandlers()
        {
            For<UserAccountDenormalizer>().Use<UserAccountDenormalizer>();
            For<SubmittedItemDenormalizer>().Use<SubmittedItemDenormalizer>();
            For<PublishedItemDenormalizer>().Use<PublishedItemDenormalizer>();
        }

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
            For<IGenerateUrlSlug>().Use<Slugifier>();
            For<IPublishedItemService>().Use<PublishedItemService>();
        }

        private void ConfigureReadModel()
        {
            For<IReadModelRepository<Submission>>().Use<SubmissionReadModelRepository>();
            For<IUserReadModelRepository>().Use<UserReadModelRepository>();
            For<IFindExistingUsername>().Use<FindExistingUsernameQuery>();
            For<IFindExistingEmailAddress>().Use<FindExistingEmailAddressQuery>();
            For<IFindExistingUrlSlug>().Use<FindExistingUrlSlugQuery>();
        }
    }
}