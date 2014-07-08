using Common.Api.Models;
using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class UnitUserDO
    {
        public UnitUserDO()
        {
        }

        public UnitUserDO(UnitUser u)
            : this()
        {
            if (u != null)
            {
                this.UnitUserId = u.UnitUserId;
                this.UserId = u.UserId;
                this.UnitId = u.UnitId;
                this.IsActive = u.IsActive;
                this.Version = u.Version;

                if (u.User != null)
                {
                    this.Username = u.User.Username;
                }

                if (u.Unit != null)
                {
                    this.UnitName = u.Unit.Name;
                }
            }
        }

        public int UnitUserId { get; set; }
        public int UserId { get; set; }
        public int UnitId { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }

        public string Username { get; set; }
        public string UnitName { get; set; }
    }
}
