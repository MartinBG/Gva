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

                this.UnitName = d.Unit != null ? d.Unit.Name : string.Empty;
                this.DocUnitPermissionName = d.DocUnitPermission != null ? d.DocUnitPermission.Name : string.Empty;
            }
        }

        public int DocId { get; set; }
        public int UnitId { get; set; }
        public int DocUnitPermissionId { get; set; }
        public bool HasRead { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> ActivateDate { get; set; }
        public Nullable<System.DateTime> DeactivateDate { get; set; }

        public string UnitName { get; set; }
        public string DocUnitPermissionName { get; set; }
    }
}
