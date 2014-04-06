using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gva.MigrationTool
{
    /// <summary>
    /// Supports shaping IDbCommand and IDbDataReader as CLR instances.
    /// </summary>
    /// <remarks>
    /// This type is thread-safe. For performance reasons, static instances of this type
    /// should be shared wherever possible. Note that a single instance of the Materializer
    /// cannot be used with two commands returning different fields.
    /// </remarks>
    /// <typeparam name="T">CLR type to materialize.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Materializer")]
    public sealed class Materializer<T>
    {
        private static readonly ParameterExpression s_recordParameter = Expression.Parameter(typeof(IDataRecord), "record");
        private static readonly MethodInfo s_fieldOfTMethod = typeof(DataExtensions).GetMethod("Field", new Type[] { typeof(IDataRecord), typeof(int) });

        private readonly Expression<Func<IDataRecord, T>> userSuppliedShaper;
        private readonly object syncLock = new object();

        private Func<IDataRecord, T> shaperDelegate;
        private ReadOnlyCollection<string> fieldNames;

        /// <summary>
        /// Default constructor. Instances of T are materialized by assigning field values to
        /// writable properties on T having the same name. By default, allows fields
        /// that do not have corresponding properties and properties that do not have corresponding
        /// fields.
        /// </summary>
        public Materializer()
        {
        }

        /// <summary>
        /// Instances of T are materialized using the given shaper. For every row in the result,
        /// the shaper is evaluated.
        /// </summary>
        /// <param name="shaper">Describes how reader rows are transformed into typed results.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public Materializer(Expression<Func<IDataRecord, T>> shaper)
        {
            this.userSuppliedShaper = shaper;
        }

        /// <summary>
        /// Materializes the results of the given command.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <returns>Shaped results.</returns>
        public IEnumerable<T> Materialize(IDbCommand command)
        {
            return Materialize(command, CommandBehavior.Default);
        }

        /// <summary>
        /// Materializes the results of the given command.
        /// </summary>
        /// <param name="command">Command to execute.</param>
        /// <param name="commandBehavior">Command behavior to use when executing the command.</param>
        /// <returns>Shaped results.</returns>
        public IEnumerable<T> Materialize(IDbCommand command, CommandBehavior commandBehavior)
        {
            using (command.Connection.CreateConnectionScope())
            {
                using (IDataReader reader = command.ExecuteReader(commandBehavior))
                {
                    foreach (T element in this.Materialize(reader))
                    {
                        yield return element;
                    }
                }
            }
        }

        /// <summary>
        /// Materializes rows in the given reader.
        /// </summary>
        /// <param name="reader">Results to materialize.</param>
        /// <returns>Shaped results.</returns>
        public IEnumerable<T> Materialize(IDataReader reader)
        {
            bool first = true;
            while (reader.Read())
            {
                if (first)
                {
                    InitializeShaper(reader);
                    first = false;
                }

                yield return this.shaperDelegate(reader);
            }

            yield break;
        }

        private void InitializeShaper(IDataRecord record)
        {
            // Determine the layout of the record.
            if (null != this.fieldNames)
            {
                // If a record layout has already been established, make sure the current
                // record is compatible with it.
                ValidateFieldNames(record);
            }
            else
            {
                // Initialize a new shaper delegate within a lock (first one wins). 
                lock (this.syncLock)
                {
                    if (null != this.fieldNames)
                    {
                        // another thread beat us to it...
                        ValidateFieldNames(record);
                    }
                    else
                    {
                        // if the user didn't provide an explicit shaper, generate a default shaper
                        // based on the element type and the record layout.
                        ReadOnlyCollection<string> recordFieldNames = GetFieldNames(record);
                        Expression<Func<IDataRecord, T>> shaper = this.userSuppliedShaper ?? GetDefaultShaper(recordFieldNames);

                        // compile the expression
                        Func<IDataRecord, T> compiledShaper = shaper.Compile();

                        // lock down the Materializer instance to use the (first encountered) field information and delegate
                        this.fieldNames = recordFieldNames;
                        this.shaperDelegate = compiledShaper;
                    }
                }
            }
        }

        private static ReadOnlyCollection<string> GetFieldNames(IDataRecord record)
        {
            List<string> fieldNames = new List<string>(record.FieldCount);
            fieldNames.AddRange(Enumerable.Range(0, record.FieldCount)
                .Select(i => record.GetName(i)));
            return fieldNames.AsReadOnly();
        }

        private void ValidateFieldNames(IDataRecord record)
        {
            if (this.fieldNames.Count != record.FieldCount ||
                this.fieldNames.Where((fieldName, ordinal) => record.GetName(ordinal) != fieldName)
                .Any())
            {
                throw new InvalidOperationException(
                    "The given reader is incompatible with the current materializer. Create a different materializer for this reader.");
            }
        }

        private static Expression<Func<IDataRecord, T>> GetDefaultShaper(ReadOnlyCollection<string> fieldNames)
        {
            // get constructor
            ConstructorInfo defaultConstructor = typeof(T).GetConstructor(Type.EmptyTypes);
            if (null == defaultConstructor)
            {
                throw new InvalidOperationException(
                    "Unable to build a default materialization delegate for this type. Initialize a materializer with a specific delegate definition.");
            }

            // figure out which fields/properties have corresponding columns
            List<MemberBinding> memberBindings = new List<MemberBinding>();
            int ordinal = 0;
            foreach (string columnName in fieldNames)
            {
                MemberBinding memberBinding;
                if (TryCreateMemberBinding(columnName, ordinal, out memberBinding))
                {
                    memberBindings.Add(memberBinding);
                }
                ordinal++;
            }

            // record => new T { ColumnName = record.Field<TColumn>(columnOrdinal), ... }
            return Expression.Lambda<Func<IDataRecord, T>>(
                Expression.MemberInit(
                    Expression.New(defaultConstructor),
                    memberBindings),
                s_recordParameter);
        }

        private static bool TryCreateMemberBinding(string columnName, int ordinal, out MemberBinding memberBinding)
        {
            PropertyInfo propertyInfo = typeof(T).GetProperty(columnName);
            if (null != propertyInfo)
            {
                if (propertyInfo.GetIndexParameters().Length == 0 && propertyInfo.CanWrite)
                {
                    memberBinding = Expression.Bind(propertyInfo.GetSetMethod(), CreateGetValueCall(propertyInfo.PropertyType, ordinal));
                    return true;
                }
            }
            memberBinding = null;
            return false;
        }

        private static Expression CreateGetValueCall(Type type, int ordinal)
        {
            MethodInfo fieldOfTMethod = s_fieldOfTMethod.MakeGenericMethod(type);
            return Expression.Call(fieldOfTMethod, s_recordParameter, Expression.Constant(ordinal));
        }

    }
}
