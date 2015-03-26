using Rio.Data.RioObjectExtractor;
using Rio.Objects;
using Rio.Data.DataObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_0009_000152;
using R_0009_000153;

namespace Rio.Data.Extractions.ApplicationDataDo.Gva
{
    public class R5170ApplicationDataDoExtraction : ServiceHeaderAndFooterApplicationDataDoExtraction<R_5170.ConfirmationRecoveryRatingLicenseEngineeringTechnicalStaffMaintenanceFundsManagementApplication>
    {
        protected override ElectronicAdministrativeServiceHeader GetElectronicAdministrativeServiceHeader(R_5170.ConfirmationRecoveryRatingLicenseEngineeringTechnicalStaffMaintenanceFundsManagementApplication rioObject)
        {
            return rioObject.ElectronicAdministrativeServiceHeader;
        }

        protected override ElectronicAdministrativeServiceFooter GetElectronicAdministrativeServiceFooter(R_5170.ConfirmationRecoveryRatingLicenseEngineeringTechnicalStaffMaintenanceFundsManagementApplication rioObject)
        {
            return rioObject.ElectronicAdministrativeServiceFooter;
        }
    }
}
