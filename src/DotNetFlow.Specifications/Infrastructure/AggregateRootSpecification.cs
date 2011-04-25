using System;
using System.Collections.Generic;
using Ncqrs.Commanding;
using Ncqrs.Domain;
using Ncqrs.Eventing;
using NUnit.Framework;
using Ncqrs.Eventing.Sourcing;

namespace DotNetFlow.Specifications.Infrastructure
{
    public abstract class AggregateRootSpecification<TAggregateRoot> where TAggregateRoot : AggregateRoot, new()
    {
        protected abstract IEnumerable<ISourcedEvent> Given();
        protected abstract ICommand When();

        protected TAggregateRoot Subject;
        protected Exception RaisedException;
        protected IEnumerable<IEvent> Events;

        [SetUp]
        public void Setup()
        {
            Subject = new TAggregateRoot();
            Subject.InitializeFromHistory(Given());

            try
            {
                When();
                Events = Subject.GetUncommittedEvents();
            }
            catch (Exception ex)
            {
                RaisedException = ex;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class SpecificationAttribute : TestFixtureAttribute { }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class ThenAttribute : TestAttribute { }
}