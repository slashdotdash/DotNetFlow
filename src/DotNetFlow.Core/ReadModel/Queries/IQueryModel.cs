using System.Collections.Generic;

namespace DotNetFlow.Core.ReadModel.Queries
{
    public interface IQueryModel<out TModel>
    {
        IEnumerable<TModel> Execute();
    }
}