using System.Data.Entity;
using Common.Data;

namespace Mosv.Api.Models
{
    public class MosvDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MosvViewAdmissionMap());
            modelBuilder.Configurations.Add(new MosvViewSignalMap());
            modelBuilder.Configurations.Add(new MosvViewSuggestionMap());
        }
    }
}
