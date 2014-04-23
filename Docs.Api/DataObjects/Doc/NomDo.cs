using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class NomDo
    {
        public NomDo()
        {
        }

        public NomDo(DocUnit d)
            : this()
        {
            if (d != null && d.Unit != null)
            {
                this.Alias = d.Unit.Name;
                this.IsActive = d.Unit.IsActive;
                this.Name = String.Format("{0}{1}", d.Unit.Name, d.Unit.UnitRelations.Any(f => f.ParentUnitId.HasValue) ? " (" + d.Unit.UnitRelations.First().ParentUnit.Name + ")" : String.Empty);
                this.NomValueId = d.Unit.UnitId;
            }
        }

        public NomDo(DocCorrespondent d)
            : this()
        {
            if (d != null && d.Correspondent != null)
            {
                this.Alias = d.Correspondent.Alias;
                this.IsActive = d.Correspondent.IsActive;
                this.Name = d.Correspondent.DisplayName;
                this.NomValueId = d.Correspondent.CorrespondentId;
            }
        }

        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public int NomValueId { get; set; }

        public bool IsProcessed { get; set; }
        public int ForeignKeyId { get; set; }
    }
}
