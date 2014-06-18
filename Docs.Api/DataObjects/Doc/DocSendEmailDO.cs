using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocSendEmailDO
    {
        public DocSendEmailDO()
        {
            this.EmailTo = new List<NomDo>();
        }

        public List<NomDo> EmailTo { get; set; }
        public string EmailBcc { get; set; }
        public int? CorrespondentContactId { get; set; }
        public int? CorrespondentId { get; set; }
        public int? UserId { get; set; }
        public int? TypeId { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
