using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Newtonsoft.Json.Schema;

namespace Regs.Api.Models
{
    public class RegsDbContextInitializer : IDbContextInitializer
    {
        private static readonly object SyncRoot = new object();
        private static List<Set> sets;
        private static List<SetPart> setParts;
        private static List<Schema> schemas;
        private static bool isInitialized = false;

        private static void InitializeStatics(DbContext context)
        {
            if (!isInitialized)
            {
                lock (SyncRoot)
                {
                    if (!isInitialized)
                    {
                        schemas = context.Set<Schema>().ToList();
                        sets = context.Set<Set>()
                            .Include(s => s.Schemas)
                            .ToList();
                        setParts = context.Set<SetPart>()
                            .Include(sp => sp.Schema)
                            .ToList();

                        JsonSchemaResolver resolver = new JsonSchemaResolver();

                        foreach (var set in sets)
                        {
                            foreach (var schema in set.Schemas)
                            {
                                schema.JsonSchema = JsonSchema.Parse(schema.SchemaText, resolver);
                            }
                        }

                        foreach (var setPart in setParts)
                        {
                            if (setPart.Schema != null)
                            {
                                setPart.Schema.JsonSchema = JsonSchema.Parse(setPart.Schema.SchemaText, resolver);
                            }
                        }

                        isInitialized = true;
                    }
                }
            }
        }

        public void InitializeContext(DbContext context)
        {
            InitializeStatics(context);

            context.Configuration.AutoDetectChangesEnabled = false;

            foreach (Set set in sets)
            {
                context.Set<Set>().Attach(set);
            }

            foreach (SetPart setPart in setParts)
            {
                context.Set<SetPart>().Attach(setPart);
            }

            context.Configuration.AutoDetectChangesEnabled = true;
            context.ChangeTracker.DetectChanges();
        }
    }
}
