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
            modelBuilder.Configurations.Add(new ClassificationRelationMap());
            modelBuilder.Configurations.Add(new ClassificationMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new MunicipalityMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new SettlementMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new NomMap());
            modelBuilder.Configurations.Add(new NomValueMap());
            modelBuilder.Configurations.Add(new BlobMap());
            modelBuilder.Configurations.Add(new UnitClassificationMap());
            modelBuilder.Configurations.Add(new UnitRelationMap());
            modelBuilder.Configurations.Add(new UnitMap());
            modelBuilder.Configurations.Add(new UnitTokenMap());
            modelBuilder.Configurations.Add(new UnitTypeMap());
            modelBuilder.Configurations.Add(new UnitUserMap());
        }
    }
}
