using System.Data.Entity;
using Common.Data;

namespace Regs.Api.Models
{
    public class RegsDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CommitMap());
            modelBuilder.Configurations.Add(new CommitVersionMap());
            modelBuilder.Configurations.Add(new LotMap());
            modelBuilder.Configurations.Add(new PartMap());
            modelBuilder.Configurations.Add(new PartVersionMap());
            modelBuilder.Configurations.Add(new SetMap());
            modelBuilder.Configurations.Add(new SetPartMap());
        }
    }
}
