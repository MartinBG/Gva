using System.Collections.Generic;

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
        public UserForUnitAttachmentDomainModel User { get; set; }
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
