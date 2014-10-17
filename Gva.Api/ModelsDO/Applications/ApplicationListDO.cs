using Gva.Api.Models;
using System;

namespace Gva.Api.ModelsDO.Applications
{
    public class ApplicationListDO
    {
        public int ApplicationId { get; set; }

        public int? DocId { get; set; }

        public string LotSetName { get; set; }

        public int? AppPartId { get; set; }

        public int? AppPartIndex { get; set; }

        public DateTime? AppPartDocumentDate { get; set; }

        public string AppPartDocumentNumber { get; set; }

        public string AppPartApplicationTypeName { get; set; }

        public string AppPartStatusName { get; set; }

        public int? PersonId { get; set; }

        public int? PersonLin { get; set; }

        public string PersonNames { get; set; }

        public int? GvaOrganizationId { get; set; }

        public string GvaOrganizationName { get; set; }

        public string GvaOrganizationUin { get; set; }

        public int? GvaAircraftId { get; set; }

        public string GvaAirCategory { get; set; }

        public string GvaAircraftProducer { get; set; }

        public string GvaAircraftICAO { get; set; }

        public int? GvaAirportId { get; set; }

        public string GvaAirportType { get; set; }

        public string GvaAirportName { get; set; }

        public int? GvaEquipmentId { get; set; }

        public string GvaEquipmentName { get; set; }

        public string GvaEquipmentType { get; set; }

        public string GvaEquipmentProducer { get; set; }

        public string Description
        {
            get
            {
                if (this.PersonId.HasValue)
                {
                    return string.Format("{0} - {1}", this.PersonLin, this.PersonNames);
                }
                else if (this.GvaOrganizationId.HasValue)
                {
                    return string.Format("{0} - {1}", this.GvaOrganizationUin, this.GvaOrganizationName);
                }
                else if (this.GvaAircraftId.HasValue)
                {
                    return string.Format("{0} - {1} - {2}", this.GvaAircraftProducer, this.GvaAirCategory, this.GvaAircraftICAO);
                }
                else if (this.GvaAirportId.HasValue)
                {
                    return string.Format("{0} - {1}", this.GvaAirportType, this.GvaAirportName);
                }
                else if (this.GvaEquipmentId.HasValue)
                {
                    return string.Format("{0} - {1} - {2}", this.GvaEquipmentName, this.GvaEquipmentType, this.GvaEquipmentProducer);
                }

                return "";
            }
        }
    }
}
