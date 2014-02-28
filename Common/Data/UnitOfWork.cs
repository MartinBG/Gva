using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Common.Utils.Expressions;
using NLog;

namespace Common.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public static readonly string ContextName = "DbContext";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static readonly Func<EntityReference, EntityKey> RelatedEndCachedValueAccessor = ExpressionHelper.GetFieldAccessor<EntityReference, EntityKey>("_cachedForeignKey");

        private static object syncRoot = new object();

        private static DbCompiledModel compiledModel = null;

        private bool disposed = false;

        private DbContext context = null;

        public UnitOfWork(IEnumerable<IDbConfiguration> configurations)
        {
            Initialize(configurations);
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

#if DEBUG
                    this.context.Database.Log = s => Debug.WriteLine(s);
#endif
                }

                return this.context;
            }
        }

        public void Save()
        {
            try
            {
                this.context.ChangeTracker.DetectChanges();

                foreach (ObjectStateEntry entry in ((IObjectContextAdapter)this.context).ObjectContext.ObjectStateManager
                                                 .GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                                                 .Where(e => !e.IsRelationship))
                {
                    if (entry.RelationshipManager.GetAllRelatedEnds()
                        .Any(re =>
                            re is EntityReference &&
                            re.RelationshipSet.ElementType.RelationshipEndMembers
                                .Any(rem => rem.Name == re.TargetRoleName &&
                                    rem.DeleteBehavior == OperationAction.Cascade) &&
                            RelatedEndCachedValueAccessor((EntityReference)re).EntityContainerName.Contains("EntityHasNullForeignKey")))
                    {
                        ((IObjectContextAdapter)this.context).ObjectContext.DeleteObject(entry.Entity);
                    }
                }

                this.context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Logger.Error(
                            "Class: {0}, Property: {1}, Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName,
                            validationError.ErrorMessage);
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

        public DbContextTransaction BeginTransaction()
        {
            return this.DbContext.Database.BeginTransaction();
        }

        public DbContextTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return this.DbContext.Database.BeginTransaction(isolationLevel);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing && this.context != null)
                {
                    this.context.Dispose();
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

                        modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                        modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

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
    }
}
