using Aop.Api.Models;
using Docs.Api.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aop.Api.DataObjects
{
    public class AppListItemDO
    {
        public AppListItemDO()
        {
            this.STDocRelation = new DocRelationDO();
            this.NDDocRelation = new DocRelationDO();
        }

        public AppListItemDO(AopApp a)
            : this()
        {
            if (a != null)
            {
                this.AopApplicationId = a.AopApplicationId;
                this.CreateUnitId = a.CreateUnitId;
                this.AopEmployerId = a.AopEmployerId;
                this.Email = a.Email;
                this.STDocId = a.STDocId;
                this.NDDocId = a.NDDocId;
                this.Version = a.Version;

                if (a.CreateUnit != null)
                {
                    this.CreateUnitName = a.CreateUnit.Name;
                }

                if (a.AopEmployer != null)
                {
                    this.AopEmployerName = string.Format("{0} ({1})", a.AopEmployer.Name, a.AopEmployer.LotNum);
                }
            }
        }

        public Nullable<int> AopApplicationId { get; set; }
        public Nullable<int> CreateUnitId { get; set; }
        public Nullable<int> AopEmployerId { get; set; }
        public int? STDocId { get; set; }
        public int? NDDocId { get; set; }
        public string Email { get; set; }
        public byte[] Version { get; set; }

        //
        public string AopEmployerName { get; set; }
        public string CreateUnitName { get; set; }
        public DocRelationDO STDocRelation { get; set; }
        public DocRelationDO NDDocRelation { get; set; }
    }
}
