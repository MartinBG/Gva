using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NLog;
using NLog.Internal;

namespace Common.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static object syncRoot = new object();

        private static DbCompiledModel compiledModel = null;

        private bool disposed = false;

        private DbContext context = null;

        public static readonly string ContextName = "DbContext";

        public UnitOfWork(IEnumerable<IDbConfiguration> configurations)
        {
            Initialize(configurations);
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Logger.Error("Class: {0}, Property: {1}, Error: {2}", validationErrors.Entry.Entity.GetType().FullName,
                                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && this.context != null)
                {
                    context.Dispose();
                }

                this.context = null;
                this.disposed = true;
            }
        }

        private static void Initialize(IEnumerable<IDbConfiguration> configurations)
        {
            if (compiledModel == null)
            {
                lock (syncRoot)
                {
                    if (compiledModel == null)
                    {
                        Database.SetInitializer<DbContext>(null);

                        DbModelBuilder modelBuilder = new DbModelBuilder();

                        foreach (IDbConfiguration configuration in configurations)
                        {
                            configuration.AddConfiguration(modelBuilder);
                        }

                        using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[ContextName].ConnectionString))
                        {
                            compiledModel = modelBuilder.Build(connection).Compile();
                        }
                    }
                }
            }
        }

        public DbContext DbContext
        {
            get
            {
                if (this.context == null)
                {
                    this.context = new DbContext("Name=" + ContextName, compiledModel);
                    this.context.Configuration.LazyLoadingEnabled = false;
                    this.context.Configuration.ProxyCreationEnabled = false;
                    this.context.Configuration.UseDatabaseNullSemantics = true;
                }

                return this.context;
            }
        }

        public DbContextTransaction BeginTransaction()
        {
            return this.DbContext.Database.BeginTransaction();
        }

        public DbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return this.DbContext.Database.BeginTransaction(isolationLevel);
        }
    }
}
