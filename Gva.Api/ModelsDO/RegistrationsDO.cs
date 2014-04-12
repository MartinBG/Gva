using System.Collections.Generic;
using System.Linq;
using Gva.Api.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using Common.Json;
using System;

namespace Gva.Api.ModelsDO
{
    public class RegistrationsDO
    {
        public RegistrationsDO(IEnumerable<PartVersion> registrations, IEnumerable<PartVersion> airworthinesses)
        {
            var regs = registrations.OrderBy(r => r.CreateDate).ToArray();
            var regIndex = regs.Length;
            
            this.CurrentIndex = regs.Last().Part.Index.Value;

            this.AirworthinessIndex = airworthinesses
                .Where(aw =>
                    {
                        JObject content = JObject.Parse(aw.TextContent);
                        bool isMatch = true;

                        isMatch &= content.Get<int>("registration.nomValueId") <= CurrentIndex;

                        return isMatch;
                    }
                    )
                .OrderBy(aw => aw.CreateDate)
                .Last().Part.Index;

            this.FirstIndex = regs[0].Part.Index.Value;
            this.PrevIndex = regIndex > 0 ? regs[regIndex - 1].Part.Index : null;
            this.NextIndex = regIndex < regs.Length - 1 ? regs[regIndex + 1].Part.Index : null;
            this.FirstIndex = regs[regs.Length - 1].Part.Index.Value;
            this.FirstReg = regs[0].Content;
            this.LastReg = regs[regs.Length - 1].Content;
        }

        public int? FirstIndex { get; set; }

        public int? PrevIndex { get; set; }

        public int? CurrentIndex { get; set; }

        public int? NextIndex { get; set; }

        public int? LastIndex { get; set; }

        public int? AirworthinessIndex { get; set; }

        public JObject FirstReg { get; set; }

        public JObject LastReg { get; set; }
    }
}
