﻿using Common.Rio.RioObjectExtractor;
using Aop.Portal.RioObjects;
using Aop.RioBridge.DataObjects;
using Abbcdn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aop;
using Aop.RioBridge.Extractions.AttachedDocDo;

namespace Aop.RioBridge.Extractions.AttachedDocDo
{
    public class AopApplicationAttachedDocDoExtraction : AttachedDocDoExtraction<AopApplication>
    {
        protected override AopAttachedDocuments.AopAttachedDocumentDatasCollection GetAttachedDocumentsCollection(AopApplication rioObject)
        {
            return rioObject.AopAttachedDocumentDatasCollection;
        }
    }
}