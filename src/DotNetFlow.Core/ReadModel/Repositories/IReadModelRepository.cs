using System;
using System.Collections.Generic;

namespace DotNetFlow.Core.ReadModel.Repositories
{
    public interface IReadModelRepository<out T> 
    {
        T Get(Guid id);
        IEnumerable<T> All();
    }
}