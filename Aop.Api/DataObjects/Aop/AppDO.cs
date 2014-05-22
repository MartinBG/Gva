using Aop.Api.Models;
using Docs.Api.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aop.Api.DataObjects
{
    public class AppDO
    {
        public AppDO()
        {
        }

        public AppDO(AopApp a)
            : this()
        {
            if (a != null)
            {
                this.AopApplicationId = a.AopApplicationId;
                this.DocId = a.DocId;
                this.AopEmployerId = a.AopEmployerId;
                this.Email = a.Email;
                this.Version = a.Version;

                //I
                this.STAopApplicationTypeId = a.STAopApplicationTypeId;
                this.STObjectId = a.STObjectId;
                this.STSubject = a.STSubject;
                this.STCriteriaId = a.STCriteriaId;
                this.STValue = a.STValue;
                this.STRemark = a.STRemark;
                this.STIsMilitary = a.STIsMilitary;
                this.STNoteTypeId = a.STNoteTypeId;

                this.STDocId = a.STDocId;
                this.STChecklistId = a.STChecklistId;
                this.STChecklistStatusId = a.STChecklistStatusId;
                this.STNoteId = a.STNoteId;

                //II
                this.NDAopApplicationTypeId = a.NDAopApplicationTypeId;
                this.NDObjectId = a.NDObjectId;
                this.NDSubject = a.NDSubject;
                this.NDCriteriaId = a.NDCriteriaId;
                this.NDValue = a.NDValue;
                this.NDIsMilitary = a.NDIsMilitary;
                this.NDROPIdNum = a.NDROPIdNum;
                this.NDROPUnqNum = a.NDROPUnqNum;
                this.NDROPDate = a.NDROPDate;
                this.NDProcedureStatusId = a.NDProcedureStatusId;
                this.NDRefusalReason = a.NDRefusalReason;
                this.NDAppeal = a.NDAppeal;
                this.NDRemark = a.NDRemark;

                this.NDDocId = a.NDDocId;
                this.NDChecklistId = a.NDChecklistId;
                this.NDChecklistStatusId = a.NDChecklistStatusId;
                this.NDReportId = a.NDReportId;

                if (a.AopEmployer != null)
                {
                    this.AopEmployerName = string.Format("{0} ({1})", a.AopEmployer.Name, a.AopEmployer.LotNum);
                }
            }
        }

        public Nullable<int> AopApplicationId { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<int> AopEmployerId { get; set; }
        public string Email { get; set; }
        //I
        public Nullable<int> STAopApplicationTypeId { get; set; }
        public Nullable<int> STObjectId { get; set; }
        public string STSubject { get; set; }
        public Nullable<int> STCriteriaId { get; set; }
        public string STValue { get; set; }
        public string STRemark { get; set; }
        public Nullable<bool> STIsMilitary { get; set; }
        public Nullable<int> STNoteTypeId { get; set; }

        public Nullable<int> STDocId { get; set; }
        public Nullable<int> STChecklistId { get; set; }
        public Nullable<int> STChecklistStatusId { get; set; }
        public Nullable<int> STNoteId { get; set; }

        //II
        public Nullable<int> NDAopApplicationTypeId { get; set; }
        public Nullable<int> NDObjectId { get; set; }
        public string NDSubject { get; set; }
        public Nullable<int> NDCriteriaId { get; set; }
        public string NDValue { get; set; }
        public Nullable<bool> NDIsMilitary { get; set; }
        public string NDROPIdNum { get; set; }
        public string NDROPUnqNum { get; set; }
        public Nullable<System.DateTime> NDROPDate { get; set; }
        public Nullable<int> NDProcedureStatusId { get; set; }
        public string NDRefusalReason { get; set; }
        public string NDAppeal { get; set; }
        public string NDRemark { get; set; }

        public Nullable<int> NDDocId { get; set; }
        public Nullable<int> NDChecklistId { get; set; }
        public Nullable<int> NDChecklistStatusId { get; set; }
        public Nullable<int> NDReportId { get; set; }

        public byte[] Version { get; set; }

        //
        public string AopEmployerName { get; set; }

        //
        public DocRelationDO STDocRelation { get; set; }
        public DocRelationDO STChecklistRelation { get; set; }
        public DocRelationDO STNoteRelation { get; set; }
        public DocRelationDO NDDocRelation { get; set; }
        public DocRelationDO NDChecklistRelation { get; set; }
        public DocRelationDO NDReportRelation { get; set; }
    }
}
