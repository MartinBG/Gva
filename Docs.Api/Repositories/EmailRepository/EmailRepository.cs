using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Docs.Api.Models;
using Common.Api.UserContext;
using Common.Extensions;
using Common.Linq;
using Common.Api.Models;
using Docs.Api.Enums;
using System.Data.SqlClient;
using Common.Api.Repositories;
using Common.Utils;
using System.Linq.Expressions;
using System.Data.Entity.Core;
using Docs.Api.DataObjects;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Configuration;

namespace Docs.Api.Repositories.EmailRepository
{
    public class EmailRepository : Repository<Email>, IEmailRepository
    {
        public static readonly string CaseViewUrl = ConfigurationManager.AppSettings["Docs.Api:PortalWebAddress"] + "bg/Information/Information/CaseSearch";
        public static readonly string DocViewUrl = ConfigurationManager.AppSettings["Docs.Api:PortalWebAddress"] + "bg/App/Upload/Upload";

        public EmailRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public string emailRegex = @"^[\w\-!#$%&'*+/=?^`{|}~.""]+@([\w]+[.-]?)+[\w]\.[\w]+$";

        public Email CreateEmail(int emailTypeId, int emailStatusId, string subject, string body)
        {
            Email email = new Email();

            email.EmailTypeId = emailTypeId;
            email.EmailStatusId = emailStatusId;
            email.Subject = subject;
            email.Body = body;

            return email;
        }

        public Email CreateResolutionEmail(Doc doc, HttpRequestMessage request)
        {
            List<DocUnitRole> docUnitRoles = this.unitOfWork.DbContext.Set<DocUnitRole>().Where(e => e.IsActive).ToList();

            DocUnitRole docUnitRoleFrom = docUnitRoles.SingleOrDefault(e => e.Alias == "From");
            DocUnitRole docUnitRoleImportedBy = docUnitRoles.SingleOrDefault(e => e.Alias == "ImportedBy");
            DocUnitRole docUnitRoleInCharge = docUnitRoles.SingleOrDefault(e => e.Alias == "InCharge");
            DocUnitRole docUnitRoleControlling = docUnitRoles.SingleOrDefault(e => e.Alias == "Controlling");

            List<DocUnit> receivingDocUnits = doc.DocUnits.Where(du => du.DocUnitRoleId == docUnitRoleInCharge.DocUnitRoleId || du.DocUnitRoleId == docUnitRoleControlling.DocUnitRoleId).ToList();
            DocUnit docUnitFrom = doc.DocUnits.FirstOrDefault(e => e.DocUnitRoleId == docUnitRoleFrom.DocUnitRoleId);

            if (docUnitFrom == null)
            {
                docUnitFrom = doc.DocUnits.FirstOrDefault(e => e.DocUnitRoleId == docUnitRoleImportedBy.DocUnitRoleId);
            }

            DocRelation docRelation = doc.DocRelations.FirstOrDefault();

            EmailStatus pendingStatus = this.GetEmailStatusByAlias("Pending");

            EmailAddresseeType to = this.GetEmailAddresseeTypeByAlias("To");

            Email email = new Email();

            email.EmailStatusId = pendingStatus.EmailStatusId;

            foreach (var item in receivingDocUnits)
            {
                List<UnitUser> receivingUnitUsers = this.unitOfWork.DbContext.Set<UnitUser>()
                    .Include(e => e.User)
                    .Where(e => e.UnitId == item.UnitId)
                    .ToList();

                foreach (var ruu in receivingUnitUsers)
                {
                    if (!string.IsNullOrEmpty(ruu.User.Email) && Regex.IsMatch(ruu.User.Email, this.emailRegex))
                    {
                        email.EmailAddressees.Add(new EmailAddressee()
                        {
                            EmailAddresseeTypeId = to.EmailAddresseeTypeId,
                            Address = ruu.User.Email
                        });
                    }
                }
            }

            if (!docRelation.ParentDocId.HasValue)
            {
                throw new Exception("Resolution with no parent");
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                Doc caseDoc = this.unitOfWork.DbContext.Set<Doc>()
                    .Include(e => e.DocCorrespondents.Select(d => d.Correspondent))
                    .Include(e => e.DocType)
                    .FirstOrDefault(e => e.DocId == docRelation.RootDocId.Value);

                if (docRelation.ParentDocId == docRelation.RootDocId)
                {
                    EmailType emailType = this.GetEmailTypeByAlias("ResolutionOrTaskAssigned2"); //към преписка

                    foreach (var docCorr in caseDoc.DocCorrespondents)
                    {
                        sb.Append(docCorr.Correspondent.DisplayName).Append("; ");
                    }

                    email.EmailTypeId = emailType.EmailTypeId;

                    email.Subject = emailType.Subject
                        .Replace("@@Param1", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]")
                        .Replace("@@Param2", caseDoc.DocType.Name)
                        .Replace("@@Param3", caseDoc.DocSubject);

                    email.Body = emailType.Body
                        .Replace("@@Param1", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]")
                        .Replace("@@Param2", caseDoc.DocType.Name)
                        .Replace("@@Param3", caseDoc.DocSubject)
                        .Replace("@@Param4", sb.Length > 0 ? sb.ToString() : "[невъведен]")
                        .Replace("@@Param5", docUnitFrom != null ? docUnitFrom.Unit.Name : "[невъведен]")
                        .Replace("@@Param6", String.Format(request.RequestUri.OriginalString.Replace(request.RequestUri.PathAndQuery, "/#/docs/{0}/case"), doc.DocId));
                }
                else
                {
                    EmailType emailType = this.GetEmailTypeByAlias("ResolutionOrTaskAssigned"); //към подчинен документ

                    Doc parentDoc = this.unitOfWork.DbContext.Set<Doc>()
                        .Include(e => e.DocCorrespondents.Select(f => f.Correspondent))
                        .Include(e => e.DocType)
                        .FirstOrDefault(e => e.DocId == docRelation.ParentDocId.Value);

                    foreach (var docCorr in caseDoc.DocCorrespondents)
                    {
                        sb.Append(docCorr.Correspondent.DisplayName).Append("; ");
                    }

                    email.EmailTypeId = emailType.EmailTypeId;

                    email.Subject = emailType.Subject
                        .Replace("@@Param1", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]")
                        .Replace("@@Param2", caseDoc.DocType.Name)
                        .Replace("@@Param3", caseDoc.DocSubject);

                    email.Body = emailType.Body
                        .Replace("@@Param1", !string.IsNullOrEmpty(parentDoc.RegUri) ? parentDoc.RegUri : "[Няма номер на документ]")
                        .Replace("@@Param2", parentDoc.DocType.Name)
                        .Replace("@@Param3", parentDoc.DocSubject)
                        .Replace("@@Param4", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]")
                        .Replace("@@Param5", sb.Length > 0 ? sb.ToString() : "[невъведен]")
                        .Replace("@@Param6", docUnitFrom != null ? docUnitFrom.Unit.Name : "[невъведен]")
                        .Replace("@@Param7", String.Format(request.RequestUri.OriginalString.Replace(request.RequestUri.PathAndQuery, "/#/docs/{0}/case"), doc.DocId));
                }
            }

            return email;
        }

