using System;
using Common.Json;
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
            this.ApplicationId = application.GvaApplicationId;
            this.PartIndex = application.GvaAppLotPart.Index;
            this.ApplicationName = application.GvaAppLotPart.Lot.Index.GetPart<DocumentApplicationDO>(application.GvaAppLotPart.Path).Content.ApplicationType.Name;
        }

        public int ApplicationId { get; set; }

        public int? PartIndex { get; set; }

        public string ApplicationName { get; set; }
    }
}
