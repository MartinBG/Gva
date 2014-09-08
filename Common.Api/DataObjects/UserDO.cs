using Common.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Api.DataObjects
{
    public class UserDO
    {
        public UserDO()
        {
            this.Roles = new List<RoleDO>();
        }

        public UserDO(User u)
            : this()
        {
            if (u != null)
            {
                this.UserId = u.UserId;
                this.Username = u.Username;
                this.Fullname = u.Fullname;
                this.AppointmentDate = u.AppointmentDate;
                this.ResignationDate = u.ResignationDate;
                this.Notes = u.Notes;
                this.CertificateThumbprint = u.CertificateThumbprint;
                this.Email = u.Email;
                this.IsActive = u.IsActive;
                this.HasPassword = u.HasPassword;
                this.Version = u.Version;

                if (u.Roles != null && u.Roles.Any())
                {
                    this.Roles.AddRange(u.Roles.Select(e => new RoleDO(e)));
                }
            }
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public DateTime? ResignationDate { get; set; }
        public string Notes { get; set; }
        public string CertificateThumbprint { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool HasPassword { get; set; }
        public byte[] Version { get; set; }

        //
        public List<RoleDO> Roles { get; set; }
        public string Password { get; set; }
        public int? UnitId { get; set; }
    }
}
