using System.Linq;
using DotNetFlow.Core.Infrastructure.Commanding;
using NUnit.Framework;

namespace DotNetFlow.Specifications.Infrastructure
{
    public abstract class OneEventTestFixture<TCommand, TEvent> : BigBangTestFixture<TCommand> where TCommand : ICommand
    {
        public TEvent TheEvent { get; private set; }

        public override void When()
        {
            base.When();

            TheEvent = PublishedEvents
                .Select(e => e.Body)
                .OfType<TEvent>()
                .FirstOrDefault();
        }

        [Then]
        public void Should_Do_No_More()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(1));
        }

        [Then]
        public void Should_Publish_The_Event()
        {
            Assert.That(TheEvent, Is.Not.Null);
        }

        [Then]
        public void Should_Not_Throw_Exception()
        {
            Assert.That(CaughtException, Is.Null);
        }
    }
}