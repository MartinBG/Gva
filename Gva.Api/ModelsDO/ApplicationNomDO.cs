using System;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO
{
    public class ApplicationNomDO
    {
        public ApplicationNomDO()
        {
        }

        public ApplicationNomDO(GvaApplication application)
        {
            DocumentApplicationDO applicationContent = application.GvaAppLotPart.Lot.Index.GetPart<DocumentApplicationDO>(application.GvaAppLotPart.Path).Content;
            this.ApplicationId = application.GvaApplicationId;
            this.PartIndex = application.GvaAppLotPart.Index;
            this.ApplicationName = applicationContent.ApplicationType.Name;
            this.DocumentDate = applicationContent.DocumentDate;
            this.DocumentNumber = applicationContent.DocumentNumber;
            this.ApplicationTypeCode = applicationContent.ApplicationType.Code;
        }

        public int ApplicationId { get; set; }

        public int? PartIndex { get; set; }

        public string ApplicationName { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string DocumentNumber { get; set; }

        public string ApplicationTypeCode { get; set; }
    }
}
