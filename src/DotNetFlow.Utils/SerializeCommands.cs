using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Infrastructure.Eventing;
using DotNetFlow.Core.Services;

namespace DotNetFlow.Utils
{
    internal sealed class SerializeCommands
    {
        public void Execute()
        {
            var idGenerator = new GuidCombGenerator();
            var passwordHashing = new BCryptPasswordHashing();

            var register1 = new RegisterUserAccountCommand
            {
                UserId = idGenerator.GenerateNewId(),
                Email = "ben@dotnetflow.com",
                FullName = "Ben Smith",
                Username = "ben",
                Password = passwordHashing.HashPassword("7IsGSiBtssQKmCMC"),
                Website = "http://www.dotnetflow.com/",
                Twitter = "dotnetflow"
            };

            var register2 = new RegisterUserAccountCommand
            {
                UserId = idGenerator.GenerateNewId(),
                Email = "admin@dotnetflow.com",
                FullName = "Administrator",
                Username = "dotnetflow",
                Password = passwordHashing.HashPassword("7IsGSiBtssQKmCMC"),
            };

            var submission1 = new SubmitNewItemCommand
            {
                UserId = Guid.NewGuid(),
                ItemId = idGenerator.GenerateNewId(),
                FullName = "Ben Smith",
                Username = "ben",
                Title = "Welcome to DotNetFlow",
                Content = @"The new website for sharing .NET related news, events and announcements. In fact we welcome sharing of anything that might be of interest to fellow .NET developers.

Have you:

*Created a blog post about a Microsoft technology?
* Released a NuGet library worth using?
* Want to inform developers about some new tech, archetecture pattern or alternative tool - NoSQL, CQRS, SOA, service buses or similar?

Then post up a link here on [dotnetflow](http://www.dotnetflow.com) and share it with the .NET world."
            };

            var publish = new PublishItemCommand
            {
                ApprovedBy = Guid.NewGuid(),
                ItemId = Guid.NewGuid(),
                PublishedAt = DateTime.UtcNow,
            };

            var serialized = publish.ToJson();
        }
    }
}
