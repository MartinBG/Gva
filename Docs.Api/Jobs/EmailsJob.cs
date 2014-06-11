using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Autofac.Features.OwnedInstances;
using Common.Data;
using Common.Extensions;
using Common.Jobs;
using Docs.Api.Models;
using NLog;
using Docs.Api.EmailSender;
using System.Net.Mail;
using System.Net;

namespace Docs.Api.Jobs
{
    public class EmailsJob : IJob
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private Func<Owned<IUnitOfWork>> unitOfWorkFactory;
        private Func<Owned<IEmailSender>> emailSenderFactory;

        public EmailsJob(Func<Owned<IUnitOfWork>> unitOfWorkFactory, Func<Owned<IEmailSender>> emailSenderFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.emailSenderFactory = emailSenderFactory;
        }

        public string Name
        {
            get { return "EmailsJob"; }
        }

        public TimeSpan Period
        {
            get
            {
                return TimeSpan.FromSeconds(int.Parse(ConfigurationManager.AppSettings["Docs.Api:EmailsJobIntervalInSeconds"]));
            }
        }

        public void Action()
        {
            try
            {
                List<int> pendingAdministrativeEmails = null;

                int mailsToBeSentCount = Int32.Parse(ConfigurationManager.AppSettings["Docs.Api:MailsToBeSentCount"]);

                using (var unitOfWork = unitOfWorkFactory())
                {
                    pendingAdministrativeEmails = unitOfWork.Value.DbContext.Set<AdministrativeEmail>()
                        .Where(e => e.AdministrativeEmailStatus.Alias == "Pending")
                        .Take(mailsToBeSentCount)
                        .Select(d => d.AdministrativeEmailId)
                        .ToList();
                }

                if (pendingAdministrativeEmails.Count > 0)
                {
                    string smtpNetworkCredentialHost = ConfigurationManager.AppSettings["Docs.Api:SmtpNetworkCredentialHost"];
                    string smtpNetworkCredentialPort = ConfigurationManager.AppSettings["Docs.Api:SmtpNetworkCredentialPort"];
                    string smtpNetworkCredentialName = ConfigurationManager.AppSettings["Docs.Api:SmtpNetworkCredentialName"];
                    string smtpNetworkCredentialPassword = ConfigurationManager.AppSettings["Docs.Api:SmtpNetworkCredentialPassword"];
                    string smtpNetworkCredentialEnableSsl = ConfigurationManager.AppSettings["Docs.Api:SmtpNetworkCredentialEnableSsl"];

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = smtpNetworkCredentialHost;
                    if (!String.IsNullOrEmpty(smtpNetworkCredentialPort))
                    {
                        smtpClient.Port = Convert.ToInt32(smtpNetworkCredentialPort);
                    }
                    if (!String.IsNullOrEmpty(smtpNetworkCredentialName) && !String.IsNullOrEmpty(smtpNetworkCredentialPassword))
                    {
                        smtpClient.Credentials = new NetworkCredential(smtpNetworkCredentialName, smtpNetworkCredentialPassword);
                    }
                    if (!String.IsNullOrEmpty(smtpNetworkCredentialEnableSsl))
                    {
                        smtpClient.EnableSsl = true;
                    }

                    foreach (int administrativeEmailId in pendingAdministrativeEmails)
                    {
                        using (var emailSender = emailSenderFactory())
                        {
                            emailSender.Value.SmtpClient = smtpClient;
                            emailSender.Value.Send(administrativeEmailId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("General error: " + Helper.GetDetailedExceptionInfo(ex));
            }
        }
    }
}