        public Email CreateDocumentEmail(Doc doc, HttpRequestMessage request)
        {
            List<DocUnitRole> docUnitRoles = this.unitOfWork.DbContext.Set<DocUnitRole>().Where(e => e.IsActive).ToList();

            DocUnitRole docUnitRoleFrom = docUnitRoles.SingleOrDefault(e => e.Alias == "From");
            DocUnitRole docUnitRoleImportedBy = docUnitRoles.SingleOrDefault(e => e.Alias == "ImportedBy");
            DocUnitRole docUnitRoleTo = docUnitRoles.SingleOrDefault(e => e.Alias == "To");
            DocUnitRole docUnitRoleCCopy = docUnitRoles.SingleOrDefault(e => e.Alias == "CCopy");

            List<DocUnit> receivingDocUnits = doc.DocUnits.Where(du => du.DocUnitRoleId == docUnitRoleTo.DocUnitRoleId || du.DocUnitRoleId == docUnitRoleCCopy.DocUnitRoleId).ToList();

            DocUnit docUnitFrom = doc.DocUnits.FirstOrDefault(e => e.DocUnitRoleId == docUnitRoleFrom.DocUnitRoleId);

            if (docUnitFrom == null)
            {
                docUnitFrom = doc.DocUnits.FirstOrDefault(e => e.DocUnitRoleId == docUnitRoleImportedBy.DocUnitRoleId);
            }

            DocRelation docRelation = doc.DocRelations.FirstOrDefault();

            Doc caseDoc = this.unitOfWork.DbContext.Set<Doc>()
                                .Include(e => e.DocCorrespondents.Select(d => d.Correspondent))
                                .Include(e => e.DocType)
                                .FirstOrDefault(e => e.DocId == docRelation.RootDocId.Value);

            StringBuilder sb = new StringBuilder();

            EmailStatus pendingStatus = this.GetEmailStatusByAlias("Pending");

            EmailAddresseeType to = this.GetEmailAddresseeTypeByAlias("To");

            Email email = new Email();

            email.EmailStatusId = pendingStatus.EmailStatusId;

            foreach (var item in receivingDocUnits)
            {
                List<UnitUser> receivingUnitUsers = this.unitOfWork.DbContext.Set<UnitUser>()
                    .Include(e => e.User)
                    .Where(e => e.UnitId == item.UnitId)
                    .ToList();

                foreach (var ruu in receivingUnitUsers)
                {
                    if (!string.IsNullOrEmpty(ruu.User.Email) && Regex.IsMatch(ruu.User.Email, this.emailRegex))
                    {
                        email.EmailAddressees.Add(new EmailAddressee()
                        {
                            EmailAddresseeTypeId = to.EmailAddresseeTypeId,
                            Address = ruu.User.Email
                        });
                    }
                }
            }

            if (doc.IsCase)
            {
                EmailType emailType = this.GetEmailTypeByAlias("DocAssigned2"); //към преписка

                foreach (var docCorr in caseDoc.DocCorrespondents)
                {
                    sb.Append(docCorr.Correspondent.DisplayName).Append("; ");
                }

                email.EmailTypeId = emailType.EmailTypeId;

                email.Subject = emailType.Subject
                    .Replace("@@Param1", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]")
                    .Replace("@@Param2", caseDoc.DocType.Name)
                    .Replace("@@Param3", caseDoc.DocSubject);

                email.Body = emailType.Body
                    .Replace("@@Param1", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]")
                    .Replace("@@Param2", caseDoc.DocType.Name)
                    .Replace("@@Param3", caseDoc.DocSubject)
                    .Replace("@@Param4", sb.Length > 0 ? sb.ToString() : "[невъведен]")
                    .Replace("@@Param5", docUnitFrom != null ? docUnitFrom.Unit.Name : "[невъведен]")
                    .Replace("@@Param6", String.Format(request.RequestUri.OriginalString.Replace(request.RequestUri.PathAndQuery, "/#/docs/{0}/case"), doc.DocId));
            }
            else
            {
                EmailType emailType = this.GetEmailTypeByAlias("DocAssigned"); // към документ от преписка

                foreach (var docCorr in caseDoc.DocCorrespondents)
                {
                    sb.Append(docCorr.Correspondent.DisplayName).Append("; ");
                }

                email.EmailTypeId = emailType.EmailTypeId;

                email.Subject = emailType.Subject
                    .Replace("@@Param1", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]")
                    .Replace("@@Param2", caseDoc.DocType.Name)
                    .Replace("@@Param3", caseDoc.DocSubject);

                email.Body = emailType.Body
                    .Replace("@@Param1", !string.IsNullOrEmpty(doc.RegUri) ? doc.RegUri : "[Няма номер на документ]")
                    .Replace("@@Param2", doc.DocType.Name)
                    .Replace("@@Param3", doc.DocSubject)
                    .Replace("@@Param4", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]")
                    .Replace("@@Param5", sb.Length > 0 ? sb.ToString() : "[невъведен]")
                    .Replace("@@Param6", docUnitFrom != null ? docUnitFrom.Unit.Name : "[невъведен]")
                    .Replace("@@Param7", String.Format(request.RequestUri.OriginalString.Replace(request.RequestUri.PathAndQuery, "/#/docs/{0}/case"), doc.DocId));
            }

            return email;
        }

