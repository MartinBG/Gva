using System;
using Common.Json;
using Gva.Api.Models;

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
            this.ApplicationName = application.GvaAppLotPart.Lot.Index.GetPart(application.GvaAppLotPart.Path).Content.Get<string>("applicationType.name");
        }

        public int ApplicationId { get; set; }

        public int? PartIndex { get; set; }

        public string ApplicationName { get; set; }
    }
}
