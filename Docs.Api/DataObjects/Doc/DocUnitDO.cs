using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocUnitDO
    {
        public DocUnitDO()
        {
            this.Children = new List<DocUnitDO>();

            this.IsSelectable = true;
            this.IsSelected = false;
            this.IsNew = false;
            this.IsDeleted = false;
            this.IsExpanded = false;
        }

        public DocUnitDO(DocUnit d)
            : this()
        {
            if (d != null)
            {
                this.DocUnitId = d.DocUnitId;
                this.DocId = d.DocId;
                this.UnitId = d.UnitId;
                this.DocUnitRoleId = d.DocUnitRoleId;
                this.Version = d.Version;

                if (d.Unit != null)
                {
                    this.UnitName = d.Unit.Name;
                }

                if (d.DocUnitRole != null)
                {
                    this.DocUnitRoleAlias = d.DocUnitRole.Alias.ToLower();
                }
            }
        }

        public Nullable<int> DocUnitId { get; set; }
        public Nullable<int> DocId { get; set; }
        public int UnitId { get; set; }
        public int DocUnitRoleId { get; set; }
        public byte[] Version { get; set; }

        //
        public string UnitName { get; set; }
        public string DocUnitRoleAlias { get; set; }
        public List<DocUnitDO> Children { get; set; }
        public bool IsSelected { get; set; }
        public bool IsSelectable { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsExpanded { get; set; }
        public bool CanBeExpanded { get; set; }
        public int Level { get; set; }
        public string LevelCss { get; set; }

        public string UnitTypeAlias { get; set; }
    }
}
