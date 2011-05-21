using Ncqrs.Commanding;

namespace DotNetFlow.Core.Commands
{
    public sealed class SubmitNewItemCommand : CommandBase
    {
        public string UsersName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public SubmitNewItemCommand()
        {            
        }

        public SubmitNewItemCommand(string usersName, string title, string content)
        {
            UsersName = usersName;
            Title = title;
            Content = content;
        }
    }
}