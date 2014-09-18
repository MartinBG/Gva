using Gva.Api.Models;

namespace Docs.Api.DataObjects
{
    public class ApplicationLotObjectDO
    {
        public ApplicationLotObjectDO(GvaLotObject lotObject)
        {
            this.SetPartName = lotObject.LotPart.SetPart.Name;
            this.SetPartAlias = lotObject.LotPart.SetPart.Alias;
            this.PartIndex = lotObject.LotPart.Index;
        }

        public string SetPartName { get; set; }

        public string SetPartAlias { get; set; }

        public int? PartIndex { get; set; }
    }
}
