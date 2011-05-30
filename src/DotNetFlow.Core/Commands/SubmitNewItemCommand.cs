﻿using System;
using Ncqrs.Commanding;

namespace DotNetFlow.Core.Commands
{
    public sealed class SubmitNewItemCommand : CommandBase
    {
        public Guid ItemId { get; set; }
        public string UsersName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}