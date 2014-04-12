using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace Gva.MigrationTool
{
    public static class DataExtensions
    {
        public static T Field<T>(this IDataRecord record, string name)
        {
            return Field<T>(record, record.GetOrdinal(name));
        }

        public static T Field<T>(this IDataRecord record, int ordinal)
        {
            object value = record.IsDBNull(ordinal) ? null : record.GetValue(ordinal);

            return (T)value;
        }

        public static IEnumerable<T> Materialize<T>(this IDbCommand command, Expression<Func<IDataRecord, T>> expression)
        {
            return new Materializer<T>(expression).Materialize(command);
        }

        public static T MaterializeScalar<T>(this IDbCommand command)
        {
            using (command.Connection.CreateConnectionScope())
            {
                object result = command.ExecuteScalar();

                if (Convert.IsDBNull(result))
                    return default(T);

                return (T)result;
            }
        }

        public static DbCommand CreateStoreCommand(this DbConnection connection, string commandText, params DbClause[] clauses)
        {
            return CreateStoreCommand(connection, null, commandText, CommandType.Text, clauses);
        }

        public static DbCommand CreateStoreCommand(this DbConnection connection, DbTransaction transaction, string commandText, params DbClause[] clauses)
        {
            return CreateStoreCommand(connection, transaction, commandText, CommandType.Text, clauses);
        }

        public static DbCommand CreateStoreCommand(this DbConnection connection, string commandText, CommandType commandType, params DbClause[] clauses)
        {
            return CreateStoreCommand(connection, null, commandText, CommandType.Text, clauses);
        }

        public static DbCommand CreateStoreCommand(this DbConnection connection, DbTransaction transaction, string commandText, CommandType commandType, params DbClause[] clauses)
        {
            DbCommand command = connection.CreateCommand();

            if (transaction != null)
                command.Transaction = transaction;

            string[] clauseTexts = new string[clauses.Length];
            List<DbParameter> dbParameters = new List<DbParameter>();

            int i = 0;
            foreach (DbClause clause in clauses)
            {
                if (!clause.IsEnabled)
                {
                    i++;
                    continue;
                }

                List<string> clauseParameterNames = new List<string>();

                int j = 0;
                foreach (object val in clause.Parameters)
                {
                    DbParameter param = command.CreateParameter();
                    if (connection is Oracle.DataAccess.Client.OracleConnection)
                    {
                        param.ParameterName = String.Format(":P{0}{1}", i, j);
                    }
                    else
                    {
                        param.ParameterName = String.Format("@P{0}{1}", i, j);
                    }
                    param.Value = val;

                    dbParameters.Add(param);
                    clauseParameterNames.Add(param.ParameterName);

                    j++;
                }

                clauseTexts[i] = String.Format(clause.Text, clauseParameterNames.ToArray());

                i++;
            }

            command.CommandText = String.Format(commandText, clauseTexts);
            command.CommandType = commandType;
            if (dbParameters.Count != 0)
            {
                command.Parameters.AddRange(dbParameters.ToArray());
            }

            return command;
        }

        public static IDisposable CreateConnectionScope(this IDbConnection connection)
        {
            return new OpenConnectionLifetime(connection);
        }

        private class OpenConnectionLifetime : IDisposable
        {
            private readonly IDbConnection connection;
            private readonly bool closeOnDispose;

            internal OpenConnectionLifetime(IDbConnection connection)
            {
                this.connection = connection;
                this.closeOnDispose = connection.State == ConnectionState.Closed;
                if (this.closeOnDispose)
                {
                    this.connection.Open();
                }
            }

            public void Dispose()
            {
                if (this.closeOnDispose && this.connection.State == ConnectionState.Open)
                {
                    this.connection.Close();
                }
                GC.SuppressFinalize(this);
            }
        }
    }
}
