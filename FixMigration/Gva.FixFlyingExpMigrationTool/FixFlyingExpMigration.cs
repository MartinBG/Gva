using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using Autofac;
using Common;
using Common.Api;
using Common.Api.UserContext;
using Common.Data;
using Docs.Api;
using Gva.Api;
using Gva.FixFlyingExpMigrationTool.Sets;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using Regs.Api;

namespace Gva.FixFlyingExpMigrationTool
{
    class FixFlyingExpMigration
    {
        private static ContainerBuilder CreateGvaContainerBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CommonModule());
            builder.RegisterModule(new CommonApiModule());
            builder.RegisterModule(new DocsApiModule());
            builder.RegisterModule(new GvaApiModule());
            builder.RegisterModule(new RegsApiModule());

            builder.Register(c => new UserContext(2)).InstancePerLifetimeScope();
            builder.Register(c =>
            {
                var unitOfWork = new UnitOfWork(c.Resolve<IEnumerable<IDbConfiguration>>(), c.Resolve<IEnumerable<IDbContextInitializer>>());
                unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;
                return unitOfWork;
            }).As<IUnitOfWork>().InstancePerLifetimeScope();

            return builder;
        }

        private static IContainer CreateSetContainer()
        {
            ContainerBuilder builder = CreateGvaContainerBuilder();

            builder.Register(c => new OracleConnection(ConfigurationManager.ConnectionStrings["Apex"].ConnectionString)).InstancePerLifetimeScope();
            builder.Register(c => new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString)).InstancePerLifetimeScope();

            builder.RegisterType<Person>().InstancePerLifetimeScope();
            builder.RegisterType<PersonLotFixMigrator>().InstancePerLifetimeScope();

            return builder.Build();
        }

        static void Main(string[] args)
        {
            JsonConvert.DefaultSettings = () =>
            {
                return new JsonSerializerSettings()
                {
#if DEBUG
                    Formatting = Formatting.Indented,
#else
                    Formatting = Formatting.None,
#endif
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Include,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
            };

            var setContainer = CreateSetContainer();
            using (var scope = setContainer.BeginLifetimeScope())
            {
                OracleConnection oracleConn = scope.Resolve<OracleConnection>();
                SqlConnection sqlConn = scope.Resolve<SqlConnection>();

                Stopwatch timer = new Stopwatch();
                timer.Start();

                oracleConn.Open();
                sqlConn.Open();

                var person = scope.Resolve<Person>();

                person.fixPersonsFlyingExperiencesMigration();
            }

            Console.WriteLine("Fix of migration of flying experiences finished!");
        }
    }
}
