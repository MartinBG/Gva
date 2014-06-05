using Common.Rio.RioObjectExtractor;
using RioObjects;
using Gva.RioBridge.DataObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gva.RioBridge.Extractions.AttachedDocDo
{
    public class R4470AttachedDocDoExtraction : R3994CollectionAttachedDocDoExtraction<R_4470.AircraftAirworthinessCertificateApplication>
    {
        protected override R_3994.AttachedDocumentDatasCollection GetR3994Collection1(R_4470.AircraftAirworthinessCertificateApplication rioObject)
        {
            return rioObject.AttachedDocumentDatasCollection;
        }
    }
}
