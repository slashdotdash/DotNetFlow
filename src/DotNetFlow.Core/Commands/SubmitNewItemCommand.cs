using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ncqrs.Commanding;

namespace DotNetFlow.Core.Commands
{
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