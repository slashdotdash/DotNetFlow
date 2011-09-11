using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Services;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using StructureMap;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Events
{
    [Binding]
    public class IntegrationEvents
    {
        /// <summary>
        /// Configure the NCQRS environment once before running any integration tests
        /// </summary>
        [BeforeTestRun]
        public static void ConfigureNcqrsEnvironment()
        {
            Bootstrapper.Configure();            
        }

        [BeforeScenarioBlock]
        public static void PopulateScenarioContextWithNcqrsEnvironment()
        {
            ScenarioContext.Current.Set(ObjectFactory.GetInstance<ICommandService>());
            ScenarioContext.Current.Set(ObjectFactory.GetInstance<IUniqueIdentifierGenerator>());
            ScenarioContext.Current.Set(ObjectFactory.GetInstance<IHashPasswords>());
        }
    }
}