        public Email CreateDocWorkflowEmail(Doc doc, DocWorkflow docWorkflow, bool isRequest, HttpRequestMessage request)
        {
            DocRelation docRelation = doc.DocRelations.FirstOrDefault();

            Doc caseDoc = this.unitOfWork.DbContext.Set<Doc>()
                                .Include(e => e.DocCorrespondents.Select(d => d.Correspondent))
                                .Include(e => e.DocType)
                                .FirstOrDefault(e => e.DocId == docRelation.RootDocId.Value);

            EmailStatus pendingStatus = this.GetEmailStatusByAlias("Pending");

            EmailAddresseeType to = this.GetEmailAddresseeTypeByAlias("To");

            Email email = new Email();

            email.EmailStatusId = pendingStatus.EmailStatusId;

            if (isRequest)
            {
                EmailType emailType = this.GetEmailTypeByAlias("WorkflowActionRequest");

                email.EmailTypeId = emailType.EmailTypeId;

                if (docWorkflow.ToUnitId.HasValue)
                {
                    UnitUser recipient = this.unitOfWork.DbContext.Set<UnitUser>()
                        .Include(e => e.User)
                        .FirstOrDefault(e => e.UnitId == docWorkflow.ToUnitId.Value);

                    if (!string.IsNullOrEmpty(recipient.User.Email) && Regex.IsMatch(recipient.User.Email, this.emailRegex))
                    {
                        email.EmailAddressees.Add(new EmailAddressee()
                        {
                            EmailAddresseeTypeId = to.EmailAddresseeTypeId,
                            Address = recipient.User.Email
                        });
                    }
                }

                email.Subject = emailType.Subject
                    .Replace("@@Param1", !string.IsNullOrEmpty(doc.RegUri) ? doc.RegUri : "[Няма номер на преписка]")
                    .Replace("@@Param2", doc.DocType.Name)
                    .Replace("@@Param3", doc.DocSubject);

                Unit sender = null;

                if (docWorkflow.PrincipalUnitId.HasValue)
                {
                    sender = this.unitOfWork.DbContext.Set<Unit>().FirstOrDefault(e => e.UnitId == docWorkflow.PrincipalUnitId.Value);
                }

                email.Body = emailType.Body
                    .Replace("@@Param1", !string.IsNullOrEmpty(doc.RegUri) ? doc.RegUri : "[Няма номер на преписка]")
                    .Replace("@@Param2", doc.DocType.Name)
                    .Replace("@@Param3", doc.DocSubject)
                    .Replace("@@Param4", sender != null ? sender.Name : "[невъведен]")
                    .Replace("@@Param5", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]")
                    .Replace("@@Param6", String.Format(request.RequestUri.OriginalString.Replace(request.RequestUri.PathAndQuery, "/#/docs/{0}/case"), doc.DocId));
            }
            else
            {
                EmailType emailType = this.GetEmailTypeByAlias("WorkflowAction");

                email.EmailTypeId = emailType.EmailTypeId;

                if (docWorkflow.PrincipalUnitId.HasValue)
                {
                    DocWorkflowAction dwAction = this.unitOfWork.DbContext.Set<DocWorkflowAction>().Single(e => e.DocWorkflowActionId == docWorkflow.DocWorkflowActionId);

                    List<int> requestingUnitIds = this.unitOfWork.DbContext.Set<DocWorkflow>()
                        .Where(e => e.DocId == doc.DocId && e.ToUnitId == docWorkflow.PrincipalUnitId.Value && e.DocWorkflowAction.Alias == dwAction.Alias + "Request")
                        .Where(e => e.PrincipalUnitId.HasValue)
                        .Select(e => e.PrincipalUnitId.Value)
                        .ToList();

                    List<UnitUser> recipients = this.unitOfWork.DbContext.Set<UnitUser>()
                        .Include(e => e.User)
                        .Where(e => requestingUnitIds.Contains(e.UnitId))
                        .ToList();

                    foreach (var item in recipients)
                    {
                        if (!string.IsNullOrEmpty(item.User.Email) && Regex.IsMatch(item.User.Email, this.emailRegex))
                        {
                            email.EmailAddressees.Add(new EmailAddressee()
                            {
                                EmailAddresseeTypeId = to.EmailAddresseeTypeId,
                                Address = item.User.Email
                            });
                        }
                    }
                }

                email.Subject = emailType.Subject
                            .Replace("@@Param1", !string.IsNullOrEmpty(doc.RegUri) ? doc.RegUri : "[Няма номер на преписка]")
                            .Replace("@@Param2", doc.DocType.Name)
                            .Replace("@@Param3", doc.DocSubject);

                email.Body = emailType.Body
                    .Replace("@@Param1", !string.IsNullOrEmpty(doc.RegUri) ? doc.RegUri : "[Няма номер на преписка]")
                    .Replace("@@Param2", doc.DocType.Name)
                    .Replace("@@Param3", doc.DocSubject)
                    .Replace("@@Param4", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]")
                    .Replace("@@Param5", String.Format(request.RequestUri.OriginalString.Replace(request.RequestUri.PathAndQuery, "/#/docs/{0}/case"), doc.DocId));
            }

            return email;
        }

