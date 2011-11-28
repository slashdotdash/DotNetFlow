using System;

namespace DotNetFlow.Core.ReadModel.Repositories
{
    public sealed class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message)
        {            
        }
    }
}