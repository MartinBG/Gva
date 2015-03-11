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
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using Common.Blob;

namespace Docs.Api.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private IUnitOfWork unitOfWork;

        public static string emailRegex = @"^[\w\-!#$%&'*+/=?^`{|}~.""]+@([\w]+[.-]?)+[\w]\.[\w]+$";

        public SmtpClient SmtpClient { get; set; }

        public EmailSender(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Send(int pendingEmailId)
        {
            EmailStatus deliveredStatus = this.unitOfWork.DbContext.Set<EmailStatus>().Single(e => e.Alias == "Delivered");
            EmailStatus failedStatus = this.unitOfWork.DbContext.Set<EmailStatus>().Single(e => e.Alias == "Failed");

            try
            {
                Email email = this.unitOfWork.DbContext.Set<Email>()
                    .Include(e => e.EmailAddressees.Select(a => a.EmailAddresseeType))
                    .Include(e => e.EmailAttachments)
                    .Single(e => e.EmailId == pendingEmailId);

                if (!email.EmailAddressees.Any(e => e.EmailAddresseeType.Alias == "To" || e.EmailAddresseeType.Alias == "Cc"))
                {
                    //? different exception so it can be treated as a separate case
                    throw new Exception("Unspecified email recipient.");
                }

                MailMessage mailMessage = new MailMessage();
                mailMessage.Subject = email.Subject;
                mailMessage.Body = email.Body;
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.IsBodyHtml = true;

                var sender = email.EmailAddressees.FirstOrDefault(e => e.EmailAddresseeType.Alias == "From");

                if (sender != null)
                {
                    mailMessage.From = new MailAddress(sender.Address);
                }
                else
                {
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Docs.Api:SmtpNetworkCredentialName"]);
                }

                foreach (var item in email.EmailAddressees.Where(e => e.EmailAddresseeType.Alias == "To"))
                {
                    mailMessage.To.Add(new MailAddress(item.Address));
                }

                foreach (var item in email.EmailAddressees.Where(e => e.EmailAddresseeType.Alias == "Cc"))
                {
                    mailMessage.CC.Add(new MailAddress(item.Address));
                }

                foreach (var item in email.EmailAddressees.Where(e => e.EmailAddresseeType.Alias == "Bcc"))
                {
                    mailMessage.Bcc.Add(new MailAddress(item.Address));
                }

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                {
                    connection.Open();

                    foreach (var item in email.EmailAttachments)
                    {
                        // do not dispose the memory stream because the attachment needs it, it's safe to do so
                        MemoryStream m1 = new MemoryStream();
                        using (var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", item.ContentId))
                        {
                            blobStream.CopyTo(m1);

                            m1.Position = 0;

                            Attachment attachment = new Attachment(m1, item.Name);

                            mailMessage.Attachments.Add(attachment);
                        }
                    }
                }

                try
                {
                    //? what is this
                    //ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                    this.SmtpClient.Send(mailMessage);
                    email.EmailStatusId = deliveredStatus.EmailStatusId;
                    email.SentDate = DateTime.Now;
                }
                catch (Exception ex)
                {
                    email.EmailStatusId = failedStatus.EmailStatusId;

                    StringBuilder errorText = new StringBuilder();
                    errorText.AppendLine(String.Format("Error while sending email message from server {0}", this.SmtpClient.Host.ToString()));
                    errorText.AppendLine(String.Format("Email: {0}", mailMessage.Subject));
                    errorText.AppendLine(String.Format("Email ID: {0}", email.EmailId));
                    errorText.AppendLine();

                    logger.Error("EmailSender Exception: " + errorText.ToString() + Helper.GetDetailedExceptionInfo(ex));
                }
            }
            catch (Exception ex)
            {
                logger.Error("EmailSender Exception: " + String.Format("Email ID={0} ", pendingEmailId) + Helper.GetDetailedExceptionInfo(ex));

                Email email = this.unitOfWork.DbContext.Set<Email>().Single(e => e.EmailId == pendingEmailId);
                email.EmailStatusId = failedStatus.EmailStatusId;
            }
            finally
            {
                this.unitOfWork.Save();
            }
        }
    }
}
