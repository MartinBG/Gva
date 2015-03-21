using Docs.Api.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.Models.DomainModels
{
    public class UnitDomainModel
    {
        public UnitDomainModel()
        {
            ChildUnits = new List<UnitDomainModel>();
            Classifications = new List<UnitClassificationDomainModel>();
        }

        public int UnitId { get; set; }
        public int? ParentUnitId { get; set; }
        public int RootUnitId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public ICollection<UnitDomainModel> ChildUnits { get; set; }
        public ICollection<UnitClassificationDomainModel> Classifications { get; set; }

        public void AddChild(UnitDomainModel item)
        {
            ChildUnits.Add(item);
        }
    }
}
