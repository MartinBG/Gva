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
using Aop;

namespace Rio.Data.Extractions.ApplicationDataDo.Aop
{
    public class AopApplicationDataDoExtraction : ServiceHeaderAndFooterApplicationDataDoExtraction<AopApplication>
    {
        protected override string GetDocFileTypeAlias(AopApplication rioObject)
        {
            return "R-0001";
        }

        protected override ElectronicAdministrativeServiceHeader GetElectronicAdministrativeServiceHeader(AopApplication rioObject)
        {
            return rioObject.ElectronicAdministrativeServiceHeader;
        }

        protected override ElectronicAdministrativeServiceFooter GetElectronicAdministrativeServiceFooter(AopApplication rioObject)
        {
            return rioObject.ElectronicAdministrativeServiceFooter;
        }
    }
}
