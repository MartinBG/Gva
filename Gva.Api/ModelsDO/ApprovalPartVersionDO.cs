using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using System;
using Common.Json;

namespace Gva.Api.ModelsDO
{
    public class ApprovalPartVersionDO
    {
        public ApprovalPartVersionDO(PartVersion partVersion, PartVersion firstAmendment, PartVersion lastAmendment)
        {
            this.PartIndex = partVersion.Part.Index.Value;
            this.Approval = partVersion.Content;
            this.Amendment = new PartVersionDO(partVersion, null);

            this.DocumentFirstDateIssue = firstAmendment.Content.Get<DateTime>("documentDateIssue");
        }

        public int PartIndex { get; set; }

        public JObject Approval { get; set; }

        public PartVersionDO Amendment { get; set; }

        public DateTime DocumentFirstDateIssue { get; set; }

    }
}