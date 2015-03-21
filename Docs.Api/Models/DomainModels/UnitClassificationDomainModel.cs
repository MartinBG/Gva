
namespace Docs.Api.Models.DomainModels
{
    public class UnitClassificationDomainModel
    {
        public UnitClassificationDomainModel()
        {
        }

        public int UnitClassificationId { get; set; }
        public int ClassificationId { get; set; }
        public string ClassificationName { get; set; }
        // Enum type
        public string ClassificationPermission { get; set; }
    }
}
