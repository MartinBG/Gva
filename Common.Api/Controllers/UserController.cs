using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using System;
using Common.Linq;
using Common.Api.DataObjects;
using System.Collections.Generic;
using System.Data.Entity;
using Common.Api.Repositories.UserRepository;
using System.Net.Http.Formatting;

namespace Common.Api.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        public UserController(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
        }

        private IUnitOfWork unitOfWork;
        private IUserRepository userRepository;

        public IHttpActionResult GetUserData()
        {
            int userId = this.Request.GetUserContext().UserId;
            var user = this.unitOfWork.DbContext.Set<User>()
                .Include(e => e.Roles)
                .SingleOrDefault(u => u.UserId == userId);

            List<string> permissions = new List<string>();

            foreach (var item in user.Roles)
            {
                if (!string.IsNullOrEmpty(item.Permissions))
                {
                    permissions.AddRange(item.Permissions.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Trim()));
                }
            }

            return Ok(new
            {
                userFullname = user.Fullname,
                permissions = permissions.Distinct()
            });
        }

        public IHttpActionResult GetUsers(
            string username = null,
            string fullname = null,
            string showActive = null)
        {
            var predicate = PredicateBuilder.True<User>()
                .AndStringContains(u => u.Username, username)
                .AndStringContains(u => u.Fullname, fullname);

            if (!string.IsNullOrEmpty(showActive)) 
            { 
                bool isActive = showActive == "yes" ? true : false;
                predicate = predicate.And(u => u.IsActive == isActive);
            }

            List<UserListItemDO> returnValue = this.unitOfWork.DbContext.Set<User>()
                .Include(e => e.Roles)
                .Where(predicate)
                .ToList()
                .Select(e => new UserListItemDO(e))
                .ToList();

            return Ok(returnValue);
        }

        public IHttpActionResult GetUser(int id)
        {
            User user = this.unitOfWork.DbContext.Set<User>()
                .Include(e => e.Roles)
                .SingleOrDefault(e => e.UserId == id);

            //UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>()
            //    .SingleOrDefault(e => e.UserId == id);

            UserDO returnValue = new UserDO(user);
            //if (unitUser != null)
            //{
            //    returnValue.UnitId = unitUser.UnitId;
            //}

            return Ok(returnValue);
        }

        public IHttpActionResult UpdateUser(int id, UserDO user)
        {
            User oldUser = this.unitOfWork.DbContext.Set<User>()
                .Include(e => e.Roles)
                .SingleOrDefault(e => e.UserId == id);

            //UnitUser oldUnitUser = this.unitOfWork.DbContext.Set<UnitUser>()
            //    .SingleOrDefault(e => e.UserId == id);

            //if (oldUser == null)
            //{
            //    return NotFound();
            //}

            oldUser.Fullname = user.Fullname;
            oldUser.CertificateThumbprint = user.CertificateThumbprint.Replace(" ", null).ToUpperInvariant();
            oldUser.IsActive = user.IsActive;
            oldUser.AppointmentDate = user.AppointmentDate;
            oldUser.ResignationDate = user.ResignationDate;
            oldUser.Notes = user.Notes;
            oldUser.Version = user.Version;
            oldUser.Email = user.Email;

            //if (oldUnitUser != null)
            //{
            //    oldUnitUser.UnitId = user.UnitId.Value;
            //}
            //else
            //{
            //    UnitUser unitUser = new UnitUser();

            //    unitUser.UserId = oldUser.UserId;
            //    unitUser.UnitId = user.UnitId.Value;
            //    unitUser.IsActive = true;

            //    this.unitOfWork.DbContext.Set<UnitUser>().Add(unitUser);
            //}

            if (!String.IsNullOrEmpty(user.Password))
            {
                oldUser.SetPassword(user.Password);
            }

            bool newRole;
            bool oldRole;

            foreach (var role in this.unitOfWork.DbContext.Set<Role>().ToList())
            {
                newRole = user.Roles.Any(r => r.RoleId == role.RoleId);
                oldRole = oldUser.Roles.Any(r => r.RoleId == role.RoleId);

                if (newRole && !oldRole)
                    oldUser.Roles.Add(this.unitOfWork.DbContext.Set<Role>().Find(role.RoleId));

                if (!newRole && oldRole)
                    oldUser.Roles.Remove(this.unitOfWork.DbContext.Set<Role>().Find(role.RoleId));
            }

            this.unitOfWork.Save();

            //
            int unitId = user.UnitId.Value;

            //var oldUnitClassifications = this.unitOfWork.DbContext.Set<UnitClassification>()
            //    .Where(e => e.UnitId == unitId)
            //    .ToList();

            //this.unitOfWork.DbContext.Set<UnitClassification>().RemoveRange(oldUnitClassifications);

            //List<int> roleIds = user.Roles.Select(e => e.RoleId).ToList();

            //var roleClassifications = this.unitOfWork.DbContext.Set<RoleClassification>()
            //    .Where(e => roleIds.Contains(e.RoleId))
            //    .ToList();

            //foreach (var item in roleClassifications)
            //{
            //    UnitClassification uc = new UnitClassification();
            //    uc.UnitId = unitId;
            //    uc.ClassificationId = item.ClassificationId;
            //    uc.ClassificationPermissionId = item.ClassificationPermissionId;

            //    this.unitOfWork.DbContext.Set<UnitClassification>().Add(uc);
            //}

            this.unitOfWork.Save();

            this.userRepository.spSetUnitTokens(unitId);

            return Ok();
        }

        public IHttpActionResult CreateUser(UserDO user)
        {
            User newUser = new User();

            newUser.Username = user.Username;
            newUser.Fullname = user.Fullname;
            newUser.HasPassword = true;
            newUser.CertificateThumbprint = user.CertificateThumbprint.Replace(" ", null).ToUpperInvariant();
            newUser.IsActive = user.IsActive;
            newUser.AppointmentDate = user.AppointmentDate;
            newUser.ResignationDate = user.ResignationDate;
            newUser.Notes = user.Notes;
            newUser.Version = user.Version;
            newUser.Email = user.Email;

            //UnitUser unitUser = new UnitUser();

            //unitUser.User = newUser;
            //unitUser.UnitId = user.UnitId.Value;
            //unitUser.IsActive = true;

            newUser.SetPassword(user.Password);

            foreach (var role in user.Roles)
            {
                newUser.Roles.Add(this.unitOfWork.DbContext.Set<Role>().Find(role.RoleId));
            }

            this.unitOfWork.DbContext.Set<User>().Add(newUser);
            //this.unitOfWork.DbContext.Set<UnitUser>().Add(unitUser);

            this.unitOfWork.Save();

            //
            int unitId = user.UnitId.Value;

            //var oldUnitClassifications = this.unitOfWork.DbContext.Set<UnitClassification>()
            //    .Where(e => e.UnitId == unitId)
            //    .ToList();

            //this.unitOfWork.DbContext.Set<UnitClassification>().RemoveRange(oldUnitClassifications);

            List<int> roleIds = user.Roles.Select(e => e.RoleId).ToList();

            //var roleClassifications = this.unitOfWork.DbContext.Set<RoleClassification>()
            //    .Where(e => roleIds.Contains(e.RoleId))
            //    .ToList();

            //foreach (var item in roleClassifications)
            //{
            //    UnitClassification uc = new UnitClassification();
            //    uc.UnitId = unitId;
            //    uc.ClassificationId = item.ClassificationId;
            //    uc.ClassificationPermissionId = item.ClassificationPermissionId;

            //    this.unitOfWork.DbContext.Set<UnitClassification>().Add(uc);
            //}

            this.unitOfWork.Save();

            this.userRepository.spSetUnitTokens(unitId);

            return Ok();
        }

        //[HttpGet]
        //public IHttpActionResult CheckDuplicateUnit(int unitId, int? userId = null)
        //{
        //    bool result = userId.HasValue ?
        //        this.unitOfWork.DbContext.Set<UnitUser>().Any(e => e.UnitId == unitId && e.UserId != userId.Value) :
        //        this.unitOfWork.DbContext.Set<UnitUser>().Any(e => e.UnitId == unitId);

        //    return Ok(new
        //    {
        //        result = result
        //    });
        //}

        public IHttpActionResult GetRoles()
        {
            List<RoleDO> returnValue = this.unitOfWork.DbContext.Set<Role>()
                .ToList()
                .Select(e => new RoleDO(e))
                .ToList();

            return Ok(returnValue);
        }

        public IHttpActionResult ChangeCurrentUserPassword(PasswordsDO passwords)
        {
            var userContext = this.Request.GetUserContext();

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                User user = this.unitOfWork.DbContext.Set<User>()
                .Include(e => e.Roles)
                .SingleOrDefault(e => e.UserId == userContext.UserId);

                user.ChangePassword(passwords.OldPassword, passwords.NewPassword);

                this.unitOfWork.Save();

                transaction.Commit();
            }

            return Ok();
        }

        public IHttpActionResult IsCorrectPassword([FromBody] FormDataCollection formData)
        {
            var userContext = this.Request.GetUserContext();

            bool isCorrect = false;
            if (formData["password"] != null)
            {
                User user = this.unitOfWork.DbContext.Set<User>()
                .Include(e => e.Roles)
                .SingleOrDefault(e => e.UserId == userContext.UserId);

                if (user != null)
                {
                    isCorrect = user.VerifyPassword(formData["password"]);
                }
            }

            return Ok(new
            {
                isCorrect = isCorrect
            });
        }
    }
}
