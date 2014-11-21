using Common.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Api.DataObjects
{
    public class RoleDO
    {
        public RoleDO()
        {
        }

        public RoleDO(Role r)
        {
            if (r != null)
            {
                this.RoleId = r.RoleId;
                this.Name = r.Name;
                this.Permissions = r.Permissions;
                this.IsActive = r.IsActive;
                this.Version = r.Version;
            }
        }

        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Permissions { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }

        //
        public bool Selected { get; set; }
    }
}
