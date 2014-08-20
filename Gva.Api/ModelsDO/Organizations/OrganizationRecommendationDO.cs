using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationRecommendationDO
    {
        public OrganizationRecommendationDO(){
            this.Part1 = new OrganizationPartDO();
            this.Part2 = new OrganizationPartDO();
            this.Part3 = new OrganizationPartDO();
            this.Part4 = new OrganizationPartDO();
            this.Part5 = new OrganizationPartDO();
            this.DescriptionReview = new OrganizationDescriptionReviewDO();
            this.IncludedAudits = new List<int>();
        }

        public NomValue RecommendationPart { get; set; }

        public DateTime? FormDate { get; set; }

        public string FormText { get; set; }

        public string InterviewedStaff { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

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

        public OrganizationPartDO Part1 { get; set; }

        public OrganizationPartDO Part2 { get; set; }

        public OrganizationPartDO Part3 { get; set; }

        public OrganizationPartDO Part4 { get; set; }

        public OrganizationPartDO Part5 { get; set; }

        public OrganizationDescriptionReviewDO DescriptionReview { get; set; }

        public List<int> IncludedAudits { get; set; }
    }
}
