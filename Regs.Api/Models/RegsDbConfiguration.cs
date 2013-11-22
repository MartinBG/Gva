using System.Data.Entity;
using Common.Data;

namespace Regs.Api.Models
{
    public class RegsDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CommitMap());
            modelBuilder.Configurations.Add(new LotMap());
            modelBuilder.Configurations.Add(new NomMap());
            modelBuilder.Configurations.Add(new NomValueMap());
            modelBuilder.Configurations.Add(new PartMap());
            modelBuilder.Configurations.Add(new PartVersionMap());
            modelBuilder.Configurations.Add(new SetMap());
            modelBuilder.Configurations.Add(new SetPartMap());
            modelBuilder.Configurations.Add(new TextBlobMap());
        }
    }
}