        public Email CreateElectronicServiceStageChangeEmail(Doc caseDoc, DocElectronicServiceStage docElectronicServiceStage, HttpRequestMessage request)
        {
            EmailStatus pendingStatus = this.GetEmailStatusByAlias("Pending");

            EmailType emailType = this.GetEmailTypeByAlias("ElectronicServiceStageChanged");

            EmailAddresseeType to = this.GetEmailAddresseeTypeByAlias("To");

            Email email = new Email();

            email.EmailStatusId = pendingStatus.EmailStatusId;
            email.EmailTypeId = emailType.EmailTypeId;

            foreach (var item in caseDoc.DocCorrespondents)
            {
                if (!string.IsNullOrEmpty(item.Correspondent.Email) && Regex.IsMatch(item.Correspondent.Email, this.emailRegex))
                {
                    email.EmailAddressees.Add(new EmailAddressee()
                    {
                        EmailAddresseeTypeId = to.EmailAddresseeTypeId,
                        Address = item.Correspondent.Email
                    });
                }
            }

            ElectronicServiceStage ess = this.unitOfWork.DbContext.Set<ElectronicServiceStage>()
                .FirstOrDefault(e => e.ElectronicServiceStageId == docElectronicServiceStage.ElectronicServiceStageId);

            email.Subject = emailType.Subject
                .Replace("@@CaseNum", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]");
            email.Body = emailType.Body
                .Replace("@@CaseNum", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]")
                .Replace("@@StageName", ess.Name)
                .Replace("@@CaseViewUrl", EmailRepository.CaseViewUrl)
                .Replace("@@AccessCode", caseDoc.AccessCode);

            return email;
        }

