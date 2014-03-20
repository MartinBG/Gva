using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    //public class DocInventoryDO
    //{
    //    public DocInventoryDO()
    //    {
    //        this.DocInventoryItems = new List<DocInventoryItemDO>();
    //    }

    //    public DocInventoryDO(DocInventory d)
    //        : this()
    //    {
    //        if (d != null)
    //        {
    //            this.DocInventoryId = d.DocInventoryId;
    //            this.RegUri = d.RegUri;
    //            this.RegDate = d.RegDate;
    //            this.UnitId = d.UnitId;
    //            this.ModifyDate = d.ModifyDate;
    //            this.ModifyUserId = d.ModifyUserId;
    //            this.IsActive = d.IsActive;
    //            this.Version = d.Version;

    //            if (d.Unit != null)
    //            {
    //                this.UnitName = d.Unit.Name;
    //            }
    //        }
    //    }

    //    public int DocInventoryId { get; set; }
    //    public string RegUri { get; set; }
    //    public Nullable<DateTime> RegDate { get; set; }
    //    public int UnitId { get; set; }
    //    public Nullable<System.DateTime> ModifyDate { get; set; }
    //    public Nullable<int> ModifyUserId { get; set; }
    //    public bool IsActive { get; set; }
    //    public byte[] Version { get; set; }

    //    public List<DocInventoryItemDO> DocInventoryItems { get; set; }

    //    public string UnitName { get; set; }
    //    public string ErrorString { get; set; }

    //    public string DocInventoryColor
    //    {
    //        get
    //        {
    //            return IsActive ? "black" : "red";
    //        }
    //    }

    //    //команда редакция
    //    public bool IsVisibleDocInventoryEdit { get; set; }
    //}
}
