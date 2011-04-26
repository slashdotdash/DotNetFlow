using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetFlow.Core.DomainModel;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace DotNetFlow.Core.Commands
{
    //[MapsToAggregateRootConstructor(typeof(Item))]
    public sealed class SubmitNewItemCommand : CommandBase
    {
        public string UsersName { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }

        public SubmitNewItemCommand(string usersName, string title, string content)
        {
            UsersName = usersName;
            Title = title;
            Content = content;
        }
    }
}