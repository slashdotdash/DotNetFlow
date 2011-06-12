using System.Configuration;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Repositories;
using DotNetFlow.Core.Services;
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
            ConfigureNcqrsInfrastructure();
            ConfigureUnitOfWorkPerHttpRequest();
            ConfigureCommandValidators();
            ConfigureReadModelRepositories();
            ConfigureServices();
        }

        private void ConfigureNcqrsInfrastructure()
        {
            For<IEventStore>().Use(InitializeEventStore);
            For<IEventBus>().Use(InitializeEventBus);
            For<ICommandService>().Use(InitializeCommandService);
            For<IUniqueIdentifierGenerator>().Use(InitializeIdGenerator);
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

        private void ConfigureCommandValidators()
        {
            For<IValidator<SubmitNewItemCommand>>().Singleton().Use<SubmitNewItemValidator>();
            For<IValidator<RegisterUserAccountCommand>>().Singleton().Use<RegisterUserAccountValidator>();
            For<IValidator<LoginUserCommand>>().Singleton().Use<LoginUserValidator>();
        }

        private void ConfigureServices()
        {
            For<IHashPasswords>().Use(c => new BCryptPasswordHashing());
            For<IAuthenticationService>().Use<AuthenticationService>();            
        }

        private void ConfigureReadModelRepositories()
        {
            For<IRepository<Submission>>().Use<SubmissionRepository>();
            For<IUserRepository>().Use<UserRepository>();
            For<IRegisteredEmailRepository>().Use<RegisteredEmailRepository>();
        }
    }
}