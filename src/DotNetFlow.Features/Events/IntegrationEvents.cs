using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Infrastructure.Commanding;
using DotNetFlow.Core.Services;
using DotNetFlow.Features.Infrastructure;
using StructureMap;
using StructureMap.Pipeline;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Events
{
    [Binding]
    public class IntegrationEvents
    {
        [BeforeTestRun]
        public static void ClearEventStore()
        {
            new EventStoreCleaner().Execute();
        } 

        /// <summary>
        /// Configure the CQRS environment once before running any integration tests
        /// </summary>
        [BeforeTestRun]
        public static void ConfigureCqrsEnvironment()
        {
            Bootstrapper.Configure();            
        }

        [BeforeScenarioBlock]
        public static void PopulateScenarioContextWithCqrsEnvironment()
        {
            ScenarioContext.Current.Set(ObjectFactory.GetInstance<ICommandService>());
            ScenarioContext.Current.Set(ObjectFactory.GetInstance<IUniqueIdentifierGenerator>());
            ScenarioContext.Current.Set(ObjectFactory.GetInstance<IHashPasswords>());
        }

        [AfterScenario]
        public static void DisposeThreadLocalStorage()
        {
            //new ThreadLocalStorageLifecycle().EjectAll();
            new HybridLifecycle().FindCache().DisposeAndClear();
        }
    }
}