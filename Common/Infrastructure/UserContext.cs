using Common.Models;
using System.Linq;
using System.Security;

namespace Common.Infrastructure
{
    public class UserContext
    {
        private int userId;
        private string fullName;
        private bool hasPassword;
        private string[] permissions;

        public UserContext(User user)
        {
            this.userId = user.UserId;
            this.fullName = user.Fullname;
            this.hasPassword = user.HasPassword;
            this.permissions =
                user.Roles
                .SelectMany(r =>
                    r.Permissions
                    .Split(',')
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => s.Trim()))
                .Distinct()
                .ToArray();
        }

        public UserContext(int userId, string fullName, bool hasPassword, string[] permissions)
        {
            this.fullName = fullName;
            this.userId = userId;
            this.hasPassword = hasPassword;
            this.permissions = permissions;
        }

        public int UserId
        {
            get
            {
                return this.userId;
            }
        }

        public string FullName
        {
            get
            {
                if (this.fullName == null)
                {
                    return string.Empty;
                }

                return this.fullName;
            }
        }

        public bool HasPassword
        {
            get
            {
                return this.hasPassword;
            }
        }

        public string[] Permissions
        {
            get
            {
                return this.permissions;
            }
        }

        public bool Can(string action, string permissionObject)
        {
            return this.permissions.Contains(permissionObject + "#*") ||
                this.permissions.Contains(permissionObject + "#" + action);
        }

        public bool Can(string action, string[] permissionObjects)
        {
            if (permissionObjects.Length == 0)
            {
                return false;
            }

            foreach (var permissionObject in permissionObjects)
            {
                if (!this.Can(action, permissionObject))
                {
                    return false;
                }
            }

            return true;
        }

        public bool CanAll(string permissionObject)
        {
            return this.permissions.Contains(permissionObject + "#*");
        }

        public void Assert(string action, string permissionObject)
        {
            if (!this.Can(action, permissionObject))
            {
                throw new SecurityException("Access denied - insufficient privileges.");
            }
        }

        public void Assert(string action, string[] permissionObjects)
        {
            if (!this.Can(action, permissionObjects))
            {
                throw new SecurityException("Access denied - insufficient privileges.");
            }
        }
    }
}
