using System.Data.Entity;

namespace Common.Data
{
    public interface IDbContextInitializer
    {
        void InitializeContext(DbContext context);
    }
}
