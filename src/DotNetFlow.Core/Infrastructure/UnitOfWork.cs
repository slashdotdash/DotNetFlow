using System;
using System.Data;
using System.Data.SqlClient;

namespace DotNetFlow.Core.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private object _syncLock = new object();
        private readonly string _connectionString;
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        
        public UnitOfWork(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connectionString", "Connection string is null");

            _connectionString = connectionString;
        }

        public void Begin()
        {
            EnsureConnectionIsOpen();
        }

        public void Commit()
        {
            if (_transaction != null)
                _transaction.Commit();
        }

        public void Rollback()
        {
            if (_transaction != null)
                _transaction.Rollback();
        }
       
        public void Reset()
        {
            _connection = null;
        }

        public IDbConnection Connection
        {
            get
            {
                EnsureConnectionIsOpen();

                // Now raise an exception if the connection was still not opened
                if (_connection == null || _connection.State != ConnectionState.Open)
                    throw new InvalidOperationException("Connection has not yet been opened");

                return _connection;
            }
        }

        private void EnsureConnectionIsOpen()
        {
            if (_connection != null) return;

            lock (_syncLock)
            {
                if (_connection != null) return;

                _connection = new SqlConnection(_connectionString);
                _connection.Open();

                _transaction = _connection.BeginTransaction();
            }
        }

        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }

        public void Dispose()
        {
            if (_transaction != null)
                _transaction.Dispose();

            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}