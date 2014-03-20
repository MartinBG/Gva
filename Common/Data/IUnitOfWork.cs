using System;
using System.Data;
using System.Data.Entity;

namespace Common.Data
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; }

        DbContextTransaction BeginTransaction();

        DbContextTransaction BeginTransaction(IsolationLevel isolationLevel);

        void Save();
    }
}
