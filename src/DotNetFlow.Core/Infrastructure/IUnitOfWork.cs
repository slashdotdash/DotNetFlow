using System;
using System.Data;

namespace DotNetFlow.Core.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Initialize();
        void Commit();
        void Rollback();

        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
    }
}