using Common.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regs.Api.Models
{
    public class RegisDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CommitMap());
            modelBuilder.Configurations.Add(new LotMap());
            modelBuilder.Configurations.Add(new NomMap());
            modelBuilder.Configurations.Add(new NomValueMap());
            modelBuilder.Configurations.Add(new PartMap());
            modelBuilder.Configurations.Add(new PartExtMap());
            modelBuilder.Configurations.Add(new PartOperationMap());
            modelBuilder.Configurations.Add(new PartVersionMap());
            modelBuilder.Configurations.Add(new SetMap());
            modelBuilder.Configurations.Add(new SetPartMap());
            modelBuilder.Configurations.Add(new TextBlobMap());
        }
    }
}
