using Common.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Core;

namespace Common.Api.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected IUnitOfWork unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public TEntity Find(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            object[] keyValues = new object[] { id };

            return this.FindInStore(keyValues, includes);
        }

        public DbRawSqlQuery<TSpEntity> ExecProcedure<TSpEntity>(string procedureName, List<SqlParameter> parameters)
        {
            StringBuilder sb = new StringBuilder(procedureName + " ");

            for (int i = 0; i < parameters.Count; i++)
            {
                sb.AppendFormat("@{0}", parameters[i].ParameterName);
                if (i != parameters.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            return this.unitOfWork.DbContext.Database.SqlQuery<TSpEntity>(
                sb.ToString(),
                parameters.ToArray());
        }

        public DbRawSqlQuery<TSpEntity> SqlQuery<TSpEntity>(string sql, List<SqlParameter> parameters)
        {
            return this.unitOfWork.DbContext.Database.SqlQuery<TSpEntity>(sql, parameters.ToArray());
        }

        #region Protected methods

        protected List<T> FindInStore<T>(Tuple<string, object>[] keyValues, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)this.unitOfWork.DbContext).ObjectContext;
            ObjectSet<T> objectSet = objectContext.CreateObjectSet<T>();

            string quotedEntitySetName = string.Format(
                CultureInfo.InvariantCulture,
                "{0}.{1}",
                this.QuoteIdentifier(objectSet.EntitySet.EntityContainer.Name),
                this.QuoteIdentifier(objectSet.EntitySet.Name));

            var queryBuilder = new StringBuilder();
            queryBuilder.AppendFormat("SELECT VALUE X FROM {0} AS X WHERE ", quotedEntitySetName);

            var entityKeyValues = this.CreateEntityKey(objectSet.EntitySet, keyValues).EntityKeyValues;
            var parameters = new ObjectParameter[entityKeyValues.Length];

            for (var i = 0; i < entityKeyValues.Length; i++)
            {
                if (i > 0)
                {
                    queryBuilder.Append(" AND ");
                }

                var name = string.Format(CultureInfo.InvariantCulture, "p{0}", i.ToString(CultureInfo.InvariantCulture));
                queryBuilder.AppendFormat("X.{0} = @{1}", this.QuoteIdentifier(entityKeyValues[i].Key), name);
                parameters[i] = new ObjectParameter(name, entityKeyValues[i].Value);
            }

            IQueryable<T> query = objectContext.CreateQuery<T>(queryBuilder.ToString(), parameters);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList();
        }

        private EntityKey CreateEntityKey(System.Data.Entity.Core.Metadata.Edm.EntitySet entitySet, Tuple<string, object>[] keyValues)
        {
            if (keyValues == null || !keyValues.Any() || keyValues.Any(v => v == null))
            {
                throw new ArgumentException("Parameter keyValues cannot be empty or contain nulls.");
            }

            var keyNames = entitySet.ElementType.Members.Where(e => keyValues.Select(k => k.Item1).Contains(e.Name)).Select(e => e.Name).ToList();
            if (keyNames.Count != keyValues.Length)
            {
                throw new ArgumentException("Invalid number of key values.");
            }

            return new System.Data.Entity.Core.EntityKey(entitySet.EntityContainer.Name + "." + entitySet.Name, keyNames.Zip(keyValues, (name, value) => new KeyValuePair<string, object>(name, value.Item2)));
        }

        #endregion

        #region Private methods

        private TEntity FindInStore(object[] keyValues, params Expression<Func<TEntity, object>>[] includes)
        {
            ObjectContext objectContext = ((IObjectContextAdapter)this.unitOfWork.DbContext).ObjectContext;
            ObjectSet<TEntity> objectSet = objectContext.CreateObjectSet<TEntity>();

            string quotedEntitySetName = string.Format(
                CultureInfo.InvariantCulture,
                "{0}.{1}",
                this.QuoteIdentifier(objectSet.EntitySet.EntityContainer.Name),
                this.QuoteIdentifier(objectSet.EntitySet.Name));

            var queryBuilder = new StringBuilder();
            queryBuilder.AppendFormat("SELECT VALUE X FROM {0} AS X WHERE ", quotedEntitySetName);

            var entityKeyValues = this.CreateEntityKey(objectSet.EntitySet, keyValues).EntityKeyValues;
            var parameters = new ObjectParameter[entityKeyValues.Length];

            for (var i = 0; i < entityKeyValues.Length; i++)
            {
                if (i > 0)
                {
                    queryBuilder.Append(" AND ");
                }

                var name = string.Format(CultureInfo.InvariantCulture, "p{0}", i.ToString(CultureInfo.InvariantCulture));
                queryBuilder.AppendFormat("X.{0} = @{1}", this.QuoteIdentifier(entityKeyValues[i].Key), name);
                parameters[i] = new ObjectParameter(name, entityKeyValues[i].Value);
            }

            IQueryable<TEntity> query = objectContext.CreateQuery<TEntity>(queryBuilder.ToString(), parameters);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.SingleOrDefault();
        }

        private string QuoteIdentifier(string identifier)
        {
            return "[" + identifier.Replace("]", "]]") + "]";
        }

        private EntityKey CreateEntityKey(System.Data.Entity.Core.Metadata.Edm.EntitySet entitySet, object[] keyValues)
        {
            if (keyValues == null || !keyValues.Any() || keyValues.Any(v => v == null))
            {
                throw new ArgumentException("Parameter keyValues cannot be empty or contain nulls.");
            }

            var keyNames = entitySet.ElementType.KeyMembers.Select(m => m.Name).ToList();
            if (keyNames.Count != keyValues.Length)
            {
                throw new ArgumentException("Invalid number of key values.");
            }

            return new System.Data.Entity.Core.EntityKey(entitySet.EntityContainer.Name + "." + entitySet.Name, keyNames.Zip(keyValues, (name, value) => new KeyValuePair<string, object>(name, value)));
        }

        #endregion
    }
}
