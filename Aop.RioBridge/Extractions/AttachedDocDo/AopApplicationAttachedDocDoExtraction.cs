using Common.Rio.RioObjectExtractor;
using RioObjects;
using Aop.RioBridge.DataObjects;
using Abbcdn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aop.RioBridge.Extractions.AttachedDocDo;

namespace Aop.RioBridge.Extractions.AttachedDocDo
{
    public class AopApplicationAttachedDocDoExtraction : AttachedDocDoExtraction<Aop.AopApplication>
    {
        protected override AopAttachedDocuments.AopAttachedDocumentDatasCollection GetAopAttachedDocumentsCollection(Aop.AopApplication rioObject)
        {
            return rioObject.AopAttachedDocumentDatasCollection;
        }
    }
}
