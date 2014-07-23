using Gva.Api.Models;
using System;

namespace Gva.Api.ModelsDO
{
    public class ApplicationStageDO
    {
        public ApplicationStageDO(GvaApplicationStage appStage)
        {
            this.Id = appStage.GvaAppStageId;
            this.Stage = appStage.GvaStage.Name;
            this.Date = appStage.StartingDate;
            this.Inspector = appStage.Inspector != null ? appStage.Inspector.Person.Names : null;
            this.Ordinal = appStage.Ordinal;
        }

        public int Id { get; set; }

        public string Stage { get; set; }

        public DateTime Date { get; set; }

        public string Inspector { get; set; }

        public int Ordinal { get; set; }
    }
}