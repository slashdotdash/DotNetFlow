using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetFlow.Core.Events;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace DotNetFlow.Core.ReadModel.Denormalizers
{
    public sealed class SubmittedItemDenormalizer : IEventHandler<NewItemSubmittedEvent>
    {
        public void Handle(NewItemSubmittedEvent evnt)
        {
            throw new NotImplementedException();
        }
    }
}