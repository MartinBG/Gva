using Common.Api.Models;
using Common.Data;
using Common.Extensions;
using Docs.Api.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace Docs.Api.EmailSender
{
    class EmailSender : IEmailSender
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private IUnitOfWork unitOfWork;

        private string emailRegex = @"^[\w\-!#$%&'*+/=?^`{|}~.""]+@([\w]+[.-]?)+[\w]\.[\w]+$";

        public SmtpClient SmtpClient { get; set; }

        public EmailSender(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Send(int pendingAdministrativeEmailId)
        {
            try
            {
                int statusDeliveredId = this.unitOfWork.DbContext.Set<AdministrativeEmailStatus>().Single(e => e.Alias == "Delivered").AdministrativeEmailStatusId;
                int statusFailedId = this.unitOfWork.DbContext.Set<AdministrativeEmailStatus>().Single(e => e.Alias == "Failed").AdministrativeEmailStatusId;

                AdministrativeEmail administrativeEmail = this.unitOfWork.DbContext.Set<AdministrativeEmail>().Single(e => e.AdministrativeEmailId == pendingAdministrativeEmailId);

                List<MailAddress> mails = new List<MailAddress>();

                bool sendMail = false;
                string email = String.Empty;
                string name = String.Empty;

                if (administrativeEmail.UserId.HasValue)
                {
                    User receiver = this.unitOfWork.DbContext.Set<User>().Find(administrativeEmail.UserId.Value);
                    if (receiver != null)
                    {
                        if (!string.IsNullOrEmpty(receiver.Email) && Regex.IsMatch(receiver.Email, emailRegex))
                        {
                            email = receiver.Email;
                            name = receiver.Fullname;

                            sendMail = true;
                        }
                        else
                        {
                            administrativeEmail.StatusId = statusFailedId;

                            logger.Info(String.Format("User id = {0} mail missing or invalid: {1} ", administrativeEmail.UserId, receiver.Email));
                        }
                    }
                    else
                    {
                        logger.Info(String.Format("User id = {0} user missing  ", administrativeEmail.UserId));
                    }
                }
                else if (administrativeEmail.CorrespondentId.HasValue)
                {
                    Correspondent receiver = this.unitOfWork.DbContext.Set<Correspondent>().Find(administrativeEmail.CorrespondentId.Value);
                    if (receiver != null)
                    {
                        if (!string.IsNullOrEmpty(receiver.Email) && Regex.IsMatch(receiver.Email, emailRegex))
                        {
                            email = receiver.Email;
                            name = receiver.DisplayName;

                            sendMail = true;
                        }
                        else
                        {
                            administrativeEmail.StatusId = statusFailedId;

                            logger.Info(String.Format("Correspondent id = {0} mail missing or invalid: {1} ", administrativeEmail.CorrespondentId, receiver.Email));
                        }
                    }
                    else
                    {
                        logger.Info(String.Format("Correspondent id = {0} correspondent missing  ", administrativeEmail.CorrespondentId));
                    }
                }
                else
                {
                    throw new Exception("Receiver is missing.");
                }

                if (sendMail)
                {
                    MailAddress from = new MailAddress(((NetworkCredential)this.SmtpClient.Credentials).UserName);
                    MailAddress to = new MailAddress(email);

                    MailMessage mailMessage = new MailMessage(from, to);
                    mailMessage.Subject = administrativeEmail.Subject;
                    mailMessage.Body = administrativeEmail.Body;
                    mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                    mailMessage.IsBodyHtml = true;

                    try
                    {
                        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                        this.SmtpClient.Send(mailMessage);
                        administrativeEmail.StatusId = statusDeliveredId;
                        administrativeEmail.SentDate = DateTime.Now;
                    }

                    catch (Exception ex)
                    {
                        administrativeEmail.StatusId = statusFailedId;

                        StringBuilder errorText = new StringBuilder();
                        errorText.AppendLine(String.Format("Error while sending email message from server {0}", this.SmtpClient.Host.ToString()));
                        errorText.AppendLine(String.Format("Email: {0}", mailMessage.Subject));
                        errorText.AppendLine(String.Format("AdministrativeEmailId: {0}", administrativeEmail.AdministrativeEmailId));
                        errorText.AppendLine();

                        logger.Error("EmailSender Exception: " + errorText.ToString() + Helper.GetDetailedExceptionInfo(ex));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("EmailSender Exception: " + String.Format("AdministrativeEmailId={0} ", pendingAdministrativeEmailId) + Helper.GetDetailedExceptionInfo(ex));

                var email = this.unitOfWork.DbContext.Set<AdministrativeEmail>().SingleOrDefault(e => e.AdministrativeEmailId == pendingAdministrativeEmailId);
                email.StatusId = this.unitOfWork.DbContext.Set<AdministrativeEmailStatus>().Single(e => e.Alias == "Failed").AdministrativeEmailStatusId;
            }
            finally
            {
                this.unitOfWork.Save();
            }
        }
    }
}
