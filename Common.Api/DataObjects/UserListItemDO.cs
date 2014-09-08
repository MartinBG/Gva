using Common.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Api.DataObjects
{
    public class UserListItemDO
    {
        public UserListItemDO()
        {
            this.Roles = new List<RoleDO>();
        }

        public UserListItemDO(User u)
            : this()
        {
            if (u != null)
            {
                this.UserId = u.UserId;
                this.Username = u.Username;
                this.Fullname = u.Fullname;
                this.Email = u.Email;
                this.IsActive = u.IsActive;

                if (u.Roles != null && u.Roles.Any())
                {
                    this.Roles.AddRange(u.Roles.Select(e => new RoleDO(e)));
                }
            }
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        //
        public List<RoleDO> Roles { get; set; }
    }
}
