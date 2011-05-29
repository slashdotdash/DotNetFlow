using System;

namespace DotNetFlow.Core.ReadModel.Repositories
{
    public interface IRepository<out T> 
    {
        T Get(Guid id);
    }
}