        public Email CreateCorrespondentEmail(CorrespondentEmailDO data)
        {
            EmailStatus pendingStatus = this.GetEmailStatusByAlias("Pending");

            EmailAddresseeType to = this.GetEmailAddresseeTypeByAlias("To");

            EmailAddresseeType bcc = this.GetEmailAddresseeTypeByAlias("Bcc");

            Email email = new Email();

            email.EmailTypeId = data.EmailTypeId.Value;
            email.EmailStatusId = pendingStatus.EmailStatusId;
            email.Subject = data.Subject;
            email.Body = data.Body;

            List<int> correspondentIds = data.EmailTo.Select(e => e.NomValueId).ToList();
            List<Correspondent> correspondents = this.unitOfWork.DbContext.Set<Correspondent>()
                .Where(e => correspondentIds.Contains(e.CorrespondentId))
                .ToList();

            foreach (var item in correspondents)
            {
                if (!string.IsNullOrEmpty(item.Email) && Regex.IsMatch(item.Email, this.emailRegex))
                {
                    email.EmailAddressees.Add(new EmailAddressee()
                        {
                            EmailAddresseeTypeId = to.EmailAddresseeTypeId,
                            Address = item.Email
                        });
                }
            }

            if (!string.IsNullOrEmpty(data.EmailBcc))
            {
                string[] bccs = data.EmailBcc.Split(';').Select(e => e.Trim()).ToArray();

                foreach (var item in bccs)
                {
                    if (Regex.IsMatch(item, this.emailRegex))
                    {
                        email.EmailAddressees.Add(new EmailAddressee()
                        {
                            EmailAddresseeTypeId = bcc.EmailAddresseeTypeId,
                            Address = item
                        });
                    }
                }
            }

            foreach (var item in data.PublicFiles)
            {
                email.EmailAttachments.Add(new EmailAttachment()
                {
                    Name = item.File.Name,
                    ContentId = item.File.Key
                });
            }

            return email;
        }

        public EmailStatus GetEmailStatusByAlias(string alias)
        {
            return this.unitOfWork.DbContext.Set<EmailStatus>().FirstOrDefault(e => e.Alias.ToLower() == alias.ToLower());
        }

        public EmailAddresseeType GetEmailAddresseeTypeByAlias(string alias)
        {
            return this.unitOfWork.DbContext.Set<EmailAddresseeType>().FirstOrDefault(e => e.Alias.ToLower() == alias.ToLower());
        }

        public EmailType GetEmailTypeByAlias(string alias)
        {
            return this.unitOfWork.DbContext.Set<EmailType>().SingleOrDefault(e => e.Alias.ToLower() == alias.ToLower());
        }
    }
}
