using System;

namespace DotNetFlow.Core.Infrastructure.Eventing
{
    public interface IUniqueIdentifierGenerator
    {
        Guid GenerateNewId();
    }
}