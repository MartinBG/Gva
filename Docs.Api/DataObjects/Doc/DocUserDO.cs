using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocUserDO
    {
        public DocUserDO()
        {
        }

        public DocUserDO(DocUser d)
        {
            if (d != null)
            {
                this.DocId = d.DocId;
                this.UnitId = d.UnitId;
                this.DocUnitPermissionId = d.DocUnitPermissionId;
                this.HasRead = d.HasRead;
                this.IsActive = d.IsActive;
                this.ActivateDate = d.ActivateDate;
                this.DeactivateDate = d.DeactivateDate;

                if (d.Unit != null)
                {
                    this.UnitName = d.Unit.Name;
                }

                if (d.DocUnitPermission != null)
                {
                    this.DocUnitPermissionName = d.DocUnitPermission.Name;
                }
            }
        }

        public int DocId { get; set; }
        public int UnitId { get; set; }
        public int DocUnitPermissionId { get; set; }
        public bool HasRead { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ActivateDate { get; set; }
        public DateTime? DeactivateDate { get; set; }

        public string UnitName { get; set; }
        public string DocUnitPermissionName { get; set; }
    }
}
