using System;
using System.Collections.Generic;

namespace DotNetFlow.Core.ReadModel.Repositories
{
    public interface IRepository<out T> 
    {
        T Get(Guid id);
        IEnumerable<T> All();
    }
}