using System;
using System.Collections.Generic;
using Common.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationRecommendationDO
    {
        public OrganizationRecommendationDO()
        {
            this.Part1Inspectors = new List<NomValue>();
            this.Part2Inspectors = new List<NomValue>();
            this.Part3Inspectors = new List<NomValue>();
            this.Part4Inspectors = new List<NomValue>();
            this.Part5Inspectors = new List<NomValue>();

            this.Inspections = new List<int>();
            this.RecommendationDetails = new List<OrganizationRecommendationSectionDO>();
            this.Disparities = new List<DisparityDO>();
        }

        public NomValue AuditPart { get; set; }

        public DateTime? FormDate { get; set; }

        public string FormText { get; set; }

        public string InterviewedStaff { get; set; }

        public DateTime? InspectionFromDate { get; set; }

        public DateTime? InspectionToDate { get; set; }

        public DateTime? Finished1Date { get; set; }

        public string Town1 { get; set; }

        public DateTime? Finished2Date { get; set; }

        public string Town2 { get; set; }

        public DateTime? Finished3Date { get; set; }

        public string Town3 { get; set; }

        public DateTime? Finished4Date { get; set; }

        public string Town4 { get; set; }

        public DateTime? Finished5Date { get; set; }

        public string Town5 { get; set; }

        public string DocumentDescription { get; set; }

        public string Recommendation { get; set; }

        public List<NomValue> Part1Inspectors { get; set; }

        public List<NomValue> Part2Inspectors { get; set; }

        public List<NomValue> Part3Inspectors { get; set; }

        public List<NomValue> Part4Inspectors { get; set; }

        public List<NomValue> Part5Inspectors { get; set; }

        public List<int> Inspections { get; set; }

        public List<OrganizationRecommendationSectionDO> RecommendationDetails { get; set; }

        public List<DisparityDO> Disparities { get; set; }
    }
}
