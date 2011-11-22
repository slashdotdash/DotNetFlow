using System;
using System.Reflection;
using CommonDomain;
using CommonDomain.Persistence;

namespace DotNetFlow.Core.Infrastructure.Aggregates
{
    public sealed class SimpleAggregateCreationStrategy : IConstructAggregates
    {
        public IAggregate Build(Type type, Guid id, IMemento snapshot)
        {
            var ctor = type.GetConstructor(BindingFlags.NonPublic|BindingFlags.Instance, null, new [] { typeof (Guid) }, null);
            return (IAggregate)ctor.Invoke(new object[] { id });
            //return (IAggregate)Activator.CreateInstance(type);
        }
    }
}