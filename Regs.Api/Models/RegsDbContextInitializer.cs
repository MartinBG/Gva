using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;

namespace Regs.Api.Models
{
    public class RegsDbContextInitializer : IDbContextInitializer
    {
        private static readonly object SyncRoot = new object();
        private static List<Set> sets;
        private static List<SetPart> setParts;
        private static bool isInitialized = false;

        private static void InitializeStatics(DbContext context)
        {
            if (!isInitialized)
            {
                lock (SyncRoot)
                {
                    if (!isInitialized)
                    {
                        sets = context.Set<Set>().ToList();
                        setParts = context.Set<SetPart>().ToList();

                        isInitialized = true;
                    }
                }
            }
        }

        public void InitializeContext(System.Data.Entity.DbContext context)
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
