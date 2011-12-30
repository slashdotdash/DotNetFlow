using System;
using System.IO;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Infrastructure.Commanding;
using DotNetFlow.Core.Infrastructure.Eventing;
using DotNetFlow.Core.Services;

namespace DotNetFlow.Utils
{
    internal sealed class SerializeCommands
    {
        private readonly GuidCombGenerator _idGenerator;
        private readonly BCryptPasswordHashing _passwordHashing;
        private readonly IGenerateUrlSlug _urlSlugGenerator;
        private const string SeedDirectory = @"C:\src\dotnetflow\database\seed";
        private int _commandIndex = 1;
        private Guid _userId;

        public SerializeCommands()
        {
            _idGenerator = new GuidCombGenerator();
            _passwordHashing = new BCryptPasswordHashing();
            _urlSlugGenerator = new Slugifier(new InMemorySlugStore());
        }

        public void Execute()
        {
            if (Directory.Exists(SeedDirectory))
                Directory.Delete(SeedDirectory, true);
            Directory.CreateDirectory(SeedDirectory);

            _userId = _idGenerator.GenerateNewId();

            var register1 = new RegisterUserAccountCommand
            {
                UserId = _userId,
                Email = "ben@dotnetflow.com",
                FullName = "Ben Smith",
                Username = "ben",
                Password = _passwordHashing.HashPassword("7IsGSiBtssQKmCMC"),
                Website = "http://www.dotnetflow.com/",
                Twitter = "dotnetflow"
            };
            WriteCommand(register1);

            var register2 = new RegisterUserAccountCommand
            {
                UserId = _idGenerator.GenerateNewId(),
                Email = "admin@dotnetflow.com",
                FullName = "Administrator",
                Username = "dotnetflow",
                Password = _passwordHashing.HashPassword("7IsGSiBtssQKmCMC"),
            };
            WriteCommand(register2);

            SubmitItem("Welcome to DotNetFlow", "The new website for sharing .NET related news, events and announcements. In fact we welcome sharing of anything that might be of interest to fellow .NET developers.\r\n\r\nHave you:\r\n\r\n* Created a blog post about a Microsoft technology?\r\n* Released a NuGet library worth using?\r\n* Want to inform developers about some new tech, architecture pattern or alternative tool - NoSQL, CQRS, SOA, service buses or similar?\r\n\r\nThen post up a link here on [dotnetflow](http://www.dotnetflow.com) and share it with the .NET world.", new DateTime(2011, 11, 28, 10, 27, 0));
            SubmitItem("Bundling and minification support coming in ASP.NET 4", "Scott Gu has just added the sixth installment of his blog series on the new features coming in ASP.NET 4. The latest post covers the new [bundling and minification support](http://weblogs.asp.net/scottgu/archive/2011/11/27/new-bundling-and-minification-support-asp-net-4-5-series.aspx).\r\n\r\n> ASP.NET is adding a feature that makes it easy to \"bundle\" or \"combine\" multiple CSS and JavaScript files into fewer HTTP requests. This causes the browser to request a lot fewer files and in turn reduces the time it takes to fetch them.", new DateTime(2011, 11, 29, 7, 51, 0));
            SubmitItem("Jonathan Oliver\'s CQRS EventStore v3.0 release", "Jonathan Oliver has [announced the v3.0 release of his CQRS EventStore](http://blog.jonathanoliver.com/2011/11/eventstore-v3-0-release/) persistence library for event sourcing. You can download the release as a [NuGet](http://nuget.org/List/Packages/EventStore) package or grab the source from [GitHub](https://github.com/joliver/EventStore).\r\n\r\nJonathan also recently [gave a talk about his EventStore](http://vimeo.com/31153808) at E-VAN ([slides available](http://www.slideshare.net/jonathanoliver/europe-virtual-altnet-eventstore-v3)).", new DateTime(2011, 11, 30, 9, 48, 0));
            SubmitItem("Interview with Eric Evans, Udi Dahan and Diego Vega discussing CQRS and DDD", "Udi Dahan has [just blogged](http://www.udidahan.com/2011/11/30/recording-of-joint-interview-with-eric-evans/) about a discussion between him and Eric Evans covering the topics of DDD and CQRS. The talk was recorded and a [video](http://vimeo.com/32647895) has been made available to watch courtesy of .dotNetMania (DNM).", new DateTime(2011, 12, 1, 8, 51, 0));
            SubmitItem("Effective Aggregate Design Part II by Vaughn Vernon", "Vaughn Vernon has just published [Part II](http://dddcommunity.org/library/vernon_2011) of his Effective Aggregate Design series.\r\n\r\n* [Part I](http://dddcommunity.org/sites/default/files/pdf_articles/Vernon_2011_1.pdf) considers the modelling of an aggregate.\r\n* [Part II](http://dddcommunity.org/sites/default/files/pdf_articles/Vernon_2011_2.pdf) looks at the model and design issues of how different aggregates relate to each other.", new DateTime(2011, 12, 2, 8, 39, 0));
            SubmitItem("New CSS Editor Improvements in Visual Studio (ASP.NET 4.5 Series)", "The seventh in Scott Gu's [series of blog posts](http://weblogs.asp.net/scottgu/archive/2011/08/31/asp-net-vnext-series.aspx) about the up-and-coming features of ASP.NET 4.5 is about the [New CSS Editor Improvements in Visual Studio](http://weblogs.asp.net/scottgu/archive/2011/12/02/new-css-editor-improvements-in-visual-studio-asp-net-4-5-series.aspx).", new DateTime(2011, 12, 3, 10, 43, 0));
            SubmitItem("The Rise of Developeronomics", "Venkatesh Rao writes about the [rise of developeronomics](http://www.forbes.com/sites/venkateshrao/2011/12/05/the-rise-of-developeronomics/).\r\n\r\n> As every company starts to re-organize around a recognition of the criticality of software to its business model, cataclysimic changes will ripple through the economy. To the extent that the growth of the base of software talent cannot keep up with demand, companies and entire industry sectors will start to collapse as they fail to adequately fuel their need for software talent.", new DateTime(2011, 12, 6, 9, 01, 0));
            SubmitItem("New NuGet.org Deployed", "Phil Haack's last day at Microsoft wasn't the usual winding down that happens whilst clearing your desk. Instead he [announced the deployment of an updated version of the NuGet.org site](http://haacked.com/archive/2011/12/05/nuget_org_update.aspx), a complete rewrite using vanilla ASP.NET MVC 3.", new DateTime(2011, 12, 6, 11, 11, 0));
            SubmitItem("Previewing the Windows Store", "Antoine Leblond [introduces the Windows Store](http://blogs.msdn.com/b/windowsstore/archive/2011/12/06/announcing-the-new-windows-store.aspx) in the first post of a new [Windows Store for developers blog](http://blogs.msdn.com/b/windowsstore/). The article includes details about the registration fee ($49 USD for individuals, $99 USD for companies) and revenue split between Microsoft and App developers.\r\n\r\n> The revenue share base is 70%, but when an app achieves $25k USD in revenue—aggregated across all sales in every market—that app moves to 80% revenue share for the lifetime of that app.", new DateTime(2011, 12, 7, 17, 39, 0));
            SubmitItem("Win a Free Copy of Nhibernate 3 Beginner's Guide", "Ayende is [giving away three copies](http://ayende.com/blog/144385/win-free-copies-of-nhibernate-3-beginners-guide) of the NHibernate 3 Beginner’s Guide book.", new DateTime(2011, 12, 13, 13, 9, 1));
            SubmitItem("Lessons Learnt Building a Shrinkwrap .NET Server Product", "Paul Stovell writes about some of the lessons he has learnt [building a shrinkwrap .NET server product](http://www.paulstovell.com/lessons-learnt-bulding-octopus) called [Octopus](http://octopusdeploy.com/) for automated ASP.NET deployment.", new DateTime(2011, 12, 16, 11, 53, 0));
            SubmitItem("Node.js in Windows Azure, to the Cloud and Beyond!", "[Glenn Block covers the announcement](http://codebetter.com/glennblock/2011/12/16/node-js-in-windows-azure-to-the-cloud-and-beyond-2) by Scott Guthrie yesterday, at the Learn Windows Azure event, of Microsoft's support for node.js for developers using Windows Azure. Along with that announcement was the unveiling of a [Node.js Developer Center](http://www.windowsazure.com/en-us/develop/nodejs/).", new DateTime(2011, 12, 16, 12, 9, 1));
            SubmitItem("Effective Aggregate Design Part III by Vaughn Vernon", "[Part III](http://dddcommunity.org/sites/default/files/pdf_articles/Vernon_2011_3.pdf) of Vaughn Vernon's [Effective Aggregate Design](http://dddcommunity.org/library/vernon_2011) series concludes with a discussion of the discovery process. How to recognize when a design problem is a hint of a new insight, and how different aggregate models are tried and then superseded.", new DateTime(2011, 12, 21, 18, 25, 0));
            SubmitItem("2011 in Review: 15 Web Conference Talks You Need to Watch", "Simon Willison, the co-founder and CEO of Lanyrd, presents [15 of the best technology conference talks of 2011](http://www.netmagazine.com/features/2011-review-15-web-conference-talks-you-need-watch) for .net magazine.", new DateTime(2011, 12, 27, 10, 51, 0));
            SubmitItem("ASP.NET Security Update Shipping Thursday, Dec 29th", "[Microsoft has released an advance notification security bulletin](http://weblogs.asp.net/scottgu/archive/2011/12/28/asp-net-security-update-shipping-thursday-dec-29th.aspx) announcing that an out-of-band security update to address an ASP.NET Security Vulnerability is to be released on December 29, 2011.", new DateTime(2011, 12, 29, 10, 2, 0));
        }

        private void SubmitItem(string title, string content, DateTime publishedAt)
        {
            var itemId = _idGenerator.GenerateNewId();

            var submission = new SubmitNewItemCommand
            {
                UserId = _userId,
                ItemId = itemId,
                FullName = "Ben Smith",
                Username = "ben",
                Title = title,
                Content = content,
                SubmittedAt = publishedAt.AddMinutes(-10)
            };

            WriteCommand(submission);

            var publish = new PublishItemCommand
            {
                ApprovedBy = _userId,
                ItemId = itemId,
                PublishedAt = publishedAt,
                UrlSlug = _urlSlugGenerator.Slugify(submission.Title)
            };

            WriteCommand(publish);
        }

        private void WriteCommand(ICommand command)
        {
            var outputFile = Path.Combine(SeedDirectory, string.Format("{0} {1}.json", _commandIndex.ToString().PadLeft(4, '0'), command.GetType().Name));

            File.WriteAllText(outputFile, command.ToJson());

            _commandIndex += 1;
        }
    }
}