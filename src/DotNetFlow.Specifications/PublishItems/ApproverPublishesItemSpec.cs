using System;
using System.Collections.Generic;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.DomainModel;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Specifications.Builders;
using DotNetFlow.Specifications.Infrastructure;
using NUnit.Framework;

namespace DotNetFlow.Specifications.PublishItems
{
    //public sealed class ApproverPublishesItemSpec : OneEventTestFixture<PublishItemCommand, ItemPublishedEvent>
    //{
    //    public ApproverPublishesItemSpec()
    //    {
    //        new EventStoreCleaner().Execute();
    //        Bootstrapper.Configure();            
    //    }

    //    protected override IEnumerable<object> GivenEvents()
    //    {
    //        yield return new NewItemSubmittedBuilder().Id(EventSourceId).Build();
    //    }

    //    protected override PublishItemCommand WhenExecuting()
    //    {
    //        return new PublishItemCommand
    //        {
    //            ApprovedBy = Guid.NewGuid(),
    //            ItemId = EventSourceId
    //        };
    //    }
        
    //    [Then]
    //    public void Should_Publish_ItemPublishedEvent_Event()
    //    {
    //        Assert.IsInstanceOf(typeof(ItemPublishedEvent), TheEvent);
    //    }

    //    [And]
    //    public void Should_Set_Event_Properties()
    //    {
    //        Assert.AreEqual(ExecutedCommand.ItemId, TheEvent.EventSourceId);
    //        Assert.AreEqual(ExecutedCommand.ApprovedBy, TheEvent.ApprovedBy);
    //    }

    //    [And]
    //    public void Should_Mark_Item_As_Approved()
    //    {
    //        Assert.AreEqual(ApprovalStatus.Approved, TheEvent.Status);
    //    }
    //}
}