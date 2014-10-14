namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationApprovalNewDO
    {
        public CaseTypePartDO<OrganizationAmendmentDO> Amendment { get; set; }

        public CaseTypePartDO<OrganizationApprovalDO> Approval { get; set; }

    }
}
