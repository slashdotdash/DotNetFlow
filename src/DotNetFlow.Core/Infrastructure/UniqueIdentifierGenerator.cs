using System;

namespace DotNetFlow.Core.Infrastructure
{
    public interface IUniqueIdentifierGenerator
    {
        Guid GenerateNewId();
    }
}