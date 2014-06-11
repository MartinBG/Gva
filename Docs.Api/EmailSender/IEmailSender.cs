using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.EmailSender
{
    public interface IEmailSender
    {
        SmtpClient SmtpClient { get; set; }
        void Send(int pendingAdministrativeEmailId);
    }
}