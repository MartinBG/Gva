using System.Data.Entity;
using CodeFirstStoreFunctions;
using Common.Data;

namespace Common.Api.Models
{
    public class CommonDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new FunctionsConvention("dbo", typeof(DbContextExtensions)));
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new MunicipalityMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new SettlementMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new NomMap());
            modelBuilder.Configurations.Add(new NomValueMap());
            modelBuilder.Configurations.Add(new BlobMap());
        }
    }
}
