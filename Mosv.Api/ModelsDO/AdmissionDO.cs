﻿using Mosv.Api.Models;
using System;

namespace Mosv.Api.ModelsDO
{
    public class AdmissionDO
    {
        public AdmissionDO()
        {
        }

        public AdmissionDO(MosvViewAdmission admission)
        {
            this.Id = admission.LotId;
            this.ApplicationDocId = admission.ApplicationDocId;
            this.IncomingLot = admission.IncomingLot;
            this.IncomingNumber = admission.IncomingNumber;
            this.IncomingDate = admission.IncomingDate;
            this.ApplicantType = admission.ApplicantType;
            this.Applicant = admission.Applicant;
        }

        public int Id { get; set; }

        public int? ApplicationDocId { get; set; }

        public string IncomingNumber { get; set; }

        public string IncomingLot { get; set; }

        public string Applicant { get; set; }

        public DateTime? IncomingDate { get; set; }

        public string ApplicantType { get; set; }

        public Docs.Api.DataObjects.DocRelationDO ApplicationDocRelation { get; set; }
    }
}