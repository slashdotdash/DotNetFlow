using System;
using System.Collections.Generic;
using System.Reflection;
using DotNetFlow.Core.Infrastructure;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain;
using Ncqrs.Eventing;
using NUnit.Framework;
using Ncqrs.Eventing.Sourcing;
using StructureMap;

namespace DotNetFlow.Specifications.Infrastructure
{
    public abstract class AggregateRootSpecification<TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
        protected abstract IEnumerable<ISourcedEvent> Given();
        protected abstract ICommand When();

        protected TAggregateRoot Subject;
        protected Exception RaisedException;
        protected IEnumerable<IEvent> Events;

        [SetUp]
        public void Setup()
        {
            ConfigureCommandExecution();
            CreateSubjectAndInitialiseFromGivenEvents();

            try
            {
                ExecuteWhenCommand();
                Events = Subject.GetUncommittedEvents();
            }
            catch (Exception ex)
            {
                RaisedException = ex;
            }
        }

        private static void ConfigureCommandExecution()
        {
            Bootstrapper.Configure();
        }

        private void CreateSubjectAndInitialiseFromGivenEvents()
        {
            // Create subject via parameterless private ctor
            Subject = (TAggregateRoot)typeof(TAggregateRoot).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null).Invoke(new object[] { });
            Subject.InitializeFromHistory(Given());
        }

        private void ExecuteWhenCommand()
        {
            ObjectFactory.GetInstance<ICommandService>().Execute(When());            
        }
    }
}