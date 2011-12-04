using System;

namespace DotNetFlow.Core.Exceptions
{
    public class PublishedItemNotFoundException : Exception
    {
        public PublishedItemNotFoundException(string message) : base(message)
        {            
        }
    }
}
