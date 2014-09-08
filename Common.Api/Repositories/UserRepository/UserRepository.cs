using Common.Api.Models;
using Common.Data;
using Common.Extensions;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Common.Api.Repositories.UserRepository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public User GetUser(int userId)
        {
            return this.unitOfWork.DbContext.Set<User>().Find(userId);
        }

        public void spSetUnitTokens(int? unitId = null)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("UnitId", Helper.CastToSqlDbValue(unitId)));

            this.ExecuteSqlCommand("spSetUnitTokens @UnitId", parameters);
        }
    }
}
