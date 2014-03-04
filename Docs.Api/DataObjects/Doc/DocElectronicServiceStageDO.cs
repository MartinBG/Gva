using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocElectronicServiceStageDO
    {
        public DocElectronicServiceStageDO()
        {
        }

        public DocElectronicServiceStageDO(DocElectronicServiceStage d)
        {
            if (d != null)
            {
                this.DocElectronicServiceStageId = d.DocElectronicServiceStageId;
                this.DocId = d.DocId;
                this.ElectronicServiceStageId = d.ElectronicServiceStageId;
                this.StartingDate = d.StartingDate;
                this.ExpectedEndingDate = d.ExpectedEndingDate;
                this.EndingDate = d.EndingDate;
                this.IsCurrentStage = d.IsCurrentStage;
                this.Version = d.Version;

                if (d.ElectronicServiceStage != null)
                {
                    this.ElectronicServiceStageAlias = d.ElectronicServiceStage.Alias;
                    this.ElectronicServiceStageName = d.ElectronicServiceStage.Name;
                    this.ElectronicServiceStageDuration = d.ElectronicServiceStage.Duration ?? 0;
                    this.ElectronicServiceStageDocTypeId = d.ElectronicServiceStage.DocTypeId;

                    if (d.ElectronicServiceStage.ElectronicServiceStageExecutors != null && d.ElectronicServiceStage.ElectronicServiceStageExecutors.Any(e => e.IsActive))
                    {
                        StringBuilder sb = new StringBuilder();
                        var executors = d.ElectronicServiceStage.ElectronicServiceStageExecutors.Where(e => e.IsActive).ToList();
                        foreach (var executor in executors)
                        {

                            if (executor.Unit != null)
                            {
                                if (sb.Length > 0)
                                {
                                    sb.Append("<br/>");
                                }
                                sb.Append(executor.Unit.Name);
                            }
                        }

                        this.ElectronicServiceStageExecutors = sb.ToString();
                    }
                }
            }
        }

        public Nullable<int> DocElectronicServiceStageId { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<int> ElectronicServiceStageId { get; set; }
        public Nullable<System.DateTime> StartingDate { get; set; }
        public Nullable<System.DateTime> ExpectedEndingDate { get; set; }
        public Nullable<System.DateTime> EndingDate { get; set; }
        public int DocTypeId { get; set; }
        public bool IsCurrentStage { get; set; }
        public byte[] Version { get; set; }

        //
        public string ElectronicServiceStageAlias { get; set; }
        public string ElectronicServiceStageName { get; set; }
        public int ElectronicServiceStageDuration { get; set; }
        public int ElectronicServiceStageDocTypeId { get; set; }

        public string ElectronicServiceStageExecutors { get; set; }
    }
}
