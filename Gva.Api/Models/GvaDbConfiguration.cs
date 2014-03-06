using System.Data.Entity;
using Common.Data;

namespace Gva.Api.Models
{
    public class GvaDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GvaApplicationMap());
            modelBuilder.Configurations.Add(new GvaAppLotFileMap());
            modelBuilder.Configurations.Add(new GvaFileMap());
            modelBuilder.Configurations.Add(new GvaLotFileMap());
            modelBuilder.Configurations.Add(new GvaLotFileTypeMap());
            modelBuilder.Configurations.Add(new GvaLotObjectMap());
            modelBuilder.Configurations.Add(new GvaPersonMap());
            modelBuilder.Configurations.Add(new GvaInventoryItemMap());
        }
    }
}
