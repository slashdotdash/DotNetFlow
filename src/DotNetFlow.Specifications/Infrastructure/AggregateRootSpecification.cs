using System;
using System.Collections.Generic;
using Ncqrs.Domain;
using NUnit.Framework;
using Ncqrs.Eventing.Sourcing;

namespace DotNetFlow.Specifications.Infrastructure
{
    public abstract class AggregateRootSpecification<TAggregateRoot> where TAggregateRoot : AggregateRoot, new()
    {
        protected abstract IEnumerable<ISourcedEvent> Given();
        protected abstract void When();

        protected TAggregateRoot Subject;
        protected Exception RaisedException;
        protected IEnumerable<ISourcedEvent> Events;

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
}