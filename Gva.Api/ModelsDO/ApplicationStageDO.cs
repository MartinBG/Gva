using System;
using Gva.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class ApplicationStageDO
    {
        public ApplicationStageDO(GvaApplicationStage appStage)
        {
            this.Id = appStage.GvaAppStageId;
            this.StageId = appStage.GvaStage.GvaStageId;
            this.StageName = appStage.GvaStage.Name;
            this.Date = appStage.StartingDate;
            this.InspectorId = appStage.Inspector != null ? appStage.Inspector.LotId : (int?)null;
            this.InspectorName = appStage.Inspector != null ? appStage.Inspector.Person.Names : null;
            this.Ordinal = appStage.Ordinal;
        }

        public int Id { get; set; }

        public int StageId { get; set; }

        public string StageName { get; set; }

        public DateTime Date { get; set; }

        public int? InspectorId { get; set; }

        public string InspectorName { get; set; }

        public int Ordinal { get; set; }
    }
}