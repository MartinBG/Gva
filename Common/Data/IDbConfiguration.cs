using System.Data.Entity;

namespace Common.Data
{
    public interface IDbConfiguration
    {
        void AddConfiguration(DbModelBuilder modelBuilder);
    }
}
