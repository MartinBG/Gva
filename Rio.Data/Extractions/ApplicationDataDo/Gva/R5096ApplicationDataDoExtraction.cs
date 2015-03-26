﻿using Rio.Data.RioObjectExtractor;
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
    public class R5096ApplicationDataDoExtraction : ServiceHeaderAndFooterApplicationDataDoExtraction<R_5096.ApprovalTechnicalLogApplication>
    {
        protected override ElectronicAdministrativeServiceHeader GetElectronicAdministrativeServiceHeader(R_5096.ApprovalTechnicalLogApplication rioObject)
        {
            return rioObject.ElectronicAdministrativeServiceHeader;
        }

        protected override ElectronicAdministrativeServiceFooter GetElectronicAdministrativeServiceFooter(R_5096.ApprovalTechnicalLogApplication rioObject)
        {
            return rioObject.ElectronicAdministrativeServiceFooter;
        }
    }
}
