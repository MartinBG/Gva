using Common.Api.UserContext;
using Docs.Api.Enums;
using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Repositories;
using System.Linq.Expressions;
using Common.Api.Models;
using Docs.Api.DataObjects;
using System.Net.Http;

namespace Docs.Api.Repositories.EmailRepository
{
    public interface IEmailRepository : IRepository<Email>
    {
        Email CreateEmail(int emailTypeId, int emailStatusId, string subject, string body);

        Email CreateResolutionEmail(Doc doc, HttpRequestMessage request);

        Email CreateDocumentEmail(Doc doc, HttpRequestMessage request);

        Email CreateDocWorkflowEmail(Doc doc, DocWorkflow docWorkflow, bool isRequest, HttpRequestMessage request);

        Email CreateElectronicServiceStageChangeEmail(Doc caseDoc, DocElectronicServiceStage docElectronicServiceStage, HttpRequestMessage request);

        Email CreateCorrespondentEmail(CorrespondentEmailDO data);

        EmailStatus GetEmailStatusByAlias(string alias);

        EmailAddresseeType GetEmailAddresseeTypeByAlias(string alias);

        EmailType GetEmailTypeByAlias(string alias);
    }
}
