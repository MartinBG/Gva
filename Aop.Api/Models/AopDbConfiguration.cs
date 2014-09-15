using System.Data.Entity;
using Common.Data;

namespace Aop.Api.Models
{
    public class AopDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AopApplicationMap());
            modelBuilder.Configurations.Add(new AopEmployerMap());
            modelBuilder.Configurations.Add(new AopPortalDocRelationsMap());

            modelBuilder.Configurations.Add(new AopApplicationTokenMap());
            modelBuilder.Configurations.Add(new vwAopApplicationUserMap());
        }
    }
}
