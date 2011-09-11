using System;
using System.Collections.Generic;
using System.Reflection;
using DotNetFlow.Core.Infrastructure;
using Ncqrs.Commanding;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain;
using Ncqrs.Eventing;
using Ncqrs.Spec;
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
        protected IList<IEvent> Events;

        [SetUp]
        public void Setup()
        {
            ConfigureCommandExecution();
            CreateSubjectAndInitialiseFromGivenEvents();
            Events = new List<IEvent>();

            try
            {
                Subject.EventApplied += OnEventApplied;

                ExecuteWhenCommand();
            }
            catch (Exception ex)
            {
                RaisedException = ex;
            }
            finally
            {
                Subject.EventApplied -= OnEventApplied;                
            }
        }

        private void OnEventApplied(object sender, EventAppliedEventArgs e)
        {
            Events.Add(e.Event);
        }

        private static void ConfigureCommandExecution()
        {
            Bootstrapper.Configure();
        }

        /// <summary>
        /// Create subject via parameterless private ctor and initialize from the Given events
        /// </summary>
        private void CreateSubjectAndInitialiseFromGivenEvents()
        {
            Subject = (TAggregateRoot)typeof(TAggregateRoot).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { }, null).Invoke(new object[] { });
            Subject.InitializeFromHistory(PrepareEvents());
        }

        private CommittedEventStream PrepareEvents()
        {
            return new Prepare.PrepareTheseEvents(Given()).ForSource(Subject.EventSourceId);
        }

        private void ExecuteWhenCommand()
        {
            ObjectFactory.GetInstance<ICommandService>().Execute(When());            
        }
    }
}