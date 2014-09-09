using System.Data.Entity;
using Common.Data;

namespace Aop.Api.Models
{
    public class AopDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AopApplicationMap());
            modelBuilder.Configurations.Add(new AopApplicationCriteriaMap());
            modelBuilder.Configurations.Add(new AopApplicationObjectMap());
            modelBuilder.Configurations.Add(new AopApplicationTypeMap());
            modelBuilder.Configurations.Add(new AopChecklistStatusMap());
            modelBuilder.Configurations.Add(new AopEmployerMap());
            modelBuilder.Configurations.Add(new AopEmployerTypeMap());
            modelBuilder.Configurations.Add(new AopProcedureStatusMap());
            modelBuilder.Configurations.Add(new AopPortalDocRelationsMap());

            modelBuilder.Configurations.Add(new AopApplicationTokenMap());
            modelBuilder.Configurations.Add(new vwAopApplicationUserMap());
        }
    }
}
