using Gva.Api.Models;
using System;

namespace Gva.Api.ModelsDO
{
    public class ApplicationListDO
    {
        public int ApplicationId { get; set; }
        public int? DocId { get; set; }

        public int? AppPartId { get; set; }
        public int? AppPartIndex { get; set; }
        public DateTime? AppPartRequestDate { get; set; }
        public string AppPartDocumentNumber { get; set; }
        public string AppPartApplicationTypeName { get; set; }
        public string AppPartStatusName { get; set; }

        public int? PersonId { get; set; }
        public string PersonLin { get; set; }
        public string PersonNames { get; set; }

        public int? GvaOrganizationId { get; set; }
        public string GvaOrganizationName { get; set; }
        public string GvaOrganizationUin { get; set; }

        public int? GvaAircraftId { get; set; }
        public string GvaAircraftCategory { get; set; }
        public string GvaAircraftProducer { get; set; }
        public string GvaAircraftICAO { get; set; }

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
                    return string.Format("{0} - {1} - {2}", this.GvaAircraftProducer, this.GvaAircraftCategory, this.GvaAircraftICAO);
                }

                return "";
            }
        }
    }
}
