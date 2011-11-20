using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonDomain;
using CommonDomain.Persistence;

namespace DotNetFlow.Core.Infrastructure.Aggregates
{
    class SimpleAggregateCreationStrategy : IConstructAggregates
    {
        public IAggregate Build(Type type, Guid id, IMemento snapshot)
        {
            throw new NotImplementedException();
        }

        //public IAggregate Build(Type type, Guid id, IMemento snapshot)
        //{
        //    if (type == typeof(IMyInterface))
        //        return new MyAggregate();
        //    else
        //        return Activator.CreateInstance(type) as IAggregate;
        //}
    }
}
