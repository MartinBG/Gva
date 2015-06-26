using System;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO
{
    public class ApplicationNomDO
    {
        public ApplicationNomDO()
        {
        }

        public ApplicationNomDO(GvaApplication application)
        {
            this.ApplicationId = application.GvaApplicationId;

            if (application.GvaAppLotPartId != null)
            {
                var appContent = application.GvaViewApplication;

                if (!string.IsNullOrEmpty(appContent.OldDocumentNumber))
                {
                    this.ApplicationName = string.Format("{0} {1}/{2:dd.MM.yyyy}", appContent.ApplicationType.Code, appContent.OldDocumentNumber, appContent.DocumentDate);
                    this.ApplicationDate = appContent.DocumentDate;
                }
                else
                {
                    this.ApplicationName = string.Format("{0} {1}", appContent.ApplicationType.Code, application.Doc.RegUri);
                    this.ApplicationDate = application.Doc.RegDate;
                }
            }
            else
            {
                this.ApplicationName = application.Doc.RegUri;
                this.ApplicationDate = application.Doc.RegDate;
            }
        }

        public int ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public DateTime? ApplicationDate { get; set; }
    }
}
