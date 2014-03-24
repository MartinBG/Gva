﻿using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocWorkflowDO
    {
        public DocWorkflowDO()
        {
        }

        public DocWorkflowDO(DocWorkflow d)
        {
            if (d != null)
            {
                this.DocWorkflowId = d.DocWorkflowId;
                this.DocId = d.DocId;
                this.DocWorkflowActionId = d.DocWorkflowActionId;
                this.EventDate = d.EventDate;
                this.YesNo = d.YesNo.HasValue ? (d.YesNo.Value ? "true" : "false") : null;
                this.UnitUserId = d.UnitUserId;
                this.ToUnitId = d.ToUnitId;
                this.PrincipalUnitId = d.PrincipalUnitId;
                this.Note = d.Note;
                this.Version = d.Version;

                if (d.DocWorkflowAction != null)
                {
                    this.DocWorkflowActionName = d.DocWorkflowAction.Name;
                    this.DocWorkflowActionAlias = d.DocWorkflowAction.Alias;
                }

                this.ToUnitName = d.Unit != null ? d.Unit.Name : string.Empty;
                this.PrincipalUnitName = d.Unit1 != null ? d.Unit1.Name : string.Empty;

                this.Username = d.UnitUser != null && d.UnitUser.User != null ? d.UnitUser.User.Fullname : string.Empty;
                this.UserUnitId = d.UnitUser != null && d.UnitUser.Unit != null ? d.UnitUser.Unit.UnitId : (int?)null;
                this.UserUnitName = d.UnitUser != null && d.UnitUser.Unit != null ? d.UnitUser.Unit.Name : string.Empty;
            }
        }

        public Nullable<int> DocWorkflowId { get; set; }
        public Nullable<int> DocId { get; set; }
        public int DocWorkflowActionId { get; set; }
        public System.DateTime EventDate { get; set; }
        public string YesNo { get; set; }
        public int UnitUserId { get; set; }
        public Nullable<int> ToUnitId { get; set; }
        public Nullable<int> PrincipalUnitId { get; set; }
        public string Note { get; set; }
        public byte[] Version { get; set; }

        //
        public string DocWorkflowActionName { get; set; }
        public string DocWorkflowActionAlias { get; set; }

        public string ToUnitName { get; set; }
        public string PrincipalUnitName { get; set; }

        public string Username { get; set; }
        public Nullable<int> UserUnitId { get; set; }
        public string UserUnitName { get; set; }

        public DocUnitDO CurrentDocUnit { get; set; }

        public bool IsRequest
        {
            get
            {
                return DocWorkflowActionAlias.ToLower().EndsWith("request");
            }
        }

        public string ActionText
        {
            get
            {
                switch (DocWorkflowActionAlias.ToLower())
                {
                    case "sign":
                        return "Подписвам";
                    case "discuss":
                        return "Съгласувам";
                    case "approval":
                        return "Одобрявам";
                    case "signrequest":
                        return "За подписване";
                    case "discussrequest":
                        return "За съгласуване";
                    case "approvalrequest":
                        return "За одобрение";
                    default:
                        return "";
                }
            }
        }

        public string DialogTitle
        {
            get
            {
                switch (DocWorkflowActionAlias.ToLower())
                {
                    case "sign":
                        return "Подписване";
                    case "discuss":
                        return "Съгласуване";
                    case "approval":
                        return "Одобряване";
                    case "signrequest":
                        return "Искане за подпис";
                    case "discussrequest":
                        return "Искане за съгласуване";
                    case "approvalrequest":
                        return "Искане за одобрение";
                    default:
                        return "";
                }
            }
        }

        //
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }
    }
}