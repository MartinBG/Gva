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
    public class R5178ApplicationDataDoExtraction : ServiceHeaderAndFooterApplicationDataDoExtraction<R_5178.RegistrationRatingTypeClassAircraftIFRPilotLicensePilotPartFCLApplication>
    {
        protected override ElectronicAdministrativeServiceHeader GetElectronicAdministrativeServiceHeader(R_5178.RegistrationRatingTypeClassAircraftIFRPilotLicensePilotPartFCLApplication rioObject)
        {
            return rioObject.ElectronicAdministrativeServiceHeader;
        }

        protected override ElectronicAdministrativeServiceFooter GetElectronicAdministrativeServiceFooter(R_5178.RegistrationRatingTypeClassAircraftIFRPilotLicensePilotPartFCLApplication rioObject)
        {
            return rioObject.ElectronicAdministrativeServiceFooter;
        }
    }
}
