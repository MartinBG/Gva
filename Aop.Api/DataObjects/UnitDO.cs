using Common.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aop.Api.DataObjects
{
    public class UnitDO
    {
        public UnitDO()
        {
        }

        public UnitDO(Unit d)
        {
            if (d != null)
            {
                this.UnitId = d.UnitId;
                this.Name = d.Name;
                if (d.UnitRelations.Any())
                {
                    this.ParentId = d.UnitRelations.FirstOrDefault().ParentUnitId;
                    this.ParentName = d.UnitRelations.FirstOrDefault().ParentUnit != null ? d.UnitRelations.FirstOrDefault().ParentUnit.Name : "";
                }
                this.UnitTypeId = d.UnitTypeId;
                this.UnitTypeName = d.UnitType.Name;
                this.IsActive = d.IsActive;
                this.InheritParentClassification = d.InheritParentClassification;
                this.Version = d.Version;
            }
        }

        public int UnitId { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string ParentName { get; set; }
        public Nullable<int> UnitTypeId { get; set; }
        public string UnitTypeName { get; set; }
        public bool IsActive { get; set; }
        public bool InheritParentClassification { get; set; }
        public byte[] Version { get; set; }
    }